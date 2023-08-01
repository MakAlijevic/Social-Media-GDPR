using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface IPolicyService
    {
        Task<Policy> AddPolicy(CreatePolicyDto policyDto);
        Task<Policy> GetPolicyById(Guid id);
        Task<UserPolicy> AcceptPolicyWithoutValidation(Guid policyId, Guid userId);
    }
}
