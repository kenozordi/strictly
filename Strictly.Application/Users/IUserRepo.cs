using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Users
{
    public interface IUserRepo
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        Task<User?> GetUserAsync(Guid userGuid);
    }
}
