using Net6.Api.DataTransport;
using Net6.Api.Enums;

namespace Net6.Api.Repository.IRepository
{
    public interface IActivity
    {
        Task<ResponseDto<object>> CreateActivity(ActivityDto activity);
        Task<ResponseDto<object>> RescheduleActivity(int idActivity, DateTime reschedule);
        Task<ResponseDto<object>> CancelActivity(int idActivity);
        Task<ResponseDto<ActivityDetailsDto>> ActivityList(DateTime? startSchedule, DateTime? endSchedule, string? estatus);
    }
}

