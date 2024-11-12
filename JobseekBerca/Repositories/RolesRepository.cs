using JobseekBerca.Context;
using JobseekBerca.Helper;
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
            try
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
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int DeleteRole(string roleId)
        {
            try
            {
                var data = _myContext.Roles.Find(roleId);
                if (data != null)
                {
                    _myContext.Roles.Remove(data);
                    return _myContext.SaveChanges();
                }
                throw new HttpResponseExceptionHelper(404, "Invalid role id");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
            //returns FAIL;
        }

        public IEnumerable<Roles> GetAllRole()
        {
            try
            {
                return _myContext.Roles.ToList();

            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }


        public int UpdateRole(Roles roles)
        {
            try
            {
                var role = _myContext.Roles.Any(r => r.roleId == roles.roleId);
                if (role)
                {
                    _myContext.Entry(roles).State = EntityState.Modified;
                    return _myContext.SaveChanges();
                }
                throw new HttpResponseExceptionHelper(404, "Invalid role id");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
