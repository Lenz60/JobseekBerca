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
            try
            {
                return _myContext.Users.Any(e => e.email == email);

            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }

        }
        public int Register(UserVM.RegisterVM registervm)
        {
            try
            {
                string hashedPassword = HashingHelper.HashPassword(registervm.password);
                var newUser = new Users
                {
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
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }

        }

        public int ChangePassword(ChangePasswordVM changePassword)
        {
            try
            {
                var cekData = _myContext.Users
               .FirstOrDefault(a => a.userId == changePassword.userId);

                if (cekData == null)
                {
                    throw new HttpResponseExceptionHelper(404, "User is not found");
                }
                if (!HashingHelper.ValidatePassword(changePassword.oldPassword, cekData.password))
                {
                    throw new HttpResponseExceptionHelper(400, "Wrong password");
                }

                cekData.password = HashingHelper.HashPassword(changePassword.newPassword);
                _myContext.Users.Update(cekData);
                return _myContext.SaveChanges();

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int Login(UserVM.LoginVM login)
        {
            try
            {
                var user = _myContext.Users
                    .FirstOrDefault(a => a.email == login.email);

                if (user == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Email is not registered");
                }
                bool isValid = HashingHelper.ValidatePassword(login.password, user.password);
                if (isValid)
                {
                    return SUCCESS;
                }
                throw new HttpResponseExceptionHelper(400, "Wrong password");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
        public int CheckUserId(string userId)
        {
            //throw new NotImplementedException();
            try
            {
                var check = _myContext.Users.Find(userId);
                if (check == null)
                {
                    throw new HttpResponseExceptionHelper(404, "User is not found");
                }
                return SUCCESS;
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
        public PayloadVM.GenerateVM GetCredsByEmail(string email)
        {
            try
            {
                var check = CheckEmail(email);
                if (!check)
                {
                    throw new HttpResponseExceptionHelper(404, "Invalid email");
                }
                var payload = _myContext.Users.Select(u => new PayloadVM.GenerateVM
                {
                    userId = u.userId,
                    roleId = u.roleId,
                    email = u.email
                }).FirstOrDefault(u => u.email == email);
                return payload;
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public string GenerateToken(PayloadVM.GenerateVM payload)
        {
            try
            {
                return JWTHelper.GenerateToken(payload, _config);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
