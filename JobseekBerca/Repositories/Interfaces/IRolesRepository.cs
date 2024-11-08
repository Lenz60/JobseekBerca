using JobseekBerca.Models;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        IEnumerable<Roles> GetAllRole();
        int UpdateRole(Roles roles);
        int DeleteRole(string roleId);
        int AddRole(Roles roles);
    }
}
