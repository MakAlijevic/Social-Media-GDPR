using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }
        public async Task<User> RegisterUser(CreateUserDto user)
        {
            var existingUser = await userRepository.IsEmailInUse(user.Email);
            if (existingUser == false)
            {
                throw new Exception("User with that email already exists.");
            }
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                Email = user.Email,
                CreatedAt = DateTime.Now,
            };

           return await userRepository.RegisterUser(newUser);
            
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User with that email doesn't exist!");
            }

            return user;
        }
    }
}
