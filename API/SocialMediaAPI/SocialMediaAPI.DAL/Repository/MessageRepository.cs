using Microsoft.EntityFrameworkCore;
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


        public async Task<List<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId)
        {
            var messages = await context.Messages
                .Where(message =>
                    (message.SenderId == FollowerId && message.RecieverId == FollowingId) ||
                    (message.SenderId == FollowingId && message.RecieverId == FollowerId))
                .OrderBy(message => message.CreatedAt)
                .ToListAsync();

            return messages;
        }

        public async Task<List<Message>> GetAllFriendsForMessages(Guid userId)
        {
            var messages = context.Messages.Where(x => x.SenderId == userId || x.RecieverId == userId).ToListAsync();
            return await messages;
        }
    }
}
