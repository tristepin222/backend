namespace visionTests
{
    public class ImageData
    {
        public ImageData(string imageName, string[] labels, float[] confidences)
        {
            this.ImageName = imageName;
            this.Labels = labels;
            this.Confidences = confidences;
        }
        public string ImageName { get; set; }
        public string[] Labels { get; set; }
        public float[] Confidences { get; set; }
    }
}