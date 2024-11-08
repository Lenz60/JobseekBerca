using JobseekBerca.Models;
using JobseekBerca.View;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Roles> GetAllRole();
        Roles GetRoleById(string roleId);
        int UpdateRole(Roles roles);
        int DeleteRole(string roleId);
        int AddRole(Roles roles);
    }
}
