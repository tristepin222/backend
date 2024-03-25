using backend.Datas;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Vision.Analysers;
using Vision.Datas;
using Vision.Interfaces;

namespace Gateway.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GatewayController : ControllerBase
    {
        private HttpClient httpClient;
        private GoogleDataObjectImpl googleDataObjectImpl = new GoogleDataObjectImpl();

        [ApiExplorerSettings(IgnoreApi = true)]
        public async void Call()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.example.com/");
            string imageName = "wow";
            var task = googleDataObjectImpl.DoesExists(imageName);
            task.Wait();
            if (!task.Result)
            {
                var taskUpload = googleDataObjectImpl.Upload(imageName, imageName);
                taskUpload.Wait();
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> Publish([FromBody] ImageDataParams imageDataParams)
        {
            return await googleDataObjectImpl.Publish(imageDataParams.RemoteFullPath, imageDataParams.ExpirationTime);
        }
        [HttpPost]
        public async Task<string> Analyze([FromBody] ImageDataParams imageDataParams)
        {
            imageDataParams.MaxLabels = 10;
            imageDataParams.MinConfidenceLevel = 90;

            GoogleLabelDetectorImpl analyser = new GoogleLabelDetectorImpl();
            await Upload(imageDataParams.FormFile);
            string img = await analyser.Analyze(imageDataParams.RemoteFullPath, imageDataParams.MaxLabels, imageDataParams.MinConfidenceLevel);
            await Publish(imageDataParams);
            //SQLDumber.Dumb(img);
            return img;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> Upload(IFormFile formFile)
        {
            string destinationDirectory = "tmp/";
            Directory.CreateDirectory(destinationDirectory);

            string fileName = Path.GetFileName(formFile.FileName);
            string filePath = Path.Combine(destinationDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            string bucketFilePath = "RIA2/" + filePath;

            await googleDataObjectImpl.Upload(filePath, bucketFilePath);
            return bucketFilePath;
        }
    }
}
