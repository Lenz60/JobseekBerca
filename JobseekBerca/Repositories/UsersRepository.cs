using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using static JobseekBerca.ViewModels.UserVM;
using JobseekBerca.Helper;

namespace JobseekBerca.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyContext _myContext;
        public UsersRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public const int ACCOUNT_NOT_FOUND = -2;
        public const int INVALID_PASSWORD = -1;
        public const int FAIL = 0;
        public const int SUCCESS = 1;

        public int DeleteUser(string userId)
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
            string hashedPassword = Hashing.HashPassword(registervm.password);
            var newUser = new Users
            {
                userId = GenerateIdUser(),
                email = registervm.email,
                roleId = "R02",
                password = hashedPassword,
            };

            var newProfile = new Profiles
            {
                userId = newUser.userId,
                fullName = $"{registervm.firstName} {registervm.lastName}"
            };

            _myContext.Users.Add(newUser);
            _myContext.Profiles.Add(newProfile);
            return _myContext.SaveChanges();

        }

        public int ChangePassword(ChangePasswordVM changePassword)
        {
            var cekData = _myContext.Users
           .FirstOrDefault(a => a.userId == changePassword.userId);

            if (cekData == null)
            {
                return FAIL;
            }
            if (!Hashing.ValidatePassword(changePassword.oldPassword, cekData.password))
            {
                return INVALID_PASSWORD;
            }

            cekData.password = Hashing.HashPassword(changePassword.newPassword);
            _myContext.Users.Update(cekData);
            return _myContext.SaveChanges();
        }

        public int Login(UserVM.LoginVM login)
        {
            var data = _myContext.Users
                .FirstOrDefault(a =>  a.email == login.email);

            if (data == null)
            {
                return ACCOUNT_NOT_FOUND;
            }
            bool isValid = Hashing.ValidatePassword(login.password, data.password);
            return isValid ? SUCCESS : INVALID_PASSWORD;
        }
  
    }
}
