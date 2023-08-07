using SocialMediaAPI.DAL.Data;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly GeneralDataContext context;
        public MessageRepository(GeneralDataContext context)
        {
            this.context = context;
        }
        public async Task<Message> AddMessage(Message message)
        {
            context.Add(message);
            await context.SaveChangesAsync();
            return await Task.FromResult(message);
        }
    }
}
