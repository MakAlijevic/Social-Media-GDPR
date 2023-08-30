using Microsoft.IdentityModel.Tokens;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using SocialMediaAPI.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class CommentService : ICommentService
    {
        private IPostRepository postRepository;
        private IUserRepository userRepository;
        private ICommentRepository commentRepository;
        public CommentService(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
        }

        public async Task<Comment> AddComment(Guid authUserId, AddCommentDto addCommentDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, addCommentDto.Author);
            var user = await userRepository.GetUserById(authUserId);

            var post = await postRepository.GetPostById(addCommentDto.PostId);

            if (post == null)
            {
                throw new Exception("Post doesnt exits");
            }
            var comment = new Comment
            {
                Author = addCommentDto.Author,
                Content = addCommentDto.Content,
                CreatedAt = DateTime.Now
            };

            if (comment.Content.IsNullOrEmpty())
            {
                throw new Exception("Comment cannot be empty!");
            }

            return await commentRepository.AddComment(post, comment);
        }

        public async Task<bool> RemoveComment(Guid authUserId, DeleteCommentDto deleteCommentDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, deleteCommentDto.Author);
            var user = await userRepository.GetUserById(authUserId);

            var post = await postRepository.GetPostById(deleteCommentDto.PostId);
            var comment = await commentRepository.GetCommentByPostAndCommentId(deleteCommentDto.PostId, deleteCommentDto.CommentId);

            if (comment == null)
            {
                throw new Exception("Post doesnt exits");
            }

            return await commentRepository.RemoveComment(post, comment);
        }

        private bool CheckIsUserValidAgainstJWT(Guid authUserId, Guid userId)
        {
            if (authUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource.");
            }

            return true;
        }
    }
}
