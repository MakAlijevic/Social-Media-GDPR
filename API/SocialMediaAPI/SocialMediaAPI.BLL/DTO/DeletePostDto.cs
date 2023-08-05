using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class DeletePostDto
    {
        public Guid Author { get; set; }
        public Guid PostId { get; set; }
    }
}
