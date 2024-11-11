using JobseekBerca.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobseekBerca.ViewModels;
using JobseekBerca.Helper;
using JobseekBerca.Models;
using static JobseekBerca.ViewModels.UserVM;


namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersRepository _usersRepository;

        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //Register
        [HttpPost]
        public IActionResult Register(UserVM.RegisterVM registerVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(registerVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var checkEmail = _usersRepository.CheckEmail(registerVM.email);

            if (checkEmail)
            {
                return ResponseHTTP.CreateResponse(400, "Email has been used");
            }
            var data = _usersRepository.Register(registerVM);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new user.", registerVM);
            }
            else
            {
                return ResponseHTTP.CreateResponse(400, "Fail to add new user.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserVM.LoginVM login)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(login, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }

            var account = _usersRepository.Login(login);
            if (account == 1)
            {
                return ResponseHTTP.CreateResponse(200, "Login successful", true);
            }
            else if (account == -1)
            {
                return ResponseHTTP.CreateResponse(400, "Invalid password");
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Email not registered");
            }
        }

        [HttpPut("changePassword")]
        public IActionResult ChangePassword(UserVM.ChangePasswordVM changePasswordVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(changePasswordVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }

            var result = _usersRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {

                return ResponseHTTP.CreateResponse(200, "Success change password.");
            }
            else if (result == -1)
            {
                return ResponseHTTP.CreateResponse(400, "Invalid old password.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "User Id  not found.");
            }
        }
    }
}
