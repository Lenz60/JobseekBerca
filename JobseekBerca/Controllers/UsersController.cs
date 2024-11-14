using JobseekBerca.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobseekBerca.ViewModels;
using JobseekBerca.Helper;
using JobseekBerca.Models;
using static JobseekBerca.ViewModels.UserVM;
using Microsoft.AspNetCore.Cors;


namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
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

        [HttpPost("registerGoogle")]
        public IActionResult RegisterGoogle(UserVM.RegisterGoogleVM registerVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(registerVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var checkEmail = _usersRepository.CheckEmail(registerVM.email);

            if (checkEmail)
            {
                return ResponseHTTP.CreateResponse(409, "Email has been used");
            }
            var data = _usersRepository.RegisterGoogle(registerVM);

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

            try
            {
                var account = _usersRepository.Login(login);
                var creds = _usersRepository.GetCredsByEmail(login.email);
                var token = _usersRepository.GenerateToken(creds);
                return ResponseHTTP.CreateResponse(200, "Login successful", token);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }
        
        [HttpPost("loginGoogle")]
        public IActionResult LoginGoogle(UserVM.LoginGoogleVM login)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(login, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }

            try
            {
                var account = _usersRepository.LoginGoogle(login);
                var creds = _usersRepository.GetCredsByEmail(login.email);
                var token = _usersRepository.GenerateToken(creds);
                return ResponseHTTP.CreateResponse(200, "Login successful", token);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("changePassword")]
        public IActionResult ChangePassword(UserVM.ChangePasswordVM changePasswordVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(changePasswordVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var result = _usersRepository.ChangePassword(changePasswordVM);
                return ResponseHTTP.CreateResponse(200, "Password changed.");

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }
    }
}
