using SocialMediaAPI.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<List<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId);
        Task<List<Message>> GetAllFriendsForMessages(Guid userId);
    }
}
