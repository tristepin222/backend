using Google.Api.Gax;
using System;
using System.Collections.Generic;
using Vision.Analysers;
using Vision.Datas;
using Vision.Interfaces;

namespace Vision
{
    internal class Vision
    {
        /* GoogleDataObjectImpl GoogleDataObjectImpl = new GoogleDataObjectImpl(Environment.GetEnvironmentVariable("BucketName"));
         public async void Call()
         {
             string imageName = Environment.GetEnvironmentVariable("Image");
             var task = GoogleDataObjectImpl.DoesExists(imageName);
             task.Wait();
             if (!task.Result)
             {
                 var taskUpload = GoogleDataObjectImpl.Upload(imageName, imageName);
                 taskUpload.Wait();
             }
         }
         public async Task<string> Publish(string remoteFullPath, int expirationTime = 90)
         {
             return await GoogleDataObjectImpl.Publish(remoteFullPath, expirationTime);
         }
         public async Task<IImageData> Analyze(string remoteFullPath, int maxLabels = 10, int minConfidenceLevel = 90)
         {
             GoogleLabelDetectorImpl analyser = new GoogleLabelDetectorImpl();

             IImageData img = await analyser.Analyze(remoteFullPath, maxLabels, minConfidenceLevel);
             SQLDumber.Dumb(img);
             return img;
         }
         public async void Upload(string localFullPath, string remoteFullPath)
         {
             await GoogleDataObjectImpl.Upload(localFullPath, remoteFullPath);
         }*/
    }
}
