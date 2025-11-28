using Strictly.Application.Streaks;
using Strictly.Domain.Models.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Users
{
    public class UserService : IUserService
    {
        protected readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ServiceResult> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Count() > 0
                ? ResponseHelper.ToSuccess(users)
                : ResponseHelper.ToEmpty("No users found");
        }
    }
}
