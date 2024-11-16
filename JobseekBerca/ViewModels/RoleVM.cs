using System.ComponentModel.DataAnnotations;

namespace JobseekBerca.ViewModels
{
    public class RoleVM
    {
        public class GetDetailsVM
        {
            public string userId { get; set; }
            public string userName { get; set; }
            public string userEmail { get; set; }
            public string roleName { get; set; }
        }
        public class ChangeRoleVM
        {
            public string? userId { get; set; }
            public string? roleId { get; set; }
        }
    }
}
