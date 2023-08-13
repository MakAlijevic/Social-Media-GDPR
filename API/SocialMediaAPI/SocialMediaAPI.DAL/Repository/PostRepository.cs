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
    public class PostRepository : IPostRepository
    {
        private readonly GeneralDataContext context;
        public PostRepository(GeneralDataContext context)
        {
            this.context = context;
        }

        public async Task<Post> AddPost(Post post)
        {
            context.Add(post);
            await context.SaveChangesAsync();
            return await Task.FromResult(post);
        }

        public async Task<List<Post>> GetAllPosts(Guid userId)
        {
            var followerIds = await context.Follows
                .Where(follower => follower.FollowerId == userId)
                .Select(follower => follower.FollowingId)
                .ToListAsync();

            var userPosts = await context.Posts
                .Where(post => post.Author == userId)
                .ToListAsync();

            var followersPosts = await context.Posts
                .Where(post => followerIds.Contains(post.Author))
                .OrderByDescending(post => post.CreatedAt)
                .ToListAsync();

            var allPosts = userPosts.Concat(followersPosts).OrderByDescending(post => post.CreatedAt);

            return allPosts.ToList();
        }

        public async Task DeletePost(Post post)
        {
            context.Remove(post);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<Post> GetPostById(Guid postId)
        {
            var existingPost = await context.Posts
                .Include(post => post.Comments)
                .Include(post => post.Likes)
                .FirstOrDefaultAsync(post => post.Id == postId);

            return existingPost;
        }

        public async Task<List<Post>> GetPostsByUserId(Guid userId)
        {
            var posts = await context.Posts
                .Where(post => post.Author == userId)
                .ToListAsync();

            return posts;
        }
    }
}
