using System.Configuration;
using System.Reflection;
using MyPipeline;
using PipelineModel.API;

internal class Program
{
    private static CommandAssemblyReferencer _referencer = new CommandAssemblyReferencer();

    private static void Main(string[] args)
    {
        try
        {
            Initialize();

            if (args.Length < 2)
            {
                PrintUsage();
                return;
            }
            var commands = ReadCommands(args);
            ExecuteCommands(commands);
        } 
        catch (Exception e)
        {
            Console.WriteLine(e);
            PrintUsage();
        }
    }

    private static void Initialize()
    {
        string rootPath = GetAssemblyDirectory();
        var section = ConfigurationManager.GetSection("userSettings/MyPipeline.commands") as ClientSettingsSection;
        if (section is null)
            return;
        foreach (SettingElement item in section.Settings)
        {
            var val = item.Value.ValueXml.InnerText.Trim().Split(":");
            string path = Path.Combine(rootPath, val[0]);
            string name = val[1];
            _referencer.AddCommandReference(item.Name, new AssemblyReferencer(path, name));
        }
    }

    public static string GetAssemblyDirectory()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }

    private static IEnumerable<ICommand> ReadCommands(string[] args)
    {
        var commands = new List<ICommand>();
        if (string.Compare(args[0], MyPipelineConstants.PARAM_COMMAND, StringComparison.OrdinalIgnoreCase) == 0 ||
            string.Compare(args[0], MyPipelineConstants.PARAM_COMMAND_SHORT, StringComparison.OrdinalIgnoreCase) == 0)
        {
            return new List<ICommand>() { PipelineCmdLineReader.Reader.ReadCommand(GetCmdParts(args)) };
        }
        else if (string.Compare(args[0], MyPipelineConstants.PARAM_FILE, StringComparison.OrdinalIgnoreCase) == 0 ||
                 string.Compare(args[0], MyPipelineConstants.PARAM_FILE_SHORT, StringComparison.OrdinalIgnoreCase) == 0)
        {
            string path = args[1];
            string ext = Path.GetExtension(path);
            if (string.Compare(ext, ".txt", true) == 0)
            {
                return PipelineTextReader.Reader.ReadCommands(path);
            }
            if (string.Compare(ext, ".xml", true) == 0)
            {
                return PipelineXmlReader.Reader.ReadCommands(path);
            }
            throw new PipelineException("Invalid commands input file format.");
        }
        else
        {
            throw new PipelineException("Invalid command syntax.");
        }
        return commands;
    }

    private static void ExecuteCommands(IEnumerable<ICommand> commands)
    {
        foreach (var command in commands)
        {
            var reference = _referencer.GetCommandAssembly(command.Name);
            if (reference is null)
            {
                Console.WriteLine($"Command {command.Name} is not registerd.");
                continue;
            }
            reference.Executer?.Execute(commands);
        }
    }

    private static List<string> GetCmdParts(string[] args)
    {
        var parts = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            parts.Add(args[i]);
        }
        return parts;
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:\nMyPipeline [-cmd|-c [command parameters] | -file|-f] [filepath]]");
    }
}