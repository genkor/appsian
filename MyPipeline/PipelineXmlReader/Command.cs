using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PipelineXmlReader
{
    [XmlRoot("command")]
    public class Command
    {
        [XmlElement("name")]
        public string name { get; set; }
        [XmlArray("parameters")]
        [XmlArrayItem("parameter")]
        public List<string> parameters { get; set; }
        public Command()
        {
            name = string.Empty;
            parameters = new List<string>();
        }
    }

    [XmlRoot("execution")]
    public class Execution
    {
        [XmlArray("commands")]
        [XmlArrayItem("command")]
        public List<Command> commands { get; set; }
        public Execution()
        {
            this.commands = new List<Command>();
        }

    }

}
