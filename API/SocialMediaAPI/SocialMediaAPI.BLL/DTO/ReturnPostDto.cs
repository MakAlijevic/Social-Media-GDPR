using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class ReturnPostDto
    {
        public Guid Author { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ReturnCommentDto> Comments { get; set; } = new List<ReturnCommentDto>();
        public int Likes { get; set; } = 0;
    }
}
