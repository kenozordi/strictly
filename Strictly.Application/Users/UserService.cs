using AutoMapper;
using Strictly.Domain.Models.Shared.Constants;
using Strictly.Domain.Models.Users;
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
        protected readonly IMapper _mapper;
        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Count() > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<GetUserResponse>>(users))
                : ResponseHelper.ToEmpty("No users found");
        }
    }
}
