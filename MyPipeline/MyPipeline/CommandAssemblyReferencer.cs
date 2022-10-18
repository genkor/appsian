using PipelineModel.API;
using System.Buffers;
using System.Configuration;
using System.Reflection;

namespace MyPipeline
{
    public class AssemblyReferencer
    {
        public AssemblyReferencer(string assemblyName, string className)
        {
            AssemblyName = assemblyName;
            ClassName = className;
            LoadAssembly();
        }
        public string AssemblyName { get; private set; }
        public string ClassName { get; private set; }
        public IExecuter? Executer { get; private set; }

        private void LoadAssembly()
        {
            var asm = Assembly.LoadFile(AssemblyName);
            var type = asm.GetType(ClassName);
            var executer = Activator.CreateInstance(type) as IExecuter;
            if (executer is null)
            {
                throw new ConfigurationErrorsException($"Failed loading assembly {AssemblyName}");
            }
            Executer = executer;
        }
    }

    public class CommandAssemblyReferencer
    {
        private IDictionary<string, AssemblyReferencer> _commandReferencer;

        public CommandAssemblyReferencer()
        {
            _commandReferencer = new Dictionary<string, AssemblyReferencer>();
        }

        public void AddCommandReference(string cmd, AssemblyReferencer referencer)
        {
            _commandReferencer.Add(cmd.ToLower(), referencer);
        }

        public AssemblyReferencer? GetCommandAssembly(string cmd)
        {
            var command = cmd.ToLower();
            if (!_commandReferencer.ContainsKey(command))
                return null;
            return _commandReferencer[command];
        }
    }
}
