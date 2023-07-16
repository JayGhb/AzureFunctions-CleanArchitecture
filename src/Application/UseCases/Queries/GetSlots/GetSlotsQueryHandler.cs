using MediatR;
using Microsoft.Extensions.Logging;
using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, string>
    {
        private readonly IDynamicsCrmService _dynamicsCrmService;
        private ILogger<GetSlotsQueryHandler> _logger;

        public GetSlotsQueryHandler(IDynamicsCrmService dynamicsCrmService, ILogger<GetSlotsQueryHandler> logger)
        {
            _dynamicsCrmService = dynamicsCrmService;
            _logger = logger;
        }

        public async Task<string> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("this works lol");
            //await Task.Delay(10000, cancellationToken);
            List<string> mySettings = (await _dynamicsCrmService.GetDataAsync(cancellationToken)).ToList();

            return string.Empty;
        }
    }
}
