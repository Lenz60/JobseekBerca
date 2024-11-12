using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static JobseekBerca.ViewModels.UserVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RolesRepository _rolesRepository;

        public RolesController(RolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            try
            {
                var data = _rolesRepository.GetAllRole();
                return ResponseHTTP.CreateResponse(200, "Roles retrieved successfully.", data);

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

        [HttpPost]
        public IActionResult AddRoles(Roles roles)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(roles, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var addRole = _rolesRepository.AddRole(roles);
                return ResponseHTTP.CreateResponse(200, "Success add new role", roles);

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

        [HttpDelete]
        public IActionResult DeleteRole(string roleId)
        {
            try
            {
                int result = _rolesRepository.DeleteRole(roleId);
                return ResponseHTTP.CreateResponse(200, "Success deleted role.");

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

        [HttpPut]
        public IActionResult UpdateRole([FromBody] Roles roles)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(roles, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                int data = _rolesRepository.UpdateRole(roles);
                return ResponseHTTP.CreateResponse(200, "Role successfully updated.", roles);

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
