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
    public class CommentRepository : ICommentRepository
    {
        private readonly GeneralDataContext context;
        public CommentRepository(GeneralDataContext context)
        {
            this.context = context;
        }

        public async Task<Comment> AddComment(Post post, Comment comment)
        {
            post.Comments.Add(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> RemoveComment(Post post, Comment comment)
        {
            if (post.Comments.Contains(comment))
            {
                post.Comments.Remove(comment);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Comment> GetCommentByPostAndCommentId(Guid postId, Guid commentId)
        {
            var post = await context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post != null)
            {
                var comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
                return comment;
            }

            return null;
        }



    }
}
