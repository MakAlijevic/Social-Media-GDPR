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
        private IUserPolicyRepository userPolicyRepository;
        public PolicyService(IPolicyRepository policyRepository, IUserPolicyRepository userPolicyRepository)
        {
            this.policyRepository = policyRepository;
            this.userPolicyRepository = userPolicyRepository;
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

        public async Task<Policy> GetPolicyById(Guid id)
        {
            var policy = await policyRepository.GetPolicyById(id);
            if(policy == null)
            {
                throw new Exception("Policy doesn't exist");
            }

            return policy;
        }

        public async Task<UserPolicy> AcceptPolicyWithoutValidation(Guid policyId, Guid userId)
        {

            var userPolicy = new UserPolicy
            {
                UserId = userId,
                PolicyId = policyId,
                IsAccepted = true,
                CreatedAt = DateTime.Now,
            };

            return await userPolicyRepository.AcceptPolicy(userPolicy);
        }
    }
}
