using JobseekBerca.Context;
using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Server.IIS.Core;
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

        public IEnumerable<RoleVM.GetDetailsVM> GetDetailRoles()
        {
            try
            {
                var data = _myContext.Profiles.Include(u => u.Users.Roles)
                    .Select(r => new RoleVM.GetDetailsVM
                    {
                        userId = r.userId,
                        userName = r.fullName,
                        roleName = r.Users.Roles.roleName,
                        userEmail = r.Users.email
                    }).ToList();
                if (data != null)
                {
                    return data;

                }
                throw new HttpResponseExceptionHelper(404, "Data not found");
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

        public int ChangeRole(RoleVM.ChangeRoleVM changeVM)
        {
            try
            {
                var check = _myContext.Users.Find(changeVM.userId);
                if (check != null)
                {
                    var checkRole = _myContext.Roles.Find(changeVM.roleId);
                    if (checkRole == null)
                    {
                        throw new HttpResponseExceptionHelper(404, "Invalid role id");
                    }
                    check.roleId = changeVM.roleId;
                    _myContext.Entry(checkRole).State = EntityState.Detached;
                    _myContext.Entry(check).State = EntityState.Modified;
                    return _myContext.SaveChanges();
                }
                throw new HttpResponseExceptionHelper(404, "Invalid user id");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
