using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net6.Api.DataContext;
using Net6.Api.DataTransport;
using Net6.Api.Enums;
using Net6.Api.Repository.IRepository;

namespace Net6.Api.Repository
{
    public class ActivityRepository : IActivity
    {
        private readonly Net6Context _context;
        public readonly IMapper _mapper;

        public ActivityRepository(Net6Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<object>> CreateActivity(ActivityDto activity)
        {
            var response = new ResponseDto<object>();

            try
            {
               var property =  await _context.Property.FindAsync(activity.PropertyId);

                if(property == null)
                {   
                    throw new Exception("La propiedad no se encuentra");
                }

                if(property.Status == EstatusEnum.DISABLED)
                {
                    throw new Exception("La propiedad esta desabilitada");
                }

                var activitys = _context.Activity.Where(x => x.PropertyId == activity.PropertyId).ToList();

                if (activitys.Any(x => x.Schedule.AddHours(1) >= activity.Schedule && x.Schedule <= activity.Schedule))
                {
                    throw new Exception("Ya existe una actividad en este horario");
                }

              
                var entity = _mapper.Map<Activity>(activity);
                entity.Status = EstatusEnum.ACTIVE;

                await _context.Activity.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<object>> RescheduleActivity(int idActivity, DateTime reschedule)
        {
            var response = new ResponseDto<object>();

            try
            {
                var activity = await _context.Activity.FindAsync(idActivity);

                if (activity == null)
                {
                    throw new Exception("La actividad no se encuentra");
                }

                if (activity.Status == EstatusEnum.CANCEL)
                {
                    throw new Exception("La actividad esta cancelada");
                }
                   
                activity.Schedule = reschedule;
                _context.Entry(activity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<object>> CancelActivity(int idActivity)
        {
            var response = new ResponseDto<object>();

            try
            {
                var activity = await _context.Activity.FindAsync(idActivity);

                if (activity == null)
                {
                    throw new Exception("La actividad no se encuentra");
                }

                activity.Status = EstatusEnum.CANCEL;
                _context.Entry(activity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<ActivityDetailsDto>> ActivityList(DateTime? startSchedule, DateTime? endSchedule, string? estatus)
        {
            var response = new ResponseDto<ActivityDetailsDto>();

            try
            {
                var activitys = await _context.Activity
                                        .Include(a => a.Surveys)
                                        .Include(b => b.Property)                                            
                                        .Select(x => new ActivityDetailsDto
                                        {
                                            Id = x.Id,
                                            Schedule = x.Schedule,
                                            Title = x.Title,
                                            CreatedAt = x.CreatedAt,
                                            Status = StatusActivity(x.Status, x.Schedule),
                                            Surveys = _mapper.Map<List<SurveyDto>>(x.Surveys),
                                            Property = _mapper.Map<PropertyDto>(x.Property)
                                        })
                                        .ToListAsync();

                if (!string.IsNullOrEmpty(estatus))
                {
                    activitys = activitys.Where(x => x.Status == estatus).ToList();
                }

                if (startSchedule != null)
                {
                    activitys = activitys.Where(x=> x.Schedule >= startSchedule).ToList();
                }

                if(endSchedule != null)
                {
                    activitys = activitys.Where(x => x.Schedule <= startSchedule).ToList();
                }

                response.ListResponse = _mapper.Map<List<ActivityDetailsDto>>(activitys);
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static string StatusActivity(string status, DateTime schedule)
        {
            if(status == EstatusEnum.ACTIVE && schedule.Date >= DateTime.Now.Date)
            {
                return EstatusEnum.PENDING;
            }

            if (status == EstatusEnum.ACTIVE && schedule.Date < DateTime.Now.Date)
            {
                return EstatusEnum.LATE_PAYMENT;
            }

            if (status == EstatusEnum.DONE)
            {
                return EstatusEnum.DONE;
            }

            return string.Empty;
        }
    }
}
