using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6.Api.DataTransport;
using Net6.Api.Enums;
using Net6.Api.Repository.IRepository;

namespace Net6.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {     
        private readonly ILogger<ActivityController> _logger;
        private readonly IActivity _activity;

        public ActivityController(ILogger<ActivityController> logger, IActivity activity)
        {
            _logger = logger;
            _activity = activity;
        }

        [HttpPost("RegistraActividad")]
        public async Task<ResponseDto<object>> RegistraActividad([FromBody] ActivityDto activity)
        {
            return await _activity.CreateActivity(activity);
          
        }

        [HttpPut("ReagendarActividad/{idActivity}/{reschedule}")]
        public async Task<ResponseDto<object>> ReagendarActividad(int idActivity, DateTime reschedule)
        {
            return await _activity.RescheduleActivity(idActivity, reschedule);
        }

        [HttpPut("CancelarActividad/{idActivity}")]
        public async Task<ResponseDto<object>> CancelarActividad(int idActivity)
        {
            return await _activity.CancelActivity(idActivity);
        }

        [HttpGet("ListadoActividades")]
        public async Task<ResponseDto<ActivityDetailsDto>> ActivityList(DateTime? startSchedule, DateTime? endSchedule, string? estatus)
        {
            return await _activity.ActivityList(startSchedule, endSchedule, estatus);
        }
    }
}