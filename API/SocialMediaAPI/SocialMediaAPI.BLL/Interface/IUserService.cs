using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface IUserService
    {
        Task<User> RegisterUser(CreateUserDto user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task<ReturnUserDto> GetLoggedInUser(Guid id);
        Task SetOnline(Guid userId);
        Task SetOffline(Guid authUserId, Guid userId);
        Task<List<ReturnUserDto>> SearchUsersByName(string searchName);
    }
}
