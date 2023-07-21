//
// HALCON/.NET (C#) multithreading example
//
// © 2007-2022 MVTec Software GmbH
//
// Purpose:
// This example program shows how to perform image acquisition, image
// processing, and image display in parallel by using two threads (besides
// the main thread), one for each task. The first thread grabs images, the
// second one performs image processing tasks, and the main thread is in
// charge of the HALCON window - it displays the image processed last and
// its results.
//
// MultiThreadingForm.cs: Defines the behavior of the application's GUI.
//


using HalconDotNet;
using System.Collections.Concurrent;

namespace MultiThreading
{
    /// <summary>
    /// Summary description for MultiThreadExpDlg.
    /// </summary>

    public class MultiThreadingForm : Form
    {
        private Button startButton;
        private Button stopButton;
        public Label procTimeLabel;
        public Label imageDataLabel;
        private System.Windows.Forms.Timer closingTimer;
        private System.ComponentModel.IContainer components;
        public WorkerThread workerObject;

        public ManualResetEvent stopEventHandle;
        public Thread threadAcq, threadIP, threadCom;
        private Label LabelPT;
        private Label CopyrightLabel;
        private HSmartWindowControl WindowControl;
        private CheckBox checkboxInputFromFile;
        private Label label1;
        private Label label2;
        public Label XCordLabel;
        public Label YCordLabel;
        private Label MinConfianceLabel;
        private TrackBar trackBar1;
        private Label LabelID;

        public MultiThreadingForm()
        {
            // Required for Windows Form Designer support.
            InitializeComponent();
            // Set up the shutdown timer.
            closingTimer = new System.Windows.Forms.Timer();
            closingTimer.Tick += ForceApplicationClosing;
            closingTimer.Interval = 100;

            // Set up eventhandle and instance of WorkerThread class, which
            // contains thread functions.
            stopEventHandle = new ManualResetEvent(false);
            workerObject = new WorkerThread(this);
        }

        private void MultiThreadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // All handles used to synchronize the threads need to be set
            // to their initial states.
            stopEventHandle.Set();
            if (threadAcq != null && threadAcq.IsAlive)
            {
                e.Cancel = true;
                workerObject.RequestStop();
            }
            if (threadIP != null && threadIP.IsAlive)
            {
                e.Cancel = true;
                workerObject.RequestStop();
            }
            if (threadCom != null && threadCom.IsAlive)  // new code for communication thread
            {
                e.Cancel = true;
                workerObject.RequestStop();
            }
            closingTimer.Start();
        }

        private void ForceApplicationClosing(object sender, EventArgs e)
        {
            closingTimer.Stop();
            Application.Exit();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            stopButton = new Button();
            LabelPT = new Label();
            LabelID = new Label();
            procTimeLabel = new Label();
            imageDataLabel = new Label();
            CopyrightLabel = new Label();
            WindowControl = new HSmartWindowControl();
            checkboxInputFromFile = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            XCordLabel = new Label();
            YCordLabel = new Label();
            MinConfianceLabel = new Label();
            trackBar1 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.BackColor = Color.LightGreen;
            startButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            startButton.Location = new Point(690, 11);
            startButton.Margin = new Padding(4, 3, 4, 3);
            startButton.Name = "startButton";
            startButton.Size = new Size(234, 72);
            startButton.TabIndex = 1;
            startButton.Tag = "";
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.BackColor = Color.DarkRed;
            stopButton.Enabled = false;
            stopButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            stopButton.ForeColor = SystemColors.ControlLightLight;
            stopButton.Location = new Point(690, 446);
            stopButton.Margin = new Padding(4, 3, 4, 3);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(234, 115);
            stopButton.TabIndex = 2;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = false;
            stopButton.Click += stopButton_Click;
            // 
            // LabelPT
            // 
            LabelPT.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LabelPT.Location = new Point(14, 580);
            LabelPT.Margin = new Padding(4, 0, 4, 0);
            LabelPT.Name = "LabelPT";
            LabelPT.Size = new Size(131, 28);
            LabelPT.TabIndex = 3;
            LabelPT.Text = "Processing time:";
            LabelPT.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelID
            // 
            LabelID.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LabelID.Location = new Point(14, 617);
            LabelID.Margin = new Padding(4, 0, 4, 0);
            LabelID.Name = "LabelID";
            LabelID.Size = new Size(121, 28);
            LabelID.TabIndex = 4;
            LabelID.Text = "Nombre de pièce";
            LabelID.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // procTimeLabel
            // 
            procTimeLabel.Location = new Point(143, 580);
            procTimeLabel.Margin = new Padding(4, 0, 4, 0);
            procTimeLabel.Name = "procTimeLabel";
            procTimeLabel.Size = new Size(84, 28);
            procTimeLabel.TabIndex = 5;
            procTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // imageDataLabel
            // 
            imageDataLabel.Location = new Point(143, 617);
            imageDataLabel.Margin = new Padding(4, 0, 4, 0);
            imageDataLabel.Name = "imageDataLabel";
            imageDataLabel.Size = new Size(84, 28);
            imageDataLabel.TabIndex = 6;
            imageDataLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CopyrightLabel
            // 
            CopyrightLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            CopyrightLabel.Location = new Point(246, 672);
            CopyrightLabel.Margin = new Padding(4, 0, 4, 0);
            CopyrightLabel.Name = "CopyrightLabel";
            CopyrightLabel.Size = new Size(537, 18);
            CopyrightLabel.TabIndex = 30;
            CopyrightLabel.Text = "© 2022-2023 Actemium Rouen Automation";
            CopyrightLabel.TextAlign = ContentAlignment.MiddleCenter;
            CopyrightLabel.Click += CopyrightLabel_Click;
            // 
            // WindowControl
            // 
            WindowControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            WindowControl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            WindowControl.HDoubleClickToFitContent = true;
            WindowControl.HDrawingObjectsModifier = HSmartWindowControl.DrawingObjectsModifier.None;
            WindowControl.HImagePart = new Rectangle(-327, -307, 1294, 1094);
            WindowControl.HKeepAspectRatio = true;
            WindowControl.HMoveContent = true;
            WindowControl.HZoomContent = HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            WindowControl.Location = new Point(11, 11);
            WindowControl.Margin = new Padding(2);
            WindowControl.Name = "WindowControl";
            WindowControl.Size = new Size(650, 550);
            WindowControl.TabIndex = 31;
            WindowControl.WindowSize = new Size(650, 550);
            WindowControl.Load += WindowControl_Load;
            // 
            // checkboxInputFromFile
            // 
            checkboxInputFromFile.AutoSize = true;
            checkboxInputFromFile.Location = new Point(754, 89);
            checkboxInputFromFile.Name = "checkboxInputFromFile";
            checkboxInputFromFile.Size = new Size(105, 19);
            checkboxInputFromFile.TabIndex = 32;
            checkboxInputFromFile.Text = "Input from file ";
            checkboxInputFromFile.UseVisualStyleBackColor = true;
            checkboxInputFromFile.CheckedChanged += File_CheckedChanged;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(264, 580);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(131, 28);
            label1.TabIndex = 33;
            label1.Text = "X coordinates:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(264, 617);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(131, 28);
            label2.TabIndex = 34;
            label2.Text = "Y coordinates:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // XCordLabel
            // 
            XCordLabel.Location = new Point(367, 580);
            XCordLabel.Margin = new Padding(4, 0, 4, 0);
            XCordLabel.Name = "XCordLabel";
            XCordLabel.Size = new Size(558, 28);
            XCordLabel.TabIndex = 35;
            XCordLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // YCordLabel
            // 
            YCordLabel.Location = new Point(367, 617);
            YCordLabel.Margin = new Padding(4, 0, 4, 0);
            YCordLabel.Name = "YCordLabel";
            YCordLabel.Size = new Size(558, 28);
            YCordLabel.TabIndex = 36;
            YCordLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MinConfianceLabel
            // 
            MinConfianceLabel.Location = new Point(736, 162);
            MinConfianceLabel.Margin = new Padding(4, 0, 4, 0);
            MinConfianceLabel.Name = "MinConfianceLabel";
            MinConfianceLabel.Size = new Size(155, 28);
            MinConfianceLabel.TabIndex = 37;
            MinConfianceLabel.Text = "Min Confiance:";
            MinConfianceLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(690, 193);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 50;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(234, 45);
            trackBar1.TabIndex = 0;
            trackBar1.Value = 90;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // MultiThreadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(956, 699);
            Controls.Add(trackBar1);
            Controls.Add(MinConfianceLabel);
            Controls.Add(YCordLabel);
            Controls.Add(XCordLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(checkboxInputFromFile);
            Controls.Add(WindowControl);
            Controls.Add(CopyrightLabel);
            Controls.Add(imageDataLabel);
            Controls.Add(procTimeLabel);
            Controls.Add(LabelID);
            Controls.Add(LabelPT);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MultiThreadingForm";
            Text = "Performing Image Acquisition, Processing, and Display in Multiple Threads";
            FormClosing += MultiThreadingForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MultiThreadingForm());
        }

        ////////////////////////////////////////////////////////////////////////////
        // startButton_Click - Every click on the Start button will create new
        //                     instances of the Thread class - threadFG handles the
        //                     image acquisition, whereas threadIP performs the image
        //                     processing. Since Start and Stop button can be
        //                     clicked consecutively, all handles used to synchronize
        //                     the threads need to be set/reset to their initial
        //                     states. Handles that are members of the WorkerThread
        //                     class are reset by calling the init() method. Last but
        //                     not least we need to call Thread.Start() to "start"
        //                     the thread functions.
        ////////////////////////////////////////////////////////////////////////////
        private void startButton_Click(object sender, EventArgs e)
        {
            stopEventHandle.Reset();

            threadAcq = new Thread(new ThreadStart(workerObject.ImgAcqRun));
            threadIP = new Thread(new ThreadStart(workerObject.IPRun));
            threadCom = new Thread(new ThreadStart(workerObject.CommunicationRun));  // new thread for communication

            startButton.Enabled = false;
            stopButton.Enabled = true;

            workerObject.Init();

            threadAcq.Start();  // start the threads
            threadIP.Start();
            threadCom.Start();
        }

        ////////////////////////////////////////////////////////////////////////////
        // stopButton_Click - Once the Stop button is clicked, the stopEventHandle
        //                    is "turned on" - to signal the two threads to terminate.
        ////////////////////////////////////////////////////////////////////////////
        private void stopButton_Click(object sender, EventArgs e)
        {
            stopEventHandle.Set();
        }

        ////////////////////////////////////////////////////////////////////////////
        public HWindow GetHalconWindow()
        {
            return WindowControl.HalconWindow;
        }

        ////////////////////////////////////////////////////////////////////////////
        // ResetControls - We used the means of delegates to prevent deadlocks
        //                 between the main thread and the IPthread. The delegate is
        //                 called to "clean" the display and the labels.
        ////////////////////////////////////////////////////////////////////////////
        public void ResetControls()
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;

            components = null;
        }

        private void WindowControl_Load(object sender, EventArgs e)
        {

        }

        public void File_CheckedChanged(object sender, EventArgs e)
        {

        }
        public bool IsCheckboxChecked()
        {
            return checkboxInputFromFile.Checked;
        }

        private void CopyrightLabel_Click(object sender, EventArgs e)
        {

        }
        // Dans votre formulaire, ajoutez un ConcurrentQueue pour stocker les valeurs de la barre de suivi
        public ConcurrentQueue<double> TrackBarValues { get; } = new ConcurrentQueue<double>();
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Get the value from the track bar and scale it
            double value = trackBar1.Value / 100.0;

            // Clear the queue
            while (TrackBarValues.TryDequeue(out _)) { }
            // Add the value to the queue
            TrackBarValues.Enqueue(value);

            // Update the label text
            MinConfianceLabel.Text = "Min confiance :" + value.ToString("0.00"); // Format the value to show two decimal places
        }
    }

}