using IDataObject = Vision.Interfaces.IDataObject;

namespace Vision.Datas
{
    internal class MySQLDataObjectImpl : IDataObject
    {
        public Task<bool> DoesExists(string remoteFullPath)
        {
            throw new NotImplementedException();
        }

        public Task Download(string remoteFullPath, string localFullPath = "")
        {
            throw new NotImplementedException();
        }

        public Task<string> Publish(string remoteFullPath, int expirationTime = 90)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string remoteFullPath, bool recursive = false)
        {
            throw new NotImplementedException();
        }

        public Task Upload(string image, string remoteFullPath)
        {
            throw new NotImplementedException();
        }
    }
}
