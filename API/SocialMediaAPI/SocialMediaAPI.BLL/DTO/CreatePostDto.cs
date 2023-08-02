using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.DTO
{
    public class CreatePostDto
    {
        public Guid Author { get; set; }
        public string Content { get; set; }
    }
}
