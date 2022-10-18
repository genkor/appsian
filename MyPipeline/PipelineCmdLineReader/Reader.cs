using PipelineModel.API;
using PipelineModel.Impl;

namespace PipelineCmdLineReader
{
    public class Reader
    {
        public static ICommand ReadCommand(List<string> parts)
        {
            if (parts.Count <= 0)
            {
                throw new ArgumentException("No command is defined");
            }
            var cmd = new Command(parts[0], parts.Count > 2 ? parts.GetRange(1, parts.Count - 1) : new List<string>());
            return cmd;
        }
    }
}