using MediatR;

namespace SlottingMock.Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQuery : IRequest<string>
    {
        public string QueryPropertyA { get; set; }
        public string QueryPropertyB { get; set;}
    }
}