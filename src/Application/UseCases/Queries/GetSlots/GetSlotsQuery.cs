using MediatR;

namespace Application.UseCases.Queries.GetSlots
{
    public class GetSlotsQuery : IRequest<string>
    {
        /// <summary>
        /// The input date in yyyy-MM-dd format
        /// </summary>
        public string Date { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date">The date in yyyy-MM-dd format</param>
        public GetSlotsQuery(string date)
        {
            Date = date;
        }
    }
}