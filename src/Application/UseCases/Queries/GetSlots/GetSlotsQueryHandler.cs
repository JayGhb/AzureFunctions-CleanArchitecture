using MediatR;
using SlottingMock.Application.Common.Interfaces;

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
            await Task.Delay(10000, cancellationToken);
            List<string> mySettings = _ruleEngineService.GetSettings().ToList();

            return string.Empty;
        }
    }
}
