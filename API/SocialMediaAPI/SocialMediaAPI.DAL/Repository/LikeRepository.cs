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
    public class LikeRepository : ILikeRepository
    {
        private readonly GeneralDataContext context;
        public LikeRepository(GeneralDataContext context)
        {
            this.context = context;
        }

        public async Task<Like> AddLike(Post post, Like like)
        {
            var existingLike = await context.Likes
                .Where(like => like.Author == like.Author && like.PostId == post.Id)
                .FirstOrDefaultAsync();

            if(existingLike != null)
            {
                return existingLike;
            }

            post.Likes.Add(like);
            await context.SaveChangesAsync();
            return like;
        }
    }
}
