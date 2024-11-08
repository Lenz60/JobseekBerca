using JobseekBerca.Models;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        IEnumerable<Roles> GetAllRole();
        Roles GetRoleById(string roleId);
        int UpdateRole(Roles roles);
        int DeleteRole(string roleId);
        int AddRole(Roles roles);
    }
}
