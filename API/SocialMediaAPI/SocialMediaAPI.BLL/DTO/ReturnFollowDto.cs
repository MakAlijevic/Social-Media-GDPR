using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class ReturnFollowDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
