using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class AddFollowDto
    {
        public Guid FollowerId { get; set; }
        public Guid FollowingId { get; set; }
    }
}
