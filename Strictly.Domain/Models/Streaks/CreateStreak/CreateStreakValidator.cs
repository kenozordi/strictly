using FluentValidation;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Streaks.CreateStreak
{
    public class CreateStreakValidator : AbstractValidator<CreateStreakRequest>
    {
        public CreateStreakValidator()
        {
            // Todo: validate date
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.EndDate).NotEmpty();
        }
    }
}
