namespace PipelineModel.API
{
    public class PipelineException : Exception
    {
        public PipelineException() : base() { }
        public PipelineException(string? message) : base(message) { }
        public PipelineException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
