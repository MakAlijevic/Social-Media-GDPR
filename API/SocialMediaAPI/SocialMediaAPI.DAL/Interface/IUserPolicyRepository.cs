using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface IUserPolicyRepository
    {
        Task<UserPolicy> AcceptPolicy(UserPolicy policy);
    }
}
