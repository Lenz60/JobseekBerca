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
        private readonly IConfiguration _config;
        public UsersRepository(MyContext myContext, IConfiguration config)
        {
            _myContext = myContext;
            _config = config;
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
            string hashedPassword = HashingHelper.HashPassword(registervm.password);
            var newUser = new Users
            {
                // Generate userId with UID increment based on last created
                // userId = GenerateIdUser(),
                // Generate userId with ULID
                userId = ULIDHelper.GenerateULID(),
                email = registervm.email,
                roleId = "R03",
                password = hashedPassword,
            };

            var newProfile = new Profiles
            {
                userId = newUser.userId,
                fullName = $"{registervm.firstName} {registervm.lastName}",
                summary = $"Hello my name is {registervm.firstName} {registervm.lastName} and I'm eager to learn",
                gender = null,
                address = null,
                birthDate = null,
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
            if (!HashingHelper.ValidatePassword(changePassword.oldPassword, cekData.password))
            {
                return INVALID_PASSWORD;
            }

            cekData.password = HashingHelper.HashPassword(changePassword.newPassword);
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
            bool isValid = HashingHelper.ValidatePassword(login.password, data.password);
            return isValid ? SUCCESS : INVALID_PASSWORD;
        }
        public int CheckUserId(string userId)
        {
            //throw new NotImplementedException();
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                return FAIL;
            }
            return SUCCESS;
        }

        

        public PayloadVM.GenerateVM GetCredsByEmail(string email)
        {
            var check = CheckEmail(email);
            if (!check)
            {
                return null;
            }
            var payload = _myContext.Users.Select(u => new PayloadVM.GenerateVM
            {
                userId = u.userId,
                roleId = u.roleId,
                email = u.email
            }).FirstOrDefault(u => u.email == email);
            return payload;
        }

        public string GenerateToken(PayloadVM.GenerateVM payload)
        {
            return JWTHelper.GenerateToken(payload, _config);
        }
    }
}
