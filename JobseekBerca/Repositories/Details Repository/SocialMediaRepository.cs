using JobseekBerca.Context;
using JobseekBerca.Helper;
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
                throw new HttpResponseExceptionHelper(404, "User not found");
            }
            return SUCCESS;
        }
        public int CreateSocialMedia(SocialMedias socialMedia)
        {
            try
            {
                CheckUserId(socialMedia.userId);
                _myContext.SocialMedias.Add(socialMedia);
                _myContext.SaveChanges();
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

        public int DeleteSocialMedia(DetailsVM.DeleteVM socialMedia)
        {
            try
            {
                CheckUserId(socialMedia.userId);
                var checkSocialMedia = _myContext.SocialMedias.Find(socialMedia.id);
                if (checkSocialMedia == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(404, "Invalid social media id");
                }
                _myContext.SocialMedias.Remove(checkSocialMedia);
                _myContext.SaveChanges();
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

        public int UpdateSocialMedia(SocialMedias socialMedia)
        {
            try
            {
                CheckUserId(socialMedia.userId);
                var checkSosmed = _myContext.SocialMedias.Find(socialMedia.socialMediaId);
                if (checkSosmed == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(404, "Invalid social media id");
                }
                var newSosmed = new SocialMedias
                {
                    socialMediaId = socialMedia.socialMediaId,
                    name = socialMedia.name,
                    link = socialMedia.link,
                    userId = socialMedia.userId
                };
                _myContext.Entry(checkSosmed).State = EntityState.Detached;
                _myContext.Entry(newSosmed).State = EntityState.Modified;
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

        public IEnumerable<SocialMedias> GetSocialMediasById(string userId)
        {
            try
            {
                CheckUserId(userId);
                var sosmed = _myContext.SocialMedias.Select(Sosmed => new SocialMedias
                {
                    socialMediaId = Sosmed.socialMediaId,
                    name = Sosmed.name,
                    link = Sosmed.link,
                    userId = Sosmed.userId
                }).Where(Sosmed => Sosmed.userId == userId).ToList();
                if (sosmed == null)
                {
                    //return null;
                    throw new HttpResponseExceptionHelper(404, "Social media not found");
                }
                return sosmed;

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
