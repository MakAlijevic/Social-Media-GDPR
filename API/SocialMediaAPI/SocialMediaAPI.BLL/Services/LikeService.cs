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
    public class LikeService : ILikeService
    {
        private IPostRepository postRepository;
        private IUserRepository userRepository;
        private ILikeRepository likeRepository;
        public LikeService(IPostRepository postRepository, IUserRepository userRepository, ILikeRepository likeRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.likeRepository = likeRepository;
        }

        public async Task<Like> AddLike(Guid authUserId, AddLikeDto addLikeDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, addLikeDto.Author);
            var user = await userRepository.GetUserById(authUserId);

            var post = await postRepository.GetPostById(addLikeDto.PostId);
            
            if (post == null)
            {
                throw new Exception("Post doesnt exits");
            }
            var like = new Like
            {
                Author = addLikeDto.Author,
                CreatedAt = DateTime.Now
            };

            return await likeRepository.AddLike(post, like);
        }

        private bool CheckIsUserValidAgainstJWT(Guid authUserId, Guid userId)
        {
            if (authUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource.");
            }

            return true;
        }

        public async Task<string> RemoveLike(Guid authUserId, DeleteLikeDto deleteLikeDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, deleteLikeDto.Author);

            try
            {
                var post = await postRepository.GetPostById(deleteLikeDto.PostId);
                var like = await likeRepository.GetLikeByPostIdAndUserId(post.Id, authUserId);
                await likeRepository.RemoveLike(post, like);
            }
            catch (Exception)
            {
                throw new Exception("Unsucessfully removed like");
            }

            return ("Successfully removed like");
        }
    }
}
