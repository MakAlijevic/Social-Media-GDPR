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

        public async Task<bool> IsEmailInUse(string email)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (existingUser != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
