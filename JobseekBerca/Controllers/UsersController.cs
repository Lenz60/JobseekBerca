using JobseekBerca.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobseekBerca.ViewModels;
using JobseekBerca.Helper;
using JobseekBerca.Models;


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
            if (string.IsNullOrWhiteSpace(registerVM.firstName))
            {
                return ResponseHTTP.CreateResponse(400, "First Name is required!");
            }
            else if (string.IsNullOrWhiteSpace(registerVM.lastName))
            {
                return ResponseHTTP.CreateResponse(400, "Last Name is required!");
            }
            else if (string.IsNullOrWhiteSpace(registerVM.email)){

                return ResponseHTTP.CreateResponse(400, "Email is required!");
            }
            else if (string.IsNullOrWhiteSpace(registerVM.password)){
                return ResponseHTTP.CreateResponse(400, "Password Name is required!");
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

        [HttpPut("changePassword")]
        public IActionResult ChangePassword(UserVM.ChangePasswordVM changePasswordVM)
        {
            if (string.IsNullOrEmpty(changePasswordVM.OldPassword))
            {
                return ResponseHTTP.CreateResponse(400, "Old password is required!");
            }else if (string.IsNullOrEmpty(changePasswordVM.NewPassword))
            {
                return ResponseHTTP.CreateResponse(400, "New password is required!");
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
