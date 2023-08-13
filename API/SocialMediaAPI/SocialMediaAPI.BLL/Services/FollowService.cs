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
    public class FollowService : IFollowService
    {
        private IFollowRepository followRepository;
        private IUserRepository userRepository;
        public FollowService(IFollowRepository followRepository, IUserRepository userRepository)
        {
            this.followRepository = followRepository;
            this.userRepository = userRepository;

        }
        public async Task<Follow> AddFollow(Guid authUserId, AddFollowDto addFollowDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, addFollowDto.FollowerId);

            try
            {
                await ValidateFollowUsers(addFollowDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var existingFollow = await ValidateExistingFollow(addFollowDto.FollowerId, addFollowDto.FollowingId);
            if(existingFollow == true)
            {
                throw new Exception("User is already followed");
            }

            var follow = new Follow
            {
                FollowerId = addFollowDto.FollowerId,
                FollowingId = addFollowDto.FollowingId,
                CreatedAt = DateTime.Now,
            };

            return await followRepository.AddFollow(follow);
        }

        public async Task<string> Unfollow(Guid authUserId, AddFollowDto unfollowDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, unfollowDto.FollowerId);

            try
            {
                await ValidateFollowUsers(unfollowDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var existingFollow = await ValidateExistingFollow(unfollowDto.FollowerId, unfollowDto.FollowingId);
            if (existingFollow == false)
            {
                throw new Exception("That follow doesn't exist");
            }

            try
            {
                var follow = await followRepository.CheckExistingFollow(unfollowDto.FollowerId, unfollowDto.FollowingId);
                await followRepository.Unfollow(follow);
            }
            catch(Exception)
            {
                throw new Exception("Unsucessfully unfollowed");
            }

            return ("Successfully unfollowed");
        }

        public async Task<List<ReturnFollowDto>> GetAllFollows(Guid authUserId, Guid userId)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            var allFollows = await followRepository.GetAllFollows(userId);

            var resultFollows = new List<ReturnFollowDto>();

            foreach (var follow in allFollows)
            {
                var user = await userRepository.GetUserById(follow.FollowingId);

                if(user != null)
                {
                    var returnFollow = new ReturnFollowDto
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsOnline = user.IsOnline,
                        CreatedAt = user.CreatedAt,
                    };

                    resultFollows.Add(returnFollow);
                }
            }

            return resultFollows;
        }

        public async Task<List<ReturnFollowDto>> GetOnlineFollows(Guid authUserId, Guid userId)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            var allFollows = await followRepository.GetAllFollows(userId);

            var onlineFollows = new List<ReturnFollowDto>();

            foreach (var follow in allFollows)
            {
                var user = await userRepository.GetUserById(follow.FollowingId);

                if (user != null && user.IsOnline == true && user.Id != userId)
                {
                    var returnFollow = new ReturnFollowDto
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsOnline = user.IsOnline,
                        CreatedAt = user.CreatedAt,
                    };

                    onlineFollows.Add(returnFollow);
                }
            }

            return onlineFollows;
        }

        public async Task<bool> VerifyExistingFriendship(Guid userId1, Guid userId2)
        {
            var follow1 = await ValidateExistingFollow(userId1, userId2);
            var follow2 = await ValidateExistingFollow(userId2, userId1);

            if(follow1 != true || follow2 != true)
            {
                throw new Exception("Users must follow each other in order to send messages!");
            }

            return true;
        }

        public async Task<List<ReturnUserDto>> SearchFollowedUsersByName(Guid userId, string searchName)
        {
            var users = await followRepository.SearchFollowedUsersByName(userId, searchName);
            var returnUserDtoList = new List<ReturnUserDto>();

            foreach (var user in users)
            {
                var returnUserDto = new ReturnUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsOnline = user.IsOnline,
                    CreatedAt = user.CreatedAt
                };

                returnUserDtoList.Add(returnUserDto);
            }
            return returnUserDtoList;
        }

        private bool CheckIsUserValidAgainstJWT(Guid authUserId, Guid userId)
        {
            if (authUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource.");
            }

            return true;
        }

        private async Task<bool> ValidateFollowUsers(AddFollowDto addFollowDto)
        {
            var follower = await userRepository.GetUserById(addFollowDto.FollowerId);
            var following = await userRepository.GetUserById(addFollowDto.FollowingId);
            if (follower == null || following == null)
            {
                throw new Exception("Invalid user id.");
            }

            return true;
        }

        private async Task<bool> ValidateExistingFollow(Guid followerId, Guid followingId)
        {
            var existingFollow = await followRepository.CheckExistingFollow(followerId, followingId);
            if (existingFollow != null)
            {
                return true;
            }
            return false;
        }

    }
}
