using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Models
{
    public class UserPolicy
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }

        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
