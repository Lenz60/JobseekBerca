using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public IActionResult AddRoles(Roles roles)
        {
            if (string.IsNullOrEmpty(roles.roleName))
            {
                return ResponseHTTP.CreateResponse(400, "Role name is required.");
            }
            var addRole = _rolesRepository.AddRole(roles);

            if (addRole > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new role.", roles);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Role Failed added");
            }
        }
    }
}
