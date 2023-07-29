using MediatR;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;

namespace Application.UseCases.Queries.GetSlots
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
            List<string> dataFromExternalService = (await _yourExternalService.GetDataAsync(cancellationToken)).ToList();
            
            return string.Empty;
        }
    }
}
