/*****************************************************************************
 * File generated by HDevelop Version 22.11
 *
 * Do not modify!
 *****************************************************************************/

using System;
using System.IO;
using HalconDotNet;


/*
 * If you use this class in your program, you have to 
 * link against hdevenginedotnet.dll and halcondotnet.dll.
 * The Dlls are located in ${HALCONROOT}/bin/dotnet[20|35].
 *
 * The wrapped .hdev or .hdpl files have to be located in the folder
 * that is specified in the static ResourcePath property of 
 * dl_Dataset_pn. 
 * By default, ResourcePath is ${binary_dir}/res_dl_Dataset_pn.
 *
 * It is recommended to compile an assembly from this file using
 * the generated CMakeLists.txt.
 */

namespace dl_Dataset_ns
{
  public static class dl_Dataset_pn
  {

    public static void convert_dl_dataset_ocr_detection_to_recognition(
        HTuple DLDatasetOCRSource,
        out HTuple DLDatasetOCRRecognition)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _convert_dl_dataset_ocr_detection_to_recognition.Value.CreateCall())
      {
        SetParameter(call,"DLDatasetOCRSource",DLDatasetOCRSource);
        call.Execute();
        DLDatasetOCRRecognition = GetParameterHTuple(call,"DLDatasetOCRRecognition");
      }
    }

    public static void find_dl_samples(
        HTuple Samples,
        HTuple KeyName,
        HTuple KeyValue,
        HTuple Mode,
        out HTuple SampleIndices)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _find_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"Samples",Samples);
        SetParameter(call,"KeyName",KeyName);
        SetParameter(call,"KeyValue",KeyValue);
        SetParameter(call,"Mode",Mode);
        call.Execute();
        SampleIndices = GetParameterHTuple(call,"SampleIndices");
      }
    }

    public static void find_invalid_samples_dl_ocr_recognition(
        HTuple Samples,
        HTuple DLModelHandle,
        HTuple GenericParam,
        out HTuple Indices,
        out HTuple Reasons)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _find_invalid_samples_dl_ocr_recognition.Value.CreateCall())
      {
        SetParameter(call,"Samples",Samples);
        SetParameter(call,"DLModelHandle",DLModelHandle);
        SetParameter(call,"GenericParam",GenericParam);
        call.Execute();
        Indices = GetParameterHTuple(call,"Indices");
        Reasons = GetParameterHTuple(call,"Reasons");
      }
    }

    public static void gen_dl_dataset_ocr_recognition_statistics(
        HTuple DLDataset,
        out HTuple CharStats)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _gen_dl_dataset_ocr_recognition_statistics.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        call.Execute();
        CharStats = GetParameterHTuple(call,"CharStats");
      }
    }

    public static void gen_dl_samples(
        HTuple DLDataset,
        HTuple SampleIndices,
        HTuple RestrictKeysDLSample,
        HTuple GenParam,
        out HTuple DLSampleBatch)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _gen_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"SampleIndices",SampleIndices);
        SetParameter(call,"RestrictKeysDLSample",RestrictKeysDLSample);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLSampleBatch = GetParameterHTuple(call,"DLSampleBatch");
      }
    }

    public static void gen_dl_samples_from_images(
        HObject Images,
        out HTuple DLSampleBatch)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _gen_dl_samples_from_images.Value.CreateCall())
      {
        SetParameter(call,"Images",Images);
        call.Execute();
        DLSampleBatch = GetParameterHTuple(call,"DLSampleBatch");
      }
    }

    public static void read_dl_dataset_anomaly(
        HTuple ImageDir,
        HTuple AnomalyDir,
        HTuple ImageList,
        HTuple AnomalyList,
        HTuple GenParam,
        out HTuple DLDataset)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_dataset_anomaly.Value.CreateCall())
      {
        SetParameter(call,"ImageDir",ImageDir);
        SetParameter(call,"AnomalyDir",AnomalyDir);
        SetParameter(call,"ImageList",ImageList);
        SetParameter(call,"AnomalyList",AnomalyList);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLDataset = GetParameterHTuple(call,"DLDataset");
      }
    }

    public static void read_dl_dataset_classification(
        HTuple RawImageFolder,
        HTuple LabelSource,
        out HTuple DLDataset)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_dataset_classification.Value.CreateCall())
      {
        SetParameter(call,"RawImageFolder",RawImageFolder);
        SetParameter(call,"LabelSource",LabelSource);
        call.Execute();
        DLDataset = GetParameterHTuple(call,"DLDataset");
      }
    }

    public static void read_dl_dataset_from_coco(
        HTuple CocoFileName,
        HTuple ImageDir,
        HTuple GenParam,
        out HTuple DLDataset)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_dataset_from_coco.Value.CreateCall())
      {
        SetParameter(call,"CocoFileName",CocoFileName);
        SetParameter(call,"ImageDir",ImageDir);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLDataset = GetParameterHTuple(call,"DLDataset");
      }
    }

    public static void read_dl_dataset_ocr_recognition(
        HTuple FileName,
        HTuple ImageDir,
        HTuple GenParam,
        out HTuple DLDataset)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_dataset_ocr_recognition.Value.CreateCall())
      {
        SetParameter(call,"FileName",FileName);
        SetParameter(call,"ImageDir",ImageDir);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLDataset = GetParameterHTuple(call,"DLDataset");
      }
    }

    public static void read_dl_dataset_segmentation(
        HTuple ImageDir,
        HTuple SegmentationDir,
        HTuple ClassNames,
        HTuple ClassIDs,
        HTuple ImageList,
        HTuple SegmentationList,
        HTuple GenParam,
        out HTuple DLDataset)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_dataset_segmentation.Value.CreateCall())
      {
        SetParameter(call,"ImageDir",ImageDir);
        SetParameter(call,"SegmentationDir",SegmentationDir);
        SetParameter(call,"ClassNames",ClassNames);
        SetParameter(call,"ClassIDs",ClassIDs);
        SetParameter(call,"ImageList",ImageList);
        SetParameter(call,"SegmentationList",SegmentationList);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLDataset = GetParameterHTuple(call,"DLDataset");
      }
    }

    public static void read_dl_samples(
        HTuple DLDataset,
        HTuple SampleIndices,
        out HTuple DLSampleBatch)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _read_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"SampleIndices",SampleIndices);
        call.Execute();
        DLSampleBatch = GetParameterHTuple(call,"DLSampleBatch");
      }
    }

    public static void split_dl_dataset(
        HTuple DLDataset,
        HTuple TrainingPercent,
        HTuple ValidationPercent,
        HTuple GenParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _split_dl_dataset.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"TrainingPercent",TrainingPercent);
        SetParameter(call,"ValidationPercent",ValidationPercent);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
      }
    }

    public static void write_dl_samples(
        HTuple DLDataset,
        HTuple SampleIndices,
        HTuple DLSampleBatch,
        HTuple GenParamName,
        HTuple GenParamValue)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _write_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"SampleIndices",SampleIndices);
        SetParameter(call,"DLSampleBatch",DLSampleBatch);
        SetParameter(call,"GenParamName",GenParamName);
        SetParameter(call,"GenParamValue",GenParamValue);
        call.Execute();
      }
    }


    /****************************************************************************
    * ResourcePath
    *****************************************************************************
    * Use ResourcePath in your application to specify the location of the 
    * HDevelop script or procedure library.
    *****************************************************************************/
    public static string ResourcePath
    {
      get
      {
        return _resource_path;
      }
      set
      {
        lock(_procedure_path_lock)
        {
          _procedure_path_initialized = false;
        }
        _resource_path = value;
      }
    }

#region Implementation details

    /* Implementation details of the wrapper class.
     * You do not have to use these functions ever.
     */

    private static bool _procedure_path_initialized = false;
    private static object _procedure_path_lock = new object();

    private static string _resource_path = "./res_dl_Dataset_pn";

    private static Lazy<HDevProcedure> _convert_dl_dataset_ocr_detection_to_recognition
            = new Lazy<HDevProcedure>(() => new HDevProcedure("convert_dl_dataset_ocr_detection_to_recognition"));
    private static Lazy<HDevProcedure> _find_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("find_dl_samples"));
    private static Lazy<HDevProcedure> _find_invalid_samples_dl_ocr_recognition
            = new Lazy<HDevProcedure>(() => new HDevProcedure("find_invalid_samples_dl_ocr_recognition"));
    private static Lazy<HDevProcedure> _gen_dl_dataset_ocr_recognition_statistics
            = new Lazy<HDevProcedure>(() => new HDevProcedure("gen_dl_dataset_ocr_recognition_statistics"));
    private static Lazy<HDevProcedure> _gen_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("gen_dl_samples"));
    private static Lazy<HDevProcedure> _gen_dl_samples_from_images
            = new Lazy<HDevProcedure>(() => new HDevProcedure("gen_dl_samples_from_images"));
    private static Lazy<HDevProcedure> _read_dl_dataset_anomaly
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_dataset_anomaly"));
    private static Lazy<HDevProcedure> _read_dl_dataset_classification
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_dataset_classification"));
    private static Lazy<HDevProcedure> _read_dl_dataset_from_coco
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_dataset_from_coco"));
    private static Lazy<HDevProcedure> _read_dl_dataset_ocr_recognition
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_dataset_ocr_recognition"));
    private static Lazy<HDevProcedure> _read_dl_dataset_segmentation
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_dataset_segmentation"));
    private static Lazy<HDevProcedure> _read_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("read_dl_samples"));
    private static Lazy<HDevProcedure> _split_dl_dataset
            = new Lazy<HDevProcedure>(() => new HDevProcedure("split_dl_dataset"));
    private static Lazy<HDevProcedure> _write_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("write_dl_samples"));
        
    private static HTuple GetParameterHTuple(HDevProcedureCall call, string name)
    {
      return call.GetOutputCtrlParamTuple(name);
    }

    private static HObject GetParameterHObject(HDevProcedureCall call, string name)
    {
      return call.GetOutputIconicParamObject(name);
    }

    private static HTupleVector GetParameterHTupleVector(HDevProcedureCall call, string name)
    {
      return call.GetOutputCtrlParamVector(name);
    }

    private static HObjectVector GetParameterHObjectVector(HDevProcedureCall call, string name)
    {
      return call.GetOutputIconicParamVector(name);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HTuple tuple)
    {
      call.SetInputCtrlParamTuple(name,tuple);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HObject obj)
    {
      call.SetInputIconicParamObject(name,obj);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HTupleVector vector)
    {
      call.SetInputCtrlParamVector(name,vector);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HObjectVector vector)
    {
      call.SetInputIconicParamVector(name,vector);
    }

    private static void AddResourcePathToProcedurePath() 
    {
      lock(_procedure_path_lock)
      {
        if(!_procedure_path_initialized)
        {
          new HDevEngine().AddProcedurePath(ResourcePath);
          _procedure_path_initialized = true;
        }
      }
    }

#endregion

}
}