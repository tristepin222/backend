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
        [HttpPost]
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
        [HttpPost]
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

            string img = await analyser.Analyze(imageDataParams.RemoteFullPath, imageDataParams.MaxLabels, imageDataParams.MinConfidenceLevel);
            SQLDumber.Dumb(img);
            return "aw";
        }
        [HttpPost]
        public async void Upload([FromBody] ImageDataParams imageDataParams)
        {
            await googleDataObjectImpl.Upload(imageDataParams.LocalFullPath, imageDataParams.RemoteFullPath);
        }
    }
}
