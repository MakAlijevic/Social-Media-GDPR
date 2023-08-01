using Microsoft.IdentityModel.Tokens;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class PolicyService : IPolicyService
    {
        private IPolicyRepository policyRepository;
        public PolicyService(IPolicyRepository policyRepository)
        {
            this.policyRepository = policyRepository;
        }
        public async Task<Policy> AddPolicy(CreatePolicyDto policyDto)
        {
            if(policyDto.Name.IsNullOrEmpty() || policyDto.Content.IsNullOrEmpty())
            {
                throw new Exception("Values can't be empty");
            }
            var policy = new Policy
            {
                Name = policyDto.Name,
                Content = policyDto.Content,
                CreatedAt = DateTime.Now,
            };

            return await policyRepository.AddPolicy(policy);

        }
    }
}
