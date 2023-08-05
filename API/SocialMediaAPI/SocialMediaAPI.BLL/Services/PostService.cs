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
    public class PostService:IPostService
    {
        private IPostRepository postRepository;
        private IUserRepository userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public async Task<Post> AddPost(CreatePostDto postDto)
        {
            var author = await userRepository.GetUserById(postDto.Author);
            if (author == null)
            {
                throw new Exception("Author doesnt exits");
            }
            var post = new Post
            {
                Author = postDto.Author,
                Content = postDto.Content,
                CreatedAt = DateTime.Now
            };

            return await postRepository.AddPost(post);
        }

        public async Task<string> DeletePost(Guid authUserId, DeletePostDto deletePostDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, deletePostDto.Author);

            try
            {
                var post = await postRepository.GetPostById(deletePostDto.PostId);
                await postRepository.DeletePost(post);
            }
            catch (Exception)
            {
                throw new Exception("Unsucessfully deleted post");
            }

            return ("Successfully deleted post");
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
