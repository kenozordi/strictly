using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns.CreateCheckIn
{
    public class CreateCheckInValidator : AbstractValidator<CreateCheckInRequest>
    {
        public CreateCheckInValidator()
        {
            // Todo: validate date
            RuleFor(c => c.DueDate).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.StreakId).NotEmpty();
        }
    }
}
