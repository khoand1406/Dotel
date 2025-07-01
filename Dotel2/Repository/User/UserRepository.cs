
using Dotel2.DTOs;
using Dotel2.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotel2.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DotelDBContext context;
        public UserRepository(DotelDBContext dBContext) {
            this.context = dBContext;
        }

        public bool checkUserMemberShip(Models.User user)
        {
            return context.UserMemberships
                .Any(ship => ship.UserId == user.UserId && ship.EndDate> DateTime.UtcNow );
        }

        public Models.User? GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public Models.User? GetUserByEmailAndPassword(string email, string hashedPassword)
        {
            return context.Users.FirstOrDefault(u =>
            u.Email.ToLower() == email.ToLower() && u.Password == hashedPassword);
        }

        public Models.User getUserbyRentalId(int uId)
        {
            return context.Users.FirstOrDefault(user => user.UserId == uId);
        }

        public void RegisterUser(Models.User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUserPassword(string email, string hashedPassword)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.Password = hashedPassword;
                context.SaveChanges();
            }
        }

        public void UpdateUserProfile(Models.User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
