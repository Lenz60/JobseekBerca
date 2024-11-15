using JobseekBerca.Context;
using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using static JobseekBerca.ViewModels.ProfileVM;

namespace JobseekBerca.Repositories
{
    public class ProfilesRepository : IProfilesRepository
    {
        private readonly MyContext _myContext;
        public ProfilesRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public ProfileVM.GetVM GetProfile(string userId)
        {
            try
            {
                CheckUserId(userId);
                var checkRole = _myContext.Users
                    .Where(x => x.userId == userId)
                    .Select(u => u.roleId).FirstOrDefault();
                var profile = _myContext.Profiles.Find(userId);
                if (profile == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Profile not found");
                }
                var getProfile = new ProfileVM.GetVM
                {
                    fullName = profile.fullName,
                    summary = profile.summary,
                    phoneNumber = profile.phoneNumber,
                    gender = profile.gender,
                    address = profile.address,
                    birthDate = profile.birthDate,
                    profileImage = profile.profileImage
                };
                return getProfile;
                
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

        public int CreateProfile(ProfileVM.CreateVM create)
        {
            //throw new NotImplementedException();
            try
            {
                CheckUserId(create.userId);
                var profile = new Profiles
                {
                    userId = create.userId,
                    fullName = create.fullName,
                    summary = create.summary,
                    gender = create.gender,
                    address = create.address,
                    phoneNumber = create.address,
                    birthDate = create.birthDate
                };
                _myContext.Profiles.Add(profile);
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

        public int CheckUserId(string userId)
        {
            try
            {
                var check = _myContext.Users.Find(userId);
                if (check == null)
                {
                    throw new HttpResponseExceptionHelper(404, "User is not found");
                }
                return SUCCESS;
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

        public int UpdateProfile(UpdateVM update)
        {
            try
            {
                CheckUserId(update.userId);
                var profile = _myContext.Profiles.Find(update.userId);
                if (profile == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Profile is not found");
                }
                var newProfile = new Profiles
                {
                    userId = update.userId,
                    fullName = update.fullName,
                    summary = update.summary,
                    phoneNumber = update.phoneNumber,
                    gender = update.gender,
                    address = update.address,
                    birthDate = update.birthDate
                };
                _myContext.Entry(profile).State = EntityState.Detached;
                _myContext.Entry(newProfile).State = EntityState.Modified;
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
    }
}
