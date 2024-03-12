using Google.Cloud.Vision.V1;
using System.Text.Json;
using Vision.Interfaces;
using Vision.Datas;

namespace Vision.Analysers
{
    public class GoogleLabelDetectorImpl : ILabelDetector
    {
        ImageAnnotatorClient client;
        public GoogleLabelDetectorImpl()
        {
            client = ImageAnnotatorClient.Create();
        }
        public async Task<string> Analyze(string remoteFullPath, int maxLabels = 10, int minConfidenceLevel = 90)
        {
            List<string> labels = new List<string>();
            List<float> confidences = new List<float>();
            Image image = Image.FromFile(remoteFullPath);
            IReadOnlyList<EntityAnnotation> results = await client.DetectLabelsAsync(image, maxResults: maxLabels);
            int index = 0;
            foreach (EntityAnnotation result in results)
            {
                if (result.Score * 100 >= minConfidenceLevel && index <= maxLabels)
                {
                    confidences.Add(result.Score * 100);
                    labels.Add(result.Description);
                }
                index++;
            }
            var imageResult = new GoogleImageData(remoteFullPath, labels.ToArray(), confidences.ToArray());
            return JsonSerializer.Serialize(imageResult);
        }
    }
}
