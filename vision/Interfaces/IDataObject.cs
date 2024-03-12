namespace Vision.Interfaces
{
    public interface IDataObject
    {
        public Task<bool> DoesExists(string remoteFullPath);

        public Task Upload(string localFullPath, string remoteFullPath);

        public Task Download(string remoteFullPath, string localFullPath);

        public Task<string> Publish(string remoteFullPath, int expirationTime = 90);

        public Task Remove(string remoteFullPath, bool recursive = false);

    }
}
