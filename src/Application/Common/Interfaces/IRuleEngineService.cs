using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlottingMock.Application.Common.Interfaces
{
    public interface IRuleEngineService
    {
        IEnumerable<string> GetSettings();
    }
}
