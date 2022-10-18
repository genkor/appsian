using PipelineModel.API;
using PipelineModel.Impl;

namespace PipelineTextReader
{
    public class Reader
    {
        public static IEnumerable<ICommand> ReadCommands(string textFile)
        {
            List<ICommand> commands = new List<ICommand>();
            using (StreamReader file = new StreamReader(textFile))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    if (line.StartsWith("#"))
                        continue;
                    commands.Add(ReadCommand(line));
                }
            }
            return commands;
        }

        private static ICommand ReadCommand(string line)
        {
            var parts = line.Split(',').ToList();
            var cmd = new Command(parts[0].Trim(), parts.Count > 1 ? parts.GetRange(1, parts.Count - 1) : new List<string>());
            return cmd;
        }
    }
}