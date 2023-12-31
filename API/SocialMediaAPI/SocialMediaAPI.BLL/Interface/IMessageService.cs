﻿using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface IMessageService
    {
        Task<Message> AddMessage(Guid authUserId, AddMessageDto messageDto);
        Task<List<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId);
        Task<List<ReturnUserDto>> GetAllFriendsForMessages(Guid authUserId, Guid userId);
        Task<Message> StartAChat(Guid authUserId, NewChatDto newChatDto);
        Task<bool> CheckIfChatExists(Guid FollowerId, Guid FollowingId);
    }
}
