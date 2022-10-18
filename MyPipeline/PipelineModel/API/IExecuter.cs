using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineModel.API
{
    public interface IExecuter
    {
        public void Execute(IEnumerable<ICommand> commands);
    }
}
