using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.DAL.Data;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public async Task<bool> RemoveComment(Guid commentId)
        {
            var commentToRemove = await context.Comments.FindAsync(commentId);

            if (commentToRemove == null)
            {
                return false;
            }

            context.Comments.Remove(commentToRemove);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
