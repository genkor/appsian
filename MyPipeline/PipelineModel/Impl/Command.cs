using PipelineModel.API;

namespace PipelineModel.Impl
{
    public class Command : ICommand
    {
        public Command(string name, IList<string> parameters)
        {
            Name = name;
            Parameters = parameters;
        }
        public string Name { get; private set; }
        public IList<string> Parameters { get; private set; }
    }
}
