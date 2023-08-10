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
                .Where(l => l.Author == like.Author && l.PostId == post.Id)
                .FirstOrDefaultAsync();

            if(existingLike != null)
            {
                return existingLike;
            }

            post.Likes.Add(like);
            await context.SaveChangesAsync();
            return like;
        }

        public async Task RemoveLike(Post post, Like like)
        {
            post.Likes.Remove(like);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<Like> GetLikeByPostIdAndUserId(Guid postId, Guid userId)
        {
            var like = await context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.Author == userId);

            return like;
        }

    }
}
