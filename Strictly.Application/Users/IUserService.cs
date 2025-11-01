﻿using Strictly.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Users
{
    public interface IUserService
    {
        Task<(int, string, IEnumerable<User>?)> GetAllAsync();
    }
}
