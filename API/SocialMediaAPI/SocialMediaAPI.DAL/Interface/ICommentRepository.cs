using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Post post, Comment comment);
        Task<bool> RemoveComment(Post post, Comment comment);
        Task<Comment> GetCommentByPostAndCommentId(Guid postId, Guid commentId);
    }
}
