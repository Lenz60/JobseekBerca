using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        IEnumerable<Roles> GetAllRole();
        IEnumerable<RoleVM.GetDetailsVM> GetDetailRoles();
        int UpdateRole(Roles roles);
        int ChangeRole(RoleVM.ChangeRoleVM changeVM);
        int DeleteRole(string roleId);
        int AddRole(Roles roles);
    }
}
