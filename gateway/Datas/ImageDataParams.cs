namespace backend.Datas
{
    public class ImageDataParams
    {
        public IFormFile FormFile { get; set; }
        public string RemoteFullPath { get; set; }
        public string LocalFullPath { get; set; }
        public int MaxLabels { get; set; }
        public int MinConfidenceLevel { get; set; }
        public int ExpirationTime { get; set; }
    }
}
