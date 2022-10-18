using Microsoft.VisualBasic;
using PipelineModel.API;

namespace FolderExecuter
{
    public class Executer : IExecuter
    {
        public void Execute(IEnumerable<ICommand> commands)
        {
            foreach (var cmd in commands)
            {
                Execute(cmd);
            }
        }
        private void Execute(ICommand cmd)
        {
            switch (cmd.Name)
            {
                case Constants.CMD_QUERYFOLDER:
                    ExecQueryFoldery(cmd.Parameters);
                    break;
                case Constants.CMD_CREATEFOLDER:
                    ExecCreateFolder(cmd.Parameters);
                    break;
            }
        }

        private void ExecQueryFoldery(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 1)
            {
                throw new PipelineException("Invalid QueryFoldery command parameters");
            }
            Console.WriteLine($"QueryFoldery for {parameters[0]}");
        }
        private void ExecCreateFolder(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 1)
            {
                throw new PipelineException("Invalid CreateFolder command parameters");
            }
            Console.WriteLine($"CreateFolder {parameters[0]}");
        }
    }
}