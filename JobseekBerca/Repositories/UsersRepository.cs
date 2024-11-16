using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using static JobseekBerca.ViewModels.UserVM;
using JobseekBerca.Helper;
using Microsoft.EntityFrameworkCore;
using JobseekBerca.Helper.Interface;

namespace JobseekBerca.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyContext _myContext;
        private readonly IConfiguration _config;
        private readonly SMTPHelper _smtpHelper;
        public UsersRepository(MyContext myContext, IConfiguration config, SMTPHelper smtpHelper)
        {
            _myContext = myContext;
            _config = config;
            _smtpHelper = smtpHelper;
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
                var payload = _myContext.Users.Include(r => r.Roles)
                    .Select(u => new PayloadVM.GenerateVM
                    {
                        userId = u.userId,
                        roleName = u.Roles.roleName,
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

        public int RegisterGoogle(RegisterGoogleVM registervm)
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
                    profileImage = registervm.profileImage
                };

                //Check first on Users Google, if there is no email given by parameter , then create , if not then login
                var newGoogle = new UsersGoogle
                {
                    userId = newUser.userId,
                    email = newUser.email,
                    oauthId = registervm.oauthId
                };

                //Sent password to email
                var toEmail = newUser.email;
                var subject = "Change your password";
                var body = $@"
                            <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        background-color: #f4f4f9;
                                        margin: 0;
                                        padding: 0;
                                    }}
                                    .container {{
                                        width: 100%;
                                        padding: 20px;
                                        background-color: #ffffff;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                        margin: 20px auto;
                                        max-width: 600px;
                                    }}
                                    .header {{
                                        background-color: #0A4D80;
                                        color: white;
                                        padding: 10px 0;
                                        text-align: center;
                                    }}
                                    .content {{
                                        padding: 20px;
                                    }}
                                    .footer {{
                                        text-align: center;
                                        padding: 10px;
                                        font-size: 12px;
                                        color: #888;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <div class='header'>
                                        <h1>Welcome to BerCareer!</h1>
                                    </div>
                                    <div class='content'>
                                        <h2>Thank you for registering!</h2>
                                        <p>Here is your password: <strong>{registervm.password}</strong></p>
                                        <p>Please change it from the profile page of our site.</p>
                                        <p>Thank you!</p>
                                    </div>
                                    <div class='footer'>
                                        <p>&copy; 2024 BerCareer. All rights reserved.</p>
                                    </div>
                                </div>
                            </body>
                            </html>";
                
                _smtpHelper.SendEmail(toEmail, subject, body);
                //SMTPHelper.SendEmail(toEmail, subject, body);

                _myContext.Users.Add(newUser);
                _myContext.UsersGoogle.Add(newGoogle);
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

        public int LoginGoogle(LoginGoogleVM login)
        {
            try
            {
                //var user = _myContext.Users
                //    .FirstOrDefault(a => a.email == login.email);
                //Check for user email and oauthId by including Users andUsersGoogle table 
                var user = _myContext.UsersGoogle.Include(u => u.Users)
                    .FirstOrDefault(a => a.Users.email == login.email && a.oauthId == login.oauthId);

                if (user == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Email is not registered");
                }
                //bool isValid = HashingHelper.ValidatePassword(login.password, user.password);
                //if (isValid)
                //{

                return SUCCESS;
                //}
                //throw new HttpResponseExceptionHelper(400, "Wrong password");
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
    }
}
