using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class DeleteCommentDto
    {
        public Guid Author { get; set; }
        public Guid CommentId { get; set; }
    }
}
