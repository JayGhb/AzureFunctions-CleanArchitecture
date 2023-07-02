using MediatR;
using Microsoft.Extensions.Logging;
using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, string>
    {
        private readonly IDynamicsCrmService _externalApiService;
        private ILogger<GetSlotsQueryHandler> _logger;
        public GetSlotsQueryHandler(IDynamicsCrmService ruleEngineService, ILogger<GetSlotsQueryHandler> logger)
        {
            _externalApiService = ruleEngineService;
            _logger = logger;
        }

        public async Task<string> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("this works lol");
            //await Task.Delay(10000, cancellationToken);
            List<string> mySettings = (await _externalApiService.GetDataAsync(cancellationToken)).ToList();

            return string.Empty;
        }
    }
}
