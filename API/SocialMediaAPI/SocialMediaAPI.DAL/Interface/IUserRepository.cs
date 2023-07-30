using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<bool> IsEmailInUse(string email);
        Task<User> GetUserByEmail(string email);
    }
}
