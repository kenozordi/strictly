using Strictly.Application.Streaks;
using Strictly.Domain.Models.Constants;
using Strictly.Domain.Models.Entities;
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

        public async Task<(int, dynamic)> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Count() > 0
                ? ((int)HttpStatusCode.OK, ResponseHelper.ToSuccess(users))
                : ((int)HttpStatusCode.NotFound, ResponseHelper.ToEmpty("No users found"));
        }
    }
}
