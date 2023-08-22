using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.DAL.Data;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly UserDataContext userContext;
        private readonly GeneralDataContext context;
        public FollowRepository(GeneralDataContext context, UserDataContext userContext)
        {
            this.context = context;
            this.userContext = userContext;
        }
        public async Task<Follow> AddFollow(Follow follow)
        {
            context.Follows.Add(follow);
            await context.SaveChangesAsync();
            return await Task.FromResult(follow);
        }

        public async Task<Follow> CheckExistingFollow(Guid followerId, Guid followingId)
        {
            var existingFollow = await context.Follows.FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);
            return existingFollow;
        }

        public async Task Unfollow(Follow follow)
        {
            context.Follows.Remove(follow);
            await context.SaveChangesAsync();
            return;
        }
        
        public async Task<List<Follow>> GetAllFollows(Guid userId)
        {
            var allFollows = await context.Follows.Where(x => x.FollowerId == userId).ToListAsync();
            return allFollows;
        }

        public async Task<List<User>> SearchFollowedUsersByName(Guid userId, string searchName)
        {
            var followedUserIds = await context.Follows
                .Where(follow => follow.FollowerId == userId)
                .Select(follow => follow.FollowingId)
                .ToListAsync();

            var users = await userContext.Users
                .Where(user => followedUserIds.Contains(user.Id) &&
                               (user.FirstName.Contains(searchName) ||
                                user.LastName.Contains(searchName) ||
                                (user.FirstName + " " + user.LastName).Contains(searchName)))
                .ToListAsync();

            return users;
        }

        public async Task<List<Follow>> GetAllFollowings(Guid userId)
        {
            var allFollowings = await context.Follows.Where(x => x.FollowingId == userId).ToListAsync();
            return allFollowings;
        }
    }
}
