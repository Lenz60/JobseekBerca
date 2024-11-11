using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
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
            var check = CheckUserId(userId);
            if (check == SUCCESS)
            {
                var profile = _myContext.Profiles.Find(userId);
                var getProfile = new ProfileVM.GetVM
                {
                    fullName = profile.fullName,
                    summary = profile.summary,
                    gender = profile.gender,
                    address = profile.address,
                    birthDate = profile.birthDate
                };
                return getProfile;
            }
            return null;

        }

        public int CreateProfile(ProfileVM.CreateVM create)
        {
            //throw new NotImplementedException();
            var check = CheckUserId(create.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
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

                var add = _myContext.Profiles.Add(profile);
                if (add == null)
                {
                    return INTERNAL_ERROR;
                }
                return SUCCESS;

            }
        }

        public int CheckUserId(string userId)
        {
            //throw new NotImplementedException();
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                return FAIL;
            }
            return SUCCESS;
        }

        public int UpdateProfile(UpdateVM update)
        {
            var check = CheckUserId(update.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {

                var profile = _myContext.Profiles.Find(update.userId);
                var newProfile = new Profiles
                {
                    userId = update.userId,
                    fullName = update.fullName,
                    summary = update.summary,
                    gender = update.gender,
                    address = update.address,
                    birthDate = update.birthDate
                };
                _myContext.Entry(profile).State = EntityState.Detached;
                _myContext.Entry(newProfile).State = EntityState.Modified;
                return _myContext.SaveChanges();

            }
        }
    }
}
