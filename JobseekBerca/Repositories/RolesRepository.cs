using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using System.Data;

namespace JobseekBerca.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly MyContext _myContext;
        public RolesRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public int AddRole(Roles roles)
        {
            var checkId = _myContext.Roles.OrderByDescending(d => d.roleId).FirstOrDefault();

            if (checkId != null)
            {
                int lastId = int.Parse(checkId.roleId.Substring(1));
                roles.roleId = "R" + (lastId + 1).ToString("D2");
            }
            else
            {
                roles.roleId = "R01";
            }
            _myContext.Roles.Add(roles);
            return _myContext.SaveChanges();
        }

        public int DeleteRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> GetAllRole()
        {
            throw new NotImplementedException();
        }

        public Roles GetRoleById(string roleId)
        {
            throw new NotImplementedException();
        }

        public int UpdateRole(Roles roles)
        {
            throw new NotImplementedException();
        }
    }
}
