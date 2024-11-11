using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories.Details_Repository
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly MyContext _myContext;

        public SocialMediaRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public int CheckUserId(string userId)
        {
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                return FAIL;
            }
            return SUCCESS;
        }
        public int CreateSocialMedia(SocialMedias socialMedia)
        {
            var check = CheckUserId(socialMedia.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                _myContext.SocialMedias.Add(socialMedia);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }

        public int DeleteSocialMedia(DetailsVM.DeleteVM socialMedia)
        {
            var check = CheckUserId(socialMedia.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkSocialMedia = _myContext.SocialMedias.Find(socialMedia.id);
                if (checkSocialMedia == null)
                {
                    return FAIL;
                }
                _myContext.SocialMedias.Remove(checkSocialMedia);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }

        public int UpdateSocialMedia(SocialMedias socialMedia)
        {
            var check = CheckUserId(socialMedia.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkSosmed = _myContext.SocialMedias.Find(socialMedia.socialMediaId);
                if (checkSosmed == null)
                {
                    return FAIL;
                }
                var newSosmed = new SocialMedias
                {
                    socialMediaId = socialMedia.socialMediaId,
                    socialMediaName = socialMedia.socialMediaName,
                    socialMediaLink = socialMedia.socialMediaLink,
                    userId = socialMedia.userId
                };
                _myContext.Entry(checkSosmed).State = EntityState.Detached;
                _myContext.Entry(newSosmed).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }
        }

        public IEnumerable<SocialMedias> GetSocialMediasById(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                return null;
            }
            else
            {
                var sosmed = _myContext.SocialMedias.Select(Sosmed => new SocialMedias
                {
                    socialMediaId = Sosmed.socialMediaId,
                    socialMediaName = Sosmed.socialMediaName,
                    socialMediaLink = Sosmed.socialMediaLink,
                    userId = Sosmed.userId
                }).Where(Sosmed => Sosmed.userId == userId).ToList();
                if (sosmed == null)
                {
                    return null;
                }
                return sosmed;
            }
        }
    }
}
