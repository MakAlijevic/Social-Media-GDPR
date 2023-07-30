using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
