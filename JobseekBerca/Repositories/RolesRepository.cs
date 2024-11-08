using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public const int FAIL = 0;
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
            var data = _myContext.Roles.Find(roleId);
            if (data != null)
            {
                _myContext.Roles.Remove(data);
                return _myContext.SaveChanges();
            }
            return FAIL;
        }

        public IEnumerable<Roles> GetAllRole()
        {
            return _myContext.Roles.ToList();
        }


        public int UpdateRole(Roles roles)
        {
            var exists = _myContext.Roles.Any(r => r.roleId == roles.roleId);

            if (exists)
            {
                _myContext.Entry(roles).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }

            return FAIL;
        }
    }
}
