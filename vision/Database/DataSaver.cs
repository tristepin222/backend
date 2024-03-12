using Vision.Interfaces;

namespace Vision.Database
{
    public class DataSaver
    {
        public static void SaveImage(IImageData imageData)
        {
            SqlConnector.sqlConnection.Open();
        }
    }
}
