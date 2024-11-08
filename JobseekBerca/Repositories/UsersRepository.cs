using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using static JobseekBerca.ViewModels.UserVM;

namespace JobseekBerca.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyContext _myContext;
        public UsersRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public int DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public int Login(UserVM.RegisterVM loginvm)
        {
            throw new NotImplementedException();
        }
        public string GenerateIdUser()
        {
            var checkId = _myContext.Users.OrderByDescending(d => d.userId).FirstOrDefault();
            string newUserId;
            if (checkId != null)
            {
                int lastId = int.Parse(checkId.userId.Substring(1));
                newUserId = "U" + (lastId + 1).ToString("D4");
            }
            else
            {
                newUserId = "U0001";
            }
            return newUserId;
        }
        public bool CheckEmail(string email)
        {
            return _myContext.Users.Any(e => e.email == email);

        }
        public int Register(UserVM.RegisterVM registervm)
        {
            var newUser = new Users
            {
                userId = GenerateIdUser(),
                email = registervm.email,
                roleId = "R02",
                password = registervm.password,
            };

            var newProfile = new Profiles
            {
                userId = newUser.userId,
                fullName = registervm.firstName + registervm.lastName,
            };

            _myContext.Users.Add(newUser);
            _myContext.Profiles.Add(newProfile);
            return _myContext.SaveChanges();

        }

        public int UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
