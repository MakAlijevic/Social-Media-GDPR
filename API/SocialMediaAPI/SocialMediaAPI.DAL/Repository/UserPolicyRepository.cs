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
    public class UserPolicyRepository : IUserPolicyRepository
    {
        private readonly UserDataContext context;
        public UserPolicyRepository(UserDataContext context)
        {
            this.context = context;
        }
        public async Task<UserPolicy> AcceptPolicy(UserPolicy userPolicy)
        {
            context.Add(userPolicy);
            await context.SaveChangesAsync();
            return await Task.FromResult(userPolicy);
        }
    }
}
