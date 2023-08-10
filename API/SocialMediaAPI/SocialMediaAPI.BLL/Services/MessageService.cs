using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using SocialMediaAPI.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class MessageService : IMessageService
    {
        private IFollowService followService;
        private IUserRepository userRepository;
        private IMessageRepository messageRepository;
        public MessageService(IFollowService followService, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            this.followService = followService;
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;

        }
        public async Task<Message> AddMessage(Guid authUserId, AddMessageDto messageDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, messageDto.SenderId);

            await followService.VerifyExistingFriendship(messageDto.SenderId, messageDto.RecieverId);

            var message = new Message
            {
                SenderId = messageDto.SenderId,
                RecieverId = messageDto.RecieverId,
                Content = messageDto.Content,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            var returnMessage = await messageRepository.AddMessage(message);
            return returnMessage;
            
        }

        public async Task<List<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId)
        {
            await followService.VerifyExistingFriendship(FollowerId, FollowingId);

            var messages = await messageRepository.GetAllMessagesBetweenFriends(FollowerId, FollowingId);

            return messages;

        }

        private bool CheckIsUserValidAgainstJWT(Guid authUserId, Guid userId)
        {
            if (authUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource.");
            }

            return true;
        }

        private async Task<bool> ValidateUsers(Guid senderId, Guid recieverId)
        {
            var sender = await userRepository.GetUserById(senderId);
            var reciever = await userRepository.GetUserById(recieverId);
            if (sender == null || reciever == null)
            {
                throw new Exception("Invalid user id.");
            }

            return true;
        }
    }
}
