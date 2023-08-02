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
        private readonly GeneralDataContext context;
        public FollowRepository(GeneralDataContext context)
        {
            this.context = context;
        }
        public async Task<Follow> AddFollow(Follow follow)
        {
            context.Follows.Add(follow);
            await context.SaveChangesAsync();
            return await Task.FromResult(follow);
        }

        public async Task<Follow> CheckExistingFollow(Guid followerId, Guid followingId)
        {
            var existingFollow = await context.Follows.FirstOrDefaultAsync(x => x.FollowerId == followerId || x.FollowingId == followingId);
            return existingFollow;
        }

       public async Task Unfollow(Follow follow)
       {
            context.Follows.Remove(follow);
            await context.SaveChangesAsync();
            return;
       }
    }
}
