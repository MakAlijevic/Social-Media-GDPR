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
    }
}
