using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
            var data = _rolesRepository.GetAllRole();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data Roles.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Roles retrieved successfully.", data);
            }
        }

        [HttpPost]
        public IActionResult AddRoles(Roles roles)
        {
            if (string.IsNullOrEmpty(roles.roleName))
            {
                return ResponseHTTP.CreateResponse(400, "Role name is required!");
            }
            var addRole = _rolesRepository.AddRole(roles);

            if (addRole > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new role", roles);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Role Failed added");
            }
        }

        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(string roleId)
        {
            int result = _rolesRepository.DeleteRole(roleId);
            if (result > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success deleted role.");
            }
            return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
        }

        [HttpPut]
        public IActionResult UpdateRole([FromBody] Roles roles)
        {
            if (string.IsNullOrEmpty(roles.roleId))
            {
                return ResponseHTTP.CreateResponse(400, "RoleID is required!");
            }else if (string.IsNullOrEmpty(roles.roleName))
            {
                return ResponseHTTP.CreateResponse(400, "RoleName is required!");
            }
            int data = _rolesRepository.UpdateRole(roles);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Role successfully updated.", roles);
            }
            return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
        }
    }
}
