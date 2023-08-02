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
        public async Task<Follow> AddFollow(AddFollowDto addFollowDto)
        {
            try
            {
                await ValidateFollowUsers(addFollowDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var existingFollow = await ValidateExistingFollow(addFollowDto);
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

        public async Task<string> Unfollow(AddFollowDto unfollowDto)
        {
            try
            {
                await ValidateFollowUsers(unfollowDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var existingFollow = await ValidateExistingFollow(unfollowDto);
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

        private async Task<bool> ValidateExistingFollow(AddFollowDto addFollowDto)
        {
            var existingFollow = await followRepository.CheckExistingFollow(addFollowDto.FollowerId, addFollowDto.FollowingId);
            if (existingFollow != null)
            {
                return true;
            }
            return false;
        }
    }
}
