namespace Vision.Interfaces
{
    public interface IImageData
    {
        public string ImageName { get; }
        public string[] Labels { get; }
        public float[] Confidences { get; }
    }
}
