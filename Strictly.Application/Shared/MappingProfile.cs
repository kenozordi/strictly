using AutoMapper;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using Strictly.Domain.Models.CheckIns.GetCheckIn;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Reminders.CreateReminder;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Streaks.CreateStreak;
using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Streak, GetStreakResponse>()
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency.ToString()))
                .ReverseMap();
            CreateMap<Streak, CreateStreakRequest>()
                .ForMember(dest => dest.FirstCheckInDate, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<CheckIn, CreateCheckInRequest>()
                .ReverseMap();
            CreateMap<CheckIn, GetCheckInForTodayResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.StreakTitle, opt => opt.MapFrom(src => src.Streak.Title))
                .ReverseMap();
            CreateMap<CheckIn, GetCheckInScheduleResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            CreateMap<Reminder, CreateReminderRequest>()
                .ReverseMap();
            CreateMap<Reminder, ReminderNotification>()
                .ForMember(dest => dest.ReminderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StreakTitle, opt => opt.MapFrom(src => src.Streak.Title))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.StreakId, opt => opt.MapFrom(src => src.Streak.Id))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, GetUserResponse>()
                .ReverseMap();
        }
    }
}
