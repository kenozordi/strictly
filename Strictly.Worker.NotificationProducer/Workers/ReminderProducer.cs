using AutoMapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Reminders;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Shared;
using Strictly.Worker.NotificationProducer.Constants;
using Strictly.Worker.NotificationProducer.Enums;
using System.Collections.Generic;

namespace Strictly.Worker.NotificationProducer.Workers
{
    public class ReminderProducer : BackgroundService
    {
        private readonly ILogger<ReminderProducer> _logger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly IReminderService _reminderService;
        private readonly IReminderRepo _reminderRepo;
        private readonly IDatabase _producerDatabase;
        private readonly IMapper _mapper;

        public ReminderProducer(ILogger<ReminderProducer> logger,
            IConnectionMultiplexer redisCache, IReminderRepo reminderRepo,
            IMapper mapper, IReminderService reminderService)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _reminderRepo = reminderRepo;
            _reminderService = reminderService;
            _producerDatabase = _redisCache.GetDatabase();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.WhenAll([
                    ProduceDailyReminders(),
                    ProduceMonthlyReminders(),
                    ]);

                // Delay
                await Task.Delay(5000, stoppingToken);
            }
        }

        protected async Task ProduceDailyReminders()
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Reminder Producer running at: {time}", DateTimeOffset.Now);
                }

                var remindersResponse = await _reminderService.GetActiveReminders(ReminderFrequency.Daily);
                if (!remindersResponse.IsSuccess)
                {
                    return;
                }

                List<Reminder> reminders = (List<Reminder>)((BaseResponse<object>)remindersResponse.Data).Data;
                if (reminders is null || reminders.Count < 1)
                {
                    return;
                }

                foreach (var reminder in reminders)
                {
                    var clients = await _producerDatabase.PublishAsync(
                        $"{CacheKey.Notifications}:{NotificationEvent.Reminder.ToString()}",
                        JsonConvert.SerializeObject(_mapper.Map<ReminderNotification>(reminder)));

                    reminder.UpdatedAt = DateTime.Now;
                    await _reminderRepo.UpdateReminder(reminder);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        protected async Task ProduceMonthlyReminders()
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Monthly Reminder Producer running at: {time}", DateTimeOffset.Now);
                }

                var remindersResponse = await _reminderService.GetActiveReminders(ReminderFrequency.Monthly);
                if (!remindersResponse.IsSuccess)
                {
                    return;
                }

                List<Reminder> reminders = (List<Reminder>)((BaseResponse<object>)remindersResponse.Data).Data;
                if (reminders is null || reminders.Count < 1)
                {
                    return;
                }

                foreach (var reminder in reminders)
                {
                    var clients = await _producerDatabase.PublishAsync(
                        $"{CacheKey.Notifications}:{NotificationEvent.Reminder.ToString()}",
                        JsonConvert.SerializeObject(_mapper.Map<ReminderNotification>(reminder)));

                    reminder.UpdatedAt = DateTime.Now;
                    await _reminderRepo.UpdateReminder(reminder);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
