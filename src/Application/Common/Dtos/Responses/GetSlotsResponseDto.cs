
namespace Application.Common.Dtos.Responses
{
    public class GetSlotsResponseDto
    {
        public List<TimeSlotDto> TimeSlots { get; set; }

        public class TimeSlotDto
        { 
            public string StartTime { get; set; }
            public string EndTime { get; set; }
        }
    }
}
