using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class ReturnMessageFriend
    {
        public Guid UserId { get; set; }
        public DateTime LatestMessage { get; set; }
    }
}
