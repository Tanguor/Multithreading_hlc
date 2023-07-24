//
// HALCON/.NET (C#) multithreading example
//
// © 2007-2022 MVTec Software GmbH
//
// WorkerThread.cs: Defines the behavior of the worker threads.
//


using System.Collections;

using HalconDotNet;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Drawing;
using Microsoft.VisualBasic.ApplicationServices;

/////////////////////////////////////////////////////////////////////////////
// Detailed information:
// When using multiple threads you have to ensure that the data shared
// is valid at any time of the execution. For this, mutexes are used to
// guarantee mutual access to shared data objects. Besides, event handles
// are used to synchronize the threads with each other.
//
// The GUI depicts the main thread of the application, which is also in charge
// of displaying the results.
//
// When you press the Start button, the two (secondary) thread handles
// (threadAcq and threadIP) are 'triggered', which then start the (global) thread
// functions ImgAcqRun (image acquisition) and IPRun (image processing), respectively.
// Since these processing tasks are encapsulated units, the necessary handles
// and variables are initialized and closed within the thread functions.
//
// When you press the Stop button, the StopEvent is sent, which causes all
// threads to finish their current procedure, close all handles opened
// initially, and leave the coresponding thread function.
//
// The threads share the following data:
//
//   threadAcq & threadIP:   image    (ArrayList  imgList)
//
//   threadIP & main thread: results  (struct ResultContainer  resultData)
//
// The two variables are protected by the following mutexes:
//
//   imgList     => newImageMutex
//
//   resultData  => resultDataMutex
//
// Events exchanged among threads are as follows:
//
//   threadAcq -> threadIP:    newImageEvent
//
//   threadIP  -> main thread: newResultEvent
//
//   threadIP  <- main thread: containerIsFreeEvent
//
// All thread handles also listen for the StopEvent triggered by
// the Stop button.
//
/////////////////////////////////////////////////////////////////////////////

namespace MultiThreading
{
    delegate void FuncDelegate();

    public class WorkerThread
    {

        // Shared data and mutexes.
        ResultContainer resultData;
        ArrayList imgList;
        Mutex newImgMutex;
        Mutex resultDataMutex;

        // Event handles to synchronize threads.
        AutoResetEvent newImgEvent;
        AutoResetEvent newResultEvent;
        AutoResetEvent newRecipeIdEvent;
        AutoResetEvent communicationErrorEvent;
        ManualResetEvent containerIsFreeEvent;
        ManualResetEvent delegatedStopEvent;



        // Access to instances of GUI.
        MultiThreadingForm mainForm;
        HWindow window = null;

        // Auxiliary variables.
        FuncDelegate delegateDisplay;
        FuncDelegate delegateControlReset;
        const int MAX_BUFFERS = 1;
        public string tempRecipeId;
        public string recipeId;
        public bool ContinuousAcquisition { get; set; } = true;
        public string AdressIp { get; set; }
        public string Port { get; set; }
        public string FileChosen {get; set;}


        // Constructor: set up class members.
        public WorkerThread(MultiThreadingForm form)
        {
            newImgEvent = new AutoResetEvent(false);
            newResultEvent = new AutoResetEvent(false);
            newRecipeIdEvent = new AutoResetEvent(false);
            communicationErrorEvent = new AutoResetEvent(false);
            containerIsFreeEvent = new ManualResetEvent(true);
            recipeId = null;


            resultData = new ResultContainer();
            newImgMutex = new Mutex();
            resultDataMutex = new Mutex();

            mainForm = form;
            delegatedStopEvent = form.stopEventHandle;

            delegateDisplay = new FuncDelegate(DisplayResults);
            delegateControlReset = new FuncDelegate(mainForm.ResetControls);
            imgList = new ArrayList();
        }
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public void RequestStop()
        {
            cancellationTokenSource.Cancel();
        }
        //////////////////////////////////////////////////////////////////////////////
        //  Init() - The event handles used to synchronize the threads must be
        //           reset before a new thread.Start() can be used.
        //           If the imageList buffer wasn't processed completely (during
        //           the last run), the list needs to be emptied before it is
        //           used for the next run.
        //////////////////////////////////////////////////////////////////////////////
        public void Init()
        {
            newImgEvent.Reset();
            newResultEvent.Reset();
            containerIsFreeEvent.Set();

            int length = imgList.Count;
            for (int i = 0; i < length; i++)
            {
                HImage image = (HImage)imgList[0];
                imgList.Remove(image);
                image.Dispose();
            }

            window = mainForm.GetHalconWindow();
        }

        //////////////////////////////////////////////////////////////////////////////
        // DisplayResults() - This method is used in/as a delegate. It is invoked
        //                    from the main GUI thread
        //////////////////////////////////////////////////////////////////////////////
        public void DisplayResults()
        {
            // -------------------  INIT ----------------

            int i;
            HTuple rows = new HTuple(), cols = new HTuple(), phi = new HTuple(), confidences = new HTuple();
            HTuple lengths2 = new HTuple(), lengths1 = new HTuple(), Nombre_de_piece = new HTuple(); HObject rectContour;
            HTuple width, height;
            HOperatorSet.GetDictTuple(resultData.DLResult, "bbox_class_id", out Nombre_de_piece);

            resultDataMutex.WaitOne(); // CriticalSect.
            HTuple time = resultData.timeNeeded; // CriticalSect.
            HObject image = resultData.resultImg; // CriticalSect.
            HTuple DLResultBatch = resultData.DLResult; // Get the DL results
            containerIsFreeEvent.Set(); // CriticalSect.
            resultDataMutex.ReleaseMutex(); // CriticalSect.

            // Assuming DLResultBatch is a dictionary with the bounding box parameters as keys
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_row", out rows);
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_col", out cols);
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_length1", out lengths1);
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_length2", out lengths2);
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_phi", out phi);
            HOperatorSet.GetDictTuple(DLResultBatch, "bbox_confidence", out confidences);

            // Get the size of the image
            HOperatorSet.GetImageSize(image, out width, out height);

            //-------------------DISPLAY-----------------

            // Set the part of the image to display to be the entire image
            window.SetPart(0, 0, height.I - 1, width.I - 1);
            window.DispObj(image);

            // Draw the bounding boxes for all detected objects
            for (i = 0; i < rows.Length; i++)
            {
                window.SetColor("red");
                // Generate the contour of the rectangle
                HOperatorSet.GenRectangle2ContourXld(out rectContour, rows[i].D, cols[i].D, phi[i].D, lengths1[i].D, lengths2[i].D);
                // Display the rectangle contour
                window.SetLineWidth(3);
                window.DispObj(rectContour);
                // Don't forget to dispose of the contour to prevent memory leaks
                rectContour.Dispose();
            }
            //show Confidence Text
            for (i = 0; i < rows.Length; i++) //show Confidence 
            {
                // Set font size and color
                window.SetFont("Consolas-Bold-13");  // Adjust the size as per your requirement
            window.SetColor("black");

            // Write the confidence at the top left corner of the bounding box
            int offset = 12;  // Adjust this value as per your requirement
            int rowInt = (int)rows[i].D;
            int colInt = (int)cols[i].D;
            window.SetTposition(rowInt - offset, colInt);
            window.WriteString($"Confidence: {confidences[i].D:F2}");  // F2 formats the confidence value to 2 decimal places
            }

            mainForm.procTimeLabel.Text = time.TupleString(".1f") + "  ms";
            mainForm.procTimeLabel.Refresh();

            // Assuming the DLResultBatch is a HTuple that can be converted to string
            // and displayed in the imageDataLabel. Modify as needed to suit your actual use case.
            mainForm.imageDataLabel.Text = Nombre_de_piece.Length.ToString();
            mainForm.imageDataLabel.Refresh();


            StringBuilder RroundedStr = new StringBuilder();
            StringBuilder CroundedStr = new StringBuilder();

            for (int j = 0; j < rows.Length; j++)
            {
                if (j != 0)
                {
                    RroundedStr.Append(" - ");
                    CroundedStr.Append(" - ");
                }
                RroundedStr.Append(rows[j].D.ToString("F2")); // F4 specifies 4 decimal places
                CroundedStr.Append(cols[j].D.ToString("F2")); // F4 specifies 4 decimal places
            }

            mainForm.XCordLabel.Text = RroundedStr.ToString();
            mainForm.XCordLabel.Refresh();
            mainForm.YCordLabel.Text = CroundedStr.ToString();
            mainForm.YCordLabel.Refresh();

            image.Dispose();
        }

        //////////////////////////////////////////////////////////////////////////////
        // FGRun() - The thread functionFGRun is in charge of the asynchronous
        //           grabbing. To pass the images to the  other threads, we use
        //           a list. In case the list exceeds a certain length, because
        //           the processing thread is slower then the grabbing thread,
        //           we omit new images until the list decreases again.
        //           To prevent data races, we use a mutex to assure mutual
        //           access to the image list.
        //////////////////////////////////////////////////////////////////////////////
        public void ImgAcqRun()
        {
            // -------------------  INIT ----------------
            HFramegrabber acquisition = null;
            // ----------- TRY CONNECTION TO CAMERA --------
            while (!cancellationTokenSource.Token.IsCancellationRequested) //Si une recette est trouvée, lance la connexion avec notre caméra
            {
                if (newRecipeIdEvent.WaitOne(0))
                {
                    recipeId = tempRecipeId;
                    tempRecipeId = "";
                    newRecipeIdEvent.Reset();
                    continue;
                }
                if (communicationErrorEvent.WaitOne(0))
                {
                    // Handle the communication error
                    // ...
                }
                newRecipeIdEvent.WaitOne();
                try
                {
                    if (mainForm.IsCheckboxChecked()) // SI OUI LIRE DANS LE DOSSIER DE DESTINATION
                    {
                        // Get a list of all the image files in the directory
                        string file = null;
                        if (FileChosen == null) {
                            file = "C:\\Users\\tanguy.lebret\\Documents\\Image\\Sombre"; 
                        }
                        else
                        {
                            file = FileChosen;
                        }
                        string[] imageFiles = Directory.GetFiles(file, "*.PNG");
                        
                        // Loop over the image files
                        foreach (string imageFile in imageFiles)
                        {
                            // Read the image from the file
                            HImage grabbedImage = new HImage();
                            newImgMutex.WaitOne();
                            grabbedImage.ReadImage(imageFile);

                            // Add the image to imgList if there's room, or dispose of it if there isn't
                            if (imgList.Count < MAX_BUFFERS)
                            {
                                imgList.Add(grabbedImage);
                            }
                            else
                            {
                                grabbedImage.Dispose();
                            }

                            newImgMutex.ReleaseMutex(); // CriticalSect.

                            newImgEvent.Set();

                            if (delegatedStopEvent.WaitOne(0, true)) break;
                        }
                    }
                    else
                    {
                        // -------------  CONNEXION CAMERA ----------------
                        acquisition = new HFramegrabber("GigEVision2", 0, 0, 0, 0, 0, 0, "progressive", -1, "default",
                                    -1, "false", "default", "000f315c5fde_AlliedVisionTechnologies_MakoG131B5080", 0, -1);
                        while (!cancellationTokenSource.Token.IsCancellationRequested && newRecipeIdEvent.WaitOne())
                        {
                            HImage grabbedImage = acquisition.GrabImageAsync(-1);
                            newImgMutex.WaitOne(); // CriticalSect.
                            if (imgList.Count < MAX_BUFFERS) // CriticalSect.
                            {
                                imgList.Add(grabbedImage);
                            }
                            else
                            {
                                grabbedImage.Dispose();
                            }
                            newImgMutex.ReleaseMutex(); // CriticalSect.

                            newImgEvent.Set();

                            if (delegatedStopEvent.WaitOne(0, true)) break;
                        }
                        acquisition.Dispose();
                    }
                    newImgEvent.Reset();
                }
                catch (Exception ex)
                {
                    // Handle the error in a way that suits your needs. For example:
                    MessageBox.Show("La caméra n'est pas connectée. Error halcon: " + ex.Message);
                    delegatedStopEvent.Set();

                    // Invoke the delegate to reset the UI controls
                    mainForm.Invoke(delegateControlReset);
                }
                if (!ContinuousAcquisition)
                {
                    break;
                }
                if (delegatedStopEvent.WaitOne(0, true)) break;
            }
            newImgEvent.Reset();
            newRecipeIdEvent.Reset();
            return;  // Exit the method
        }

        //////////////////////////////////////////////////////////////////////////////
        //  IPRun() - The thread function IPRun performs the image processing.
        //            It waits for the grabbing thread to indicate a new image in the
        //            image list. After calling the operator FindDataCode2D, the
        //            result values are stored in the ResultContainer instance
        //            resultData, which can be entered only after the previous result
        //            values were displayed (containerIsFree-event).
        //////////////////////////////////////////////////////////////////////////////
        public void IPRun()
        {
            // -------------------  INIT ----------------

            // Load the pretrained deep learning model & Hand-Eye Calibration Parameters
            HTuple hv_DLModelHandle,min_confidence;
            HOperatorSet.ReadDlModel("C:\\Users\\tanguy.lebret\\Documents\\model_LR_opt.hdl", out hv_DLModelHandle);
            HTuple HomMat2D = new HTuple(new double[] { 0.0141965, 0.218402, -495.553, -0.216524, 0.0145446, 99.0114, 0, 0, 1 });
            HTuple X, Y, hv_phi, rows = new HTuple(), cols = new HTuple();

            // Load the preprocessing parameters
            HTuple hv_DLPreprocessParam; HObject preprocessedImage;
            HOperatorSet.ReadDict("C:/Users/tanguy.lebret/Documents/model_LR_opt_dl_preprocess_params.hdict", new HTuple(), new HTuple(), out hv_DLPreprocessParam);
            double minConfidence;
            if (mainForm.TrackBarValues.TryDequeue(out minConfidence))
            {
                // If a value was successfully dequeued, use it to set the min_confidence parameter
                HOperatorSet.SetDlModelParam(hv_DLModelHandle, "min_confidence", minConfidence);
            }
            // -----------  WAIT FOR EVENTS  ---------------

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                if (!newImgEvent.WaitOne(TimeSpan.FromSeconds(0.20)))
                {
                    continue;
                }
                newImgMutex.WaitOne(); // CriticalSect.
                HImage image = (HImage)imgList[0]; // CriticalSect.
                imgList.Remove(image); // CriticalSect.
                newImgMutex.ReleaseMutex(); // CriticalSect.

                // Preprocess the image
                dl_preprocessing_ns.dl_preprocessing_pn.preprocess_dl_model_images(image, out preprocessedImage, hv_DLPreprocessParam);
                HTuple t1 = HSystem.CountSeconds();

                // Prepare the image for the deep learning model
                HTuple sample;
                dl_Dataset_ns.dl_Dataset_pn.gen_dl_samples_from_images(preprocessedImage, out sample);

                // Apply the deep learning model to the image
                HTuple DLResultBatch;
                HOperatorSet.ApplyDlModel(hv_DLModelHandle, sample, new HTuple(), out DLResultBatch);
                HTuple t2 = HSystem.CountSeconds();

                // ---------  CONVERT COORDINATES -------------
                HOperatorSet.GetDictTuple(DLResultBatch, "bbox_row", out rows);
                HOperatorSet.GetDictTuple(DLResultBatch, "bbox_col", out cols);
                HOperatorSet.GetDictTuple(DLResultBatch, "bbox_phi", out hv_phi);
                HOperatorSet.AffineTransPoint2d(HomMat2D, rows, cols, out X, out Y);

                // --------  CREATE CONNECTION STRING ---------
                int maxCount = Math.Min(X.Length, 3);

                StringBuilder message = new StringBuilder();
                message.AppendFormat("{0};", maxCount);

                double v = 0;
                double w = 0;

                for (int i = 0; i < maxCount; i++)
                {
                    if (i < hv_phi.Length)
                    {
                        v = 0 * Math.Sin(hv_phi[i].D * Math.PI / 180);
                        w = 0 * Math.Cos(hv_phi[i].D * Math.PI / 180);
                    }

                    if (i < X.Length && i < Y.Length)
                    {
                        //z = 28 in this string.
                        message.AppendFormat("{0:0.00}u{1:0.00}u{2:0.00}u28.00u{3:0.00}u{4:0.00}", 4*X[i].D, 5*Y[i].D, hv_phi[i].D, v, w);
                    }

                    if (i < maxCount - 1)
                        message.Append(";");
                }
                message.AppendLine();
                message.Replace(',', '.');

                // -----------  SAVE PARAMETER  ---------------
                containerIsFreeEvent.WaitOne();
                resultDataMutex.WaitOne(); // CriticalSect.
                resultData.timeNeeded = (1000 * (t2 - t1)); // CriticalSect.
                resultData.resultImg = preprocessedImage; // CriticalSect.
                resultData.DLResult = DLResultBatch; // CriticalSect.
                resultData.X = X;
                resultData.Y = Y;
                containerIsFreeEvent.Reset(); // CriticalSect.
                resultDataMutex.ReleaseMutex(); // CriticalSect.
                newResultEvent.Set();

                mainForm.Invoke(delegateDisplay);
                // Convert the lengths according to your image size

                if (!ContinuousAcquisition)
                {
                    break;
                }
                if (delegatedStopEvent.WaitOne(0, true)) break;
            }
            // --------  RESET/CLOSE ALL HANDLES  ---------

            mainForm.threadAcq.Join();
            mainForm.Invoke(delegateControlReset);

            hv_DLModelHandle.Dispose();
            hv_DLPreprocessParam.Dispose();

            newResultEvent.Reset();

            return;
        }


        //////////////////////////////////////////////////////////////////////////////
        // CommunicationRun() - This thread function is responsible for handling
        //                      communication with a remote server. It attempts to
        //                      connect to the server and then enters a loop where it
        //                      waits for messages. When a message is received, it is
        //                      assumed to be a recipe ID, which is saved and signaled
        //                      to the other threads. If a connection error occurs, an
        //                      exception is caught and handled, and the thread exits.
        //                      This function also checks for cancellation requests
        //                      to allow the thread to be stopped cleanly.
        //////////////////////////////////////////////////////////////////////////////
        public void CommunicationRun()
        {
            Thread.Sleep(1000);
            IPAddress ip = IPAddress.Parse(AdressIp);
            int port = Port.FirstOrDefault();

            TcpClient client = new TcpClient();
            NetworkStream stream = null;
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    client.Connect(ip, port);
                    Console.WriteLine("Connectée");
                    stream = client.GetStream();
                    byte[] data = new byte[client.ReceiveBufferSize];
                    int bytesRead = stream.Read(data, 0, client.ReceiveBufferSize);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(data, 0, bytesRead);
                        Console.WriteLine("Message reçu : " + message.Trim('\0'));
                        message = message.Substring(0, message.Length - 1);
                        recipeId = message;
                        newRecipeIdEvent.Set();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une exception a été rencontrée: {ex.Message}");
                    // Signal the other threads that a communication error has occurred
                    communicationErrorEvent.Set();
                    break;
                }
                if (delegatedStopEvent.WaitOne(0, true)) break;
            }
            stream?.Dispose();
            client?.Dispose();
            return;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    // class ResultContainer - This data structure is in charge of passing the result
    //                         values (obtained in the IPthread) to the main thread
    //                         for display.
    ////////////////////////////////////////////////////////////////////////////////
    public class ResultContainer
    {
        public HObject resultImg;
        public HXLD symbolData;
        public HTuple resultHandle;
        public HTuple decodedData;
        public HTuple timeNeeded;
        public HTuple X, Y;
        public HTuple DLResult;
        public ResultContainer()
        {
        }
    }

}

