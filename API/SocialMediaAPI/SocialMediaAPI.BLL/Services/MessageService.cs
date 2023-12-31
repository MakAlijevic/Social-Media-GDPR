﻿using SocialMediaAPI.BLL.DTO;
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

        public async Task<Message> StartAChat(Guid authUserId, NewChatDto newChatDto)
        {
            CheckIsUserValidAgainstJWT(authUserId, newChatDto.SenderId);

            var message = new Message
            {
                SenderId = newChatDto.SenderId,
                RecieverId = newChatDto.RecieverId,
                Content= newChatDto.Content,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            var returnMessage = await messageRepository.AddMessage(message);
            return returnMessage;

        }


        public async Task<List<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId)
        {
            var messages = await messageRepository.GetAllMessagesBetweenFriends(FollowerId, FollowingId);

            return messages;

        }

        public async Task<bool> CheckIfChatExists(Guid FollowerId, Guid FollowingId)
        {
            var exists = await messageRepository.CheckIfChatExists(FollowerId, FollowingId);

            return exists;

        }

        public async Task<List<ReturnUserDto>> GetAllFriendsForMessages(Guid authUserId, Guid userId)
        {
            CheckIsUserValidAgainstJWT(authUserId, userId);

            var friendsForMessages = await messageRepository.GetAllFriendsForMessages(userId);

            var friends = new List<ReturnUserDto>();

            var chats = friendsForMessages
                .GroupBy(x => x.SenderId == userId ? x.RecieverId : x.SenderId)
                .Select(y => new ReturnMessageFriend
                {
                    UserId = y.Key,
                    LatestMessage = y.Max(m => m.CreatedAt)
                })
                .OrderByDescending(chat => chat.LatestMessage)
                .ToList();


            foreach (var friend in chats)
            {
                var user = await userRepository.GetUserById(friend.UserId);

                if (user != null && user.Id != userId)
                {
                    var returnUserDto = new ReturnUserDto
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsOnline = user.IsOnline,
                        CreatedAt = user.CreatedAt
                    };

                    friends.Add(returnUserDto);
                }
            }

            return friends;
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
