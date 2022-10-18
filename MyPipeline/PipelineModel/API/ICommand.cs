namespace PipelineModel.API
{
    public interface ICommand
    {
        public string Name { get; }
        public IList<string> Parameters { get; }
    }
}