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
 * dl_preprocessing_pn. 
 * By default, ResourcePath is ${binary_dir}/res_dl_preprocessing_pn.
 *
 * It is recommended to compile an assembly from this file using
 * the generated CMakeLists.txt.
 */

namespace dl_preprocessing_ns
{
  public static class dl_preprocessing_pn
  {

    public static void augment_dl_samples(
        HTuple DLSampleBatch,
        HTuple GenParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _augment_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"DLSampleBatch",DLSampleBatch);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
      }
    }

    public static void calculate_dl_segmentation_class_weights(
        HTuple DLDataset,
        HTuple MaxWeight,
        HTuple IgnoreClassIDs,
        out HTuple ClassWeights)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _calculate_dl_segmentation_class_weights.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"MaxWeight",MaxWeight);
        SetParameter(call,"IgnoreClassIDs",IgnoreClassIDs);
        call.Execute();
        ClassWeights = GetParameterHTuple(call,"ClassWeights");
      }
    }

    public static void create_dl_preprocess_param(
        HTuple DLModelType,
        HTuple ImageWidth,
        HTuple ImageHeight,
        HTuple ImageNumChannels,
        HTuple ImageRangeMin,
        HTuple ImageRangeMax,
        HTuple NormalizationType,
        HTuple DomainHandling,
        HTuple IgnoreClassIDs,
        HTuple SetBackgroundID,
        HTuple ClassIDsBackground,
        HTuple GenParam,
        out HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _create_dl_preprocess_param.Value.CreateCall())
      {
        SetParameter(call,"DLModelType",DLModelType);
        SetParameter(call,"ImageWidth",ImageWidth);
        SetParameter(call,"ImageHeight",ImageHeight);
        SetParameter(call,"ImageNumChannels",ImageNumChannels);
        SetParameter(call,"ImageRangeMin",ImageRangeMin);
        SetParameter(call,"ImageRangeMax",ImageRangeMax);
        SetParameter(call,"NormalizationType",NormalizationType);
        SetParameter(call,"DomainHandling",DomainHandling);
        SetParameter(call,"IgnoreClassIDs",IgnoreClassIDs);
        SetParameter(call,"SetBackgroundID",SetBackgroundID);
        SetParameter(call,"ClassIDsBackground",ClassIDsBackground);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLPreprocessParam = GetParameterHTuple(call,"DLPreprocessParam");
      }
    }

    public static void create_dl_preprocess_param_from_model(
        HTuple DLModelHandle,
        HTuple NormalizationType,
        HTuple DomainHandling,
        HTuple SetBackgroundID,
        HTuple ClassIDsBackground,
        HTuple GenParam,
        out HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _create_dl_preprocess_param_from_model.Value.CreateCall())
      {
        SetParameter(call,"DLModelHandle",DLModelHandle);
        SetParameter(call,"NormalizationType",NormalizationType);
        SetParameter(call,"DomainHandling",DomainHandling);
        SetParameter(call,"SetBackgroundID",SetBackgroundID);
        SetParameter(call,"ClassIDsBackground",ClassIDsBackground);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLPreprocessParam = GetParameterHTuple(call,"DLPreprocessParam");
      }
    }

    public static void gen_dl_samples_3d_gripping_point_detection(
        HObject Images,
        HObject X,
        HObject Y,
        HObject Z,
        HObject Normals,
        out HTuple DLSampleBatch)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _gen_dl_samples_3d_gripping_point_detection.Value.CreateCall())
      {
        SetParameter(call,"Images",Images);
        SetParameter(call,"X",X);
        SetParameter(call,"Y",Y);
        SetParameter(call,"Z",Z);
        SetParameter(call,"Normals",Normals);
        call.Execute();
        DLSampleBatch = GetParameterHTuple(call,"DLSampleBatch");
      }
    }

    public static void gen_dl_segmentation_weight_images(
        HTuple DLDataset,
        HTuple DLPreprocessParam,
        HTuple ClassWeights,
        HTuple GenParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _gen_dl_segmentation_weight_images.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        SetParameter(call,"ClassWeights",ClassWeights);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
      }
    }

    public static void preprocess_dl_dataset(
        HTuple DLDataset,
        HTuple DataDirectory,
        HTuple DLPreprocessParam,
        HTuple GenParam,
        out HTuple DLDatasetFileName)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_dataset.Value.CreateCall())
      {
        SetParameter(call,"DLDataset",DLDataset);
        SetParameter(call,"DataDirectory",DataDirectory);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        SetParameter(call,"GenParam",GenParam);
        call.Execute();
        DLDatasetFileName = GetParameterHTuple(call,"DLDatasetFileName");
      }
    }

    public static void preprocess_dl_model_3d_data(
        HTuple DLSample,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_3d_data.Value.CreateCall())
      {
        SetParameter(call,"DLSample",DLSample);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
      }
    }

    public static void preprocess_dl_model_anomaly(
        HObject AnomalyImages,
        out HObject AnomalyImagesPreprocessed,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_anomaly.Value.CreateCall())
      {
        SetParameter(call,"AnomalyImages",AnomalyImages);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
        AnomalyImagesPreprocessed = GetParameterHObject(call,"AnomalyImagesPreprocessed");
      }
    }

    public static void preprocess_dl_model_augmentation_data(
        HTuple DLSample,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_augmentation_data.Value.CreateCall())
      {
        SetParameter(call,"DLSample",DLSample);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
      }
    }

    public static void preprocess_dl_model_images(
        HObject Images,
        out HObject ImagesPreprocessed,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_images.Value.CreateCall())
      {
        SetParameter(call,"Images",Images);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
        ImagesPreprocessed = GetParameterHObject(call,"ImagesPreprocessed");
      }
    }

    public static void preprocess_dl_model_images_ocr_recognition(
        HObject Images,
        out HObject ImagesPreprocessed,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_images_ocr_recognition.Value.CreateCall())
      {
        SetParameter(call,"Images",Images);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
        ImagesPreprocessed = GetParameterHObject(call,"ImagesPreprocessed");
      }
    }

    public static void preprocess_dl_model_segmentations(
        HObject ImagesRaw,
        HObject Segmentations,
        out HObject SegmentationsPreprocessed,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_model_segmentations.Value.CreateCall())
      {
        SetParameter(call,"ImagesRaw",ImagesRaw);
        SetParameter(call,"Segmentations",Segmentations);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
        call.Execute();
        SegmentationsPreprocessed = GetParameterHObject(call,"SegmentationsPreprocessed");
      }
    }

    public static void preprocess_dl_samples(
        HTuple DLSampleBatch,
        HTuple DLPreprocessParam)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _preprocess_dl_samples.Value.CreateCall())
      {
        SetParameter(call,"DLSampleBatch",DLSampleBatch);
        SetParameter(call,"DLPreprocessParam",DLPreprocessParam);
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

    private static string _resource_path = "./res_dl_preprocessing_pn";

    private static Lazy<HDevProcedure> _augment_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("augment_dl_samples"));
    private static Lazy<HDevProcedure> _calculate_dl_segmentation_class_weights
            = new Lazy<HDevProcedure>(() => new HDevProcedure("calculate_dl_segmentation_class_weights"));
    private static Lazy<HDevProcedure> _create_dl_preprocess_param
            = new Lazy<HDevProcedure>(() => new HDevProcedure("create_dl_preprocess_param"));
    private static Lazy<HDevProcedure> _create_dl_preprocess_param_from_model
            = new Lazy<HDevProcedure>(() => new HDevProcedure("create_dl_preprocess_param_from_model"));
    private static Lazy<HDevProcedure> _gen_dl_samples_3d_gripping_point_detection
            = new Lazy<HDevProcedure>(() => new HDevProcedure("gen_dl_samples_3d_gripping_point_detection"));
    private static Lazy<HDevProcedure> _gen_dl_segmentation_weight_images
            = new Lazy<HDevProcedure>(() => new HDevProcedure("gen_dl_segmentation_weight_images"));
    private static Lazy<HDevProcedure> _preprocess_dl_dataset
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_dataset"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_3d_data
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_3d_data"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_anomaly
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_anomaly"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_augmentation_data
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_augmentation_data"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_images
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_images"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_images_ocr_recognition
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_images_ocr_recognition"));
    private static Lazy<HDevProcedure> _preprocess_dl_model_segmentations
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_model_segmentations"));
    private static Lazy<HDevProcedure> _preprocess_dl_samples
            = new Lazy<HDevProcedure>(() => new HDevProcedure("preprocess_dl_samples"));
        
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