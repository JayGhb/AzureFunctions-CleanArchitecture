using MediatR;
using Microsoft.Extensions.Logging;
using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, string>
    {
        private readonly IYourExternalService _yourExternalService;
        private ILogger<GetSlotsQueryHandler> _logger;

        public GetSlotsQueryHandler(IYourExternalService yourExternalService, ILogger<GetSlotsQueryHandler> logger)
        {
            _yourExternalService = yourExternalService;
            _logger = logger;
        }

        public async Task<string> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("this works lol");
            //await Task.Delay(10000, cancellationToken);
            List<string> mySettings = (await _yourExternalService.GetDataAsync(cancellationToken)).ToList();

            return string.Empty;
        }
    }
}
