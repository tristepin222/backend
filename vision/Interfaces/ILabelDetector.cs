namespace Vision.Interfaces
{
    public interface ILabelDetector
    {
        public abstract Task<string> Analyze(string remoteFullPath, int maxLabels = 10, int minConfidenceLevel = 90);
    }
}
