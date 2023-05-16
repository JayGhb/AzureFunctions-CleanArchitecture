using SlottingMock.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlottingMock.Infrastructure.Services.RuleEngine
{
    public class RuleEngineService : IRuleEngineService
    {
        public IEnumerable<string> GetSettings() 
        { 
            return Enumerable.Empty<string>(); 
        }
    }
}
