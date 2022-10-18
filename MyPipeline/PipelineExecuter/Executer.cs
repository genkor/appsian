using PipelineModel.API;

namespace PipelineExecuter
{
    public class Executer : IExecuter
    {
        public void Execute(IEnumerable<ICommand> commands)
        {
            foreach(var cmd in commands)
            {
                Execute(cmd);
            }
        }

        private void Execute(ICommand cmd)
        {
            switch (cmd.Name)
            {
                case Constants.CMD_COPY:
                    ExecCopy(cmd.Parameters);
                    break;
                case Constants.CMD_DELETE:
                    ExecDelete(cmd.Parameters);
                    break;
                case Constants.CMD_DOWNLOAD:
                    ExecDownload(cmd.Parameters);
                    break;
                case Constants.CMD_WAIT:
                    ExecWait(cmd.Parameters);
                    break;
                case Constants.CMD_COUNT:
                    ExecCount(cmd.Parameters);
                    break;
            }
        }

        private void ExecCopy(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 2)
            {
                throw new PipelineException("Invalid copy command parameters");
            }
            Console.WriteLine($"Copy from {parameters[0]} to {parameters[1]}");
        }
        private void ExecDelete(IList<string> parameters)
        {
            if (parameters is null || parameters.Count < 1)
            {
                throw new PipelineException("Invalid delete command parameters");
            }
            Console.WriteLine($"Delete file(s) {string.Join(",", parameters)}");
        }
        private void ExecDownload(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 2)
            {
                throw new PipelineException("Invalid download command parameters");
            }
            Console.WriteLine($"Download from {parameters[0]}");
        }
        private void ExecWait(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 2)
            {
                throw new PipelineException("Invalid wait command parameters");
            }
            int seconds = 0;
            if (!int.TryParse(parameters[0], out seconds))
            {
                throw new PipelineException($"Invalid wait parameter {parameters[0]} seconds");
            }
            Console.WriteLine($"Waiting for {parameters[0]} seconds");
            Thread.Sleep(seconds * 1000);
        }
        private void ExecCount(IList<string> parameters)
        {
            if (parameters is null || parameters.Count != 2)
            {
                throw new PipelineException("Invalid copy command parameters");
            }
            Console.WriteLine($"Count line in file {parameters[0]}");
        }
    }
}