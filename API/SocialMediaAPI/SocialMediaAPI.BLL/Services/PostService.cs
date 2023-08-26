using Microsoft.EntityFrameworkCore;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class PostService : IPostService
    {
        private IPostRepository postRepository;
        private IUserRepository userRepository;
        private ILikeRepository likeRepository; 
        public PostService(IPostRepository postRepository, IUserRepository userRepository, ILikeRepository likeRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.likeRepository = likeRepository;
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

            if (post.Content.IsNullOrEmpty())
            {
                throw new Exception("Post cannot be empty!");
            }

            return await postRepository.AddPost(post);
        }

        public async Task<List<ReturnPostDto>> GetAllPosts(Guid authUserId, Guid userId, int pageNumber, int pageSize)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            var posts = await postRepository.GetAllPosts(userId, pageNumber, pageSize);

            var returnPosts = new List<ReturnPostDto>();

            foreach (var post in posts)
            {
                var returnComments = new List<ReturnCommentDto>();

                foreach (var comment in post.Comments)
                {
                    var user = await userRepository.GetUserById(comment.Author);

                    if (user != null)
                    {
                        var returnComment = new ReturnCommentDto
                        {
                            Id = comment.Id,
                            Author = comment.Author,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Content = comment.Content,
                            CreatedAt = comment.CreatedAt
                        };

                        returnComments.Add(returnComment);
                    }
                }

                var author = await userRepository.GetUserById(post.Author);

                if (author != null)
                {
                    var returnPost = new ReturnPostDto
                    {
                        Id = post.Id,
                        Author = post.Author,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Email = author.Email,
                        Content = post.Content,
                        CreatedAt = post.CreatedAt,
                        Comments = returnComments,
                        Likes = post.Likes.Count,
                        IsLiked = await likeRepository.GetLikeByPostIdAndUserId(post.Id, authUserId) != null ? true : false
                    };

                    returnPosts.Add(returnPost);
                }
            }

            return returnPosts;
        }

        public async Task<ReturnPostDto> GetPostById(Guid postId)
        {
            var post = await postRepository.GetPostById(postId);
            var user = await userRepository.GetUserById(post.Author);

            var returnComments = new List<ReturnCommentDto>();

            foreach (var comment in post.Comments)
            {
                var author = await userRepository.GetUserById(comment.Author);

                if (author != null)
                {
                    var returnComment = new ReturnCommentDto
                    {
                        Id = comment.Id,
                        Author = comment.Author,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Email = author.Email,
                        Content = comment.Content,
                        CreatedAt = comment.CreatedAt
                    };

                    returnComments.Add(returnComment);
                }
            }

            var returnPost = new ReturnPostDto
            {
                Id = post.Id,
                Author = post.Author,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Comments = returnComments,
                Likes = post.Likes.Count
            };

            return returnPost;
        }

        public async Task<int> GetTotalAmoutOfPosts(Guid authUserId, Guid userId)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            return await postRepository.GetTotalAmountOfPosts(userId);
        }

        public async Task<List<ReturnPostDto>> GetPostsByUserId(Guid authUserId, Guid userId)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            var posts = await postRepository.GetPostsByUserId(userId);

            var returnPosts = new List<ReturnPostDto>();

            foreach (var post in posts)
            {
                var returnComments = new List<ReturnCommentDto>();

                foreach (var comment in post.Comments)
                {
                    var user = await userRepository.GetUserById(comment.Author);

                    if (user != null)
                    {
                        var returnComment = new ReturnCommentDto
                        {
                            Id = comment.Id,   
                            Author = comment.Author,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Content = comment.Content,
                            CreatedAt = comment.CreatedAt
                        };

                        returnComments.Add(returnComment);
                    }
                }

                var author = await userRepository.GetUserById(post.Author);

                if (author != null)
                {
                    var returnPost = new ReturnPostDto
                    {
                        Id = post.Id,
                        Author = post.Author,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Email = author.Email,
                        Content = post.Content,
                        CreatedAt = post.CreatedAt,
                        Comments = returnComments,
                        Likes = post.Likes.Count,
                        IsLiked = await likeRepository.GetLikeByPostIdAndUserId(post.Id, authUserId) != null ? true : false
                    };

                    returnPosts.Add(returnPost);
                }
            }

            return returnPosts;
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
