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
    public class PolicyRepository : IPolicyRepository
    {
        private readonly UserDataContext context;
        public PolicyRepository(UserDataContext context)
        {
            this.context = context;
        }
        public async Task<Policy> AddPolicy(Policy policy)
        {
            context.Add(policy);
            await context.SaveChangesAsync();
            return await Task.FromResult(policy);
        }
        public async Task<Policy> GetPolicyById(Guid id)
        {
            return await context.Policies.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
