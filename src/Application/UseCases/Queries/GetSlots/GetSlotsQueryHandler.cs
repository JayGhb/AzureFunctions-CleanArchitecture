using MediatR;
using Microsoft.Extensions.Logging;
using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, string>
    {
        private readonly IRuleEngineService _ruleEngineService;
        private ILogger<GetSlotsQueryHandler> _logger;
        public GetSlotsQueryHandler(IRuleEngineService ruleEngineService, ILogger<GetSlotsQueryHandler> logger)
        {
            _ruleEngineService = ruleEngineService;
            _logger = logger;
        }

        public async Task<string> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("this works lol");
            //await Task.Delay(10000, cancellationToken);
            List<string> mySettings = _ruleEngineService.GetSettings().ToList();

            return string.Empty;
        }
    }
}
