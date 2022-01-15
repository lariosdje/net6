using Net6.Api.Enums;

namespace Net6.Api.DataTransport
{
    public class FilterDto
    {
        public DateTime? startSchedule { get; set; }
        public DateTime? endSchedule { get; set; }
        public EstatusEnum estatus { get; set; }
    }
}
