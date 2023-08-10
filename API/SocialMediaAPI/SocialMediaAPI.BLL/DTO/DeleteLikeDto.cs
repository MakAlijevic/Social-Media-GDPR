using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class DeleteLikeDto
    {
        public Guid Author { get; set; }
        public Guid PostId { get; set; }
    }
}
