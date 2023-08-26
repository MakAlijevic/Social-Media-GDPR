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
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext context;

        public UserRepository(UserDataContext context)
        {
            this.context = context;
        }

        public async Task<User> RegisterUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return await Task.FromResult(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task SetOnlineState(Guid userId, bool state)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            user.IsOnline = state;
            await context.SaveChangesAsync();
            return;
        }


        public async Task<List<User>> SearchUsersByName(Guid userId, string searchName)
        {
            var users = await context.Users
                .Where(
                    user => user.Id != userId && // Exclude the current user
                    (user.FirstName.Contains(searchName) ||
                     user.LastName.Contains(searchName) ||
                     (user.FirstName + " " + user.LastName).Contains(searchName)))
                .ToListAsync();

            return users;
        }
    }
}
