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
        private IPolicyService policyService;
        private IFollowService followService;
        public UserService(IUserRepository userRepository, IPolicyService policyService, IFollowService followService) 
        {
            this.userRepository = userRepository;
            this.policyService = policyService;
            this.followService = followService;
        }
        public async Task<User> RegisterUser(CreateUserDto user)
        {
            try
            {
                var policy = await policyService.GetPolicyById(user.PolicyId);
            }
            catch(Exception)
            {
                throw new Exception("User can't be created without accepting the policy.");
            }

            var existingUser = await userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
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

            var registeredUser = await userRepository.RegisterUser(newUser);
            if(registeredUser == null) 
            {
                throw new Exception("User not created successfully. Please try again.");
            }
            
            await policyService.AcceptPolicyWithoutValidation(user.PolicyId, registeredUser.Id);

            return registeredUser;
            
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

        public async Task<User> GetUserById(Guid id)
        {
            var user = await userRepository.GetUserById(id);
            if(user == null)
            {
                throw new Exception("User doesn't exist");
            }

            return user;
        }

        public async Task<ReturnUserDto> GetLoggedInUser(Guid id)
        {
            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            var returnUserDto = new ReturnUserDto
            {
                FirstName = user.FirstName, 
                LastName = user.LastName,
                Email = user.Email,
                IsOnline = user.IsOnline,
                CreatedAt = user.CreatedAt
            };

            return returnUserDto;
        }

        public async Task SetOnline(Guid userId)
        {
            await userRepository.SetOnlineState(userId, true);
            return;
        }

        public async Task SetOffline(Guid authUserId, Guid userId)
        {
            if (authUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource.");
            }
            await userRepository.SetOnlineState(userId, false);
            return;
        }

        public async Task<List<ReturnSearchedUsers>> SearchUsersByName(Guid userId, string searchName)
        {
            var users = await userRepository.SearchUsersByName(userId, searchName);
            var returnUserDtoList = new List<ReturnSearchedUsers>();

            foreach (var user in users)
            {
                var isFollower = await followService.CheckExistingFollow(userId, userId, user.Id);
                var returnUserDto = new ReturnSearchedUsers
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsOnline = user.IsOnline,
                    IsFollowed = isFollower
                };

                returnUserDtoList.Add(returnUserDto);
            }
            return returnUserDtoList;
        }
    }
}
