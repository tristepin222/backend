using System.Text.Json;
using Vision.Interfaces;

namespace Vision.Datas
{
    public class SQLDumber
    {
        public static void Dumb(string img)
        {
            List<string> queries = new List<string>();
            string currentQuery = "";
            string query = "INSERT into Image (Path) VALUES (@imageString)";
            var imgData = JsonSerializer.Deserialize<IImageData>(img);
            currentQuery = query.Replace("imageString", imgData.ImageName.ToString());

            queries.Add(currentQuery);

            query = "INSERT into Labels (Name, Confidence, Image_idImage) VALUES (@Name, @confidence, @idImage)";

            int index = 0;
            foreach (string image in imgData.Labels)
            {
                currentQuery = query;
                currentQuery = currentQuery.Replace("@Name", image);
                currentQuery = currentQuery.Replace("@confidence", imgData.Confidences[index].ToString());
                currentQuery = currentQuery.Replace("@idImage", 21.ToString());
                queries.Add(currentQuery);

                index++;
            }
            File.WriteAllLines("query", queries);
        }
    }
}
