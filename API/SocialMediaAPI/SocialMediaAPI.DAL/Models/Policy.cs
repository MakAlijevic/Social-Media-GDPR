using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Models
{
    public class Policy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<UserPolicy> UserPolicies { get; set; }

    }
}
