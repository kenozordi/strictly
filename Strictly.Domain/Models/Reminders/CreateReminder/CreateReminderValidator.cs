using FluentValidation;
using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Reminders.CreateReminder
{
    public class CreateReminderValidator : AbstractValidator<CreateReminderRequest>
    {
        public CreateReminderValidator()
        {
            RuleFor(c => c.Time)
                .Must(t => t >= TimeSpan.Zero && t < TimeSpan.FromDays(1))
                .WithMessage("Time must be a valid time of day.");
            RuleFor(c => c.DayOfWeek)
                .NotEmpty()
                .When(c => c.Frequency == ReminderFrequency.Weekly)
                .LessThanOrEqualTo(7);
            RuleFor(c => c.DayOfMonth)
                .NotEmpty()
                .When(c => c.Frequency == ReminderFrequency.Monthly)
                .LessThanOrEqualTo(31);
            RuleFor(c => c.Message)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
