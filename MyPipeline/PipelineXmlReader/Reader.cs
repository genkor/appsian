// XML file format:
// 

using PipelineModel.API;
using System.Xml.Serialization;

namespace PipelineXmlReader
{
    public class Reader
    {
        public static IEnumerable<ICommand> ReadCommands(string xmlPath)
        {
            using (TextReader reader = new StreamReader(xmlPath))
            {
                string xml = reader.ReadToEnd();
                var execution = Deserialize(xml);
                return execution.FromExecutionToCommands();
            }
        }

        private static Execution Deserialize(string xml)
        {
            var reader = new System.IO.StringReader(xml);
            var serializer = new XmlSerializer(typeof(Execution));
            return serializer.Deserialize(reader) as Execution;
        }
    }

    public static class ExecutionConverter
    {
        public static IEnumerable<ICommand> FromExecutionToCommands(this Execution execution)
        {
            List<ICommand> commands = new List<ICommand>();
            foreach (var command in execution.commands)
            {
                var cmd = new PipelineModel.Impl.Command(command.name, command.parameters);
                commands.Add(cmd);
            }
            return commands;
        }
    }
}