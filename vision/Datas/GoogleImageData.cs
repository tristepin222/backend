using Vision.Interfaces;

namespace Vision.Datas
{
    [Serializable]
    public class GoogleImageData : IImageData
    {

        string imageName;
        string[] labels;
        float[] confidences;
        public GoogleImageData(string imageName, string[] labels, float[] confidences)
        {
            this.imageName = imageName;
            this.labels = labels;
            this.confidences = confidences;
        }
        public string[] Labels { get { return labels; } }
        public float[] Confidences { get { return confidences; } }

        public string ImageName { get { return imageName; } }
    }
}
