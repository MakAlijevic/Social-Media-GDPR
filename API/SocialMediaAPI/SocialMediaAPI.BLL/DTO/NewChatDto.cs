using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class NewChatDto
    {
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }
        public string Content { get; set; } = "Lets start a conversation!";
    }
}
