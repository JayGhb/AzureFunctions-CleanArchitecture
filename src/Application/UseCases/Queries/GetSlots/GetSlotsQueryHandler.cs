using MediatR;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Application.UseCases.Queries.NonAvailableItems;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, string>
    {
        private readonly IRuleEngineService _ruleEngineService;

        public GetSlotsQueryHandler(IRuleEngineService ruleEngineService)
        {
            _ruleEngineService = ruleEngineService;
        }

        public async Task<string> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            List<string> mySettings = _ruleEngineService.GetSettings().ToList();

            return string.Empty;
        }
    }
}
