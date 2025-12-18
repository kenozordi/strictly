using AutoMapper;
using Strictly.Application.Notifications;
using Strictly.Application.Streaks;
using Strictly.Application.Users;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.CheckIns.GetCheckIn;
using Strictly.Domain.Models.Notifications;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Reminders.CreateReminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Reminders
{
    public class ReminderService : IReminderService
    {
        //protected readonly INotificationService _notificationService;
        protected readonly IReminderRepo _reminderRepo;
        protected readonly IStreakRepo _streakRepo;
        protected readonly IUserRepo _userRepo;
        protected readonly IMapper _mapper;

        public ReminderService(IMapper mapper, IReminderRepo reminderRepo,
            IStreakRepo streakRepo, IUserRepo userRepo
            //,INotificationService notificationService
            )
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _streakRepo = streakRepo;
            _reminderRepo = reminderRepo;
            //_notificationService = notificationService;
        }

        public async Task<ServiceResult> CreateReminder(CreateReminderRequest createReminderRequest)
        {
            // validations
            var result = new CreateReminderValidator().Validate(createReminderRequest);
            if (!result.IsValid)
            {
                return ResponseHelper.ToBadRequest(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));
            }

            // validate userId
            var user = await _userRepo.GetUserAsync(createReminderRequest.UserId);
            if (user is null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }
            // validate streakId
            var streak = await _streakRepo.GetStreak(createReminderRequest.StreakId);
            if (streak is null)
            {
                return ResponseHelper.ToBadRequest("Streak does not exist");
            }

            // create reminder
            var affectedRows = await _reminderRepo.CreateReminder(_mapper.Map<Reminder>(createReminderRequest));
            return affectedRows > 0
                ? ResponseHelper.ToSuccess("Reminder created successfully")
                : ResponseHelper.ToUnprocessable("Failed to create Reminder, please try again later!");
        }
        
        public async Task<ServiceResult> SendReminder(Guid reminderId)
        {
            throw new NotImplementedException();
            // get reminder
            //var reminder = await _reminderRepo.GetReminder(reminderId);

            //if (reminder is null)
            //{
            //    return ResponseHelper.ToUnprocessable("Reminder does not exist");
            //}

            //var notificationRequest = new NotificationRequest()
            //{
            //    NotificationType = NotificationType.Email,
            //    To = reminder.User.Email,
            //    Subject = reminder.Streak.Title,
            //    Message = reminder.Message
            //};

            //return await _notificationService.SendAsync(notificationRequest);
        }
        
        public async Task<ServiceResult> GetActiveReminders(ReminderFrequency reminderFrequency)
        {
            switch (reminderFrequency)
            {
                case ReminderFrequency.Daily:
                    return await GetDailyActiveReminders();
                case ReminderFrequency.Monthly:
                    return await GetMonthlyActiveReminders();
                default:
                    throw new NotImplementedException("No implementation for the Reminder Frequency");
            }
        }
        
        private async Task<ServiceResult> GetDailyActiveReminders()
        {
            // get reminders
            var reminders = await _reminderRepo.GetActiveReminders(ReminderFrequency.Daily);

            return reminders.Count > 0
                ? ResponseHelper.ToSuccess(reminders)
                : ResponseHelper.ToEmpty("No Reminder to see here");
        }

        private async Task<ServiceResult> GetMonthlyActiveReminders(bool filterByTime = false)
        {
            // get reminders
            var reminders = await _reminderRepo.GetActiveReminders(ReminderFrequency.Monthly);
            reminders = reminders.Where(r => r.DayOfMonth == DateTime.Today.Day).ToList();
            reminders = filterByTime ? reminders.Where(r 
                => r.Time < DateTime.Now.TimeOfDay.Subtract(TimeSpan.FromMinutes(5)) // buffer of 2.5 minutes before reminder due time
                && r.Time > DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(5))) // buffer of 2.5 minutes after reminder due time
                .ToList() : reminders;

            return reminders.Count > 0
                ? ResponseHelper.ToSuccess(reminders)
                : ResponseHelper.ToEmpty("No Reminder to see here");
        }

    }
}
