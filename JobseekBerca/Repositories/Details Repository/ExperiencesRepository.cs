using JobseekBerca.Context;
using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories
{
    public class ExperiencesRepository : IExperiencesRepository
    {
        private readonly MyContext _myContext;

        public ExperiencesRepository(MyContext myContext)
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
                //return FAIL;
                throw new HttpResponseExceptionHelper(404, "User is not found");
            }
            return SUCCESS;
        }

        public int CreateExperience(Experiences experience)
        {
            try
            {
                CheckUserId(experience.userId);
                _myContext.Experiences.Add(experience);
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

        public int UpdateExperience(Experiences experience)
        {
            try
            {
                CheckUserId(experience.userId);
                var checkExperience = _myContext.Experiences.Find(experience.experienceId);
                if (checkExperience == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(404, "Invalid experience id");
                }
                var newExperience = new Experiences
                {
                    experienceId = experience.experienceId,
                    position = experience.position,
                    company = experience.company,
                    description = experience.description,
                    jobTypes = experience.jobTypes,
                    startDate = experience.startDate,
                    endDate = experience.endDate,
                    userId = experience.userId,
                };
                _myContext.Entry(checkExperience).State = EntityState.Detached;
                _myContext.Entry(newExperience).State = EntityState.Modified;
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

        public int DeleteExperience(DetailsVM.DeleteVM experience)
        {
            try
            {
                CheckUserId(experience.userId);
                var checkExperience = _myContext.Experiences.Find(experience.id);
                if (checkExperience == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(404, "Invalid experience id");
                }
                _myContext.Experiences.Remove(checkExperience);
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

        public IEnumerable<Experiences> GetExperienceById(string userId)
        {
            try
            {
                CheckUserId(userId);
                var experiences = _myContext.Experiences.Select(Experiences => new Experiences
                {
                    experienceId = Experiences.experienceId,
                    position = Experiences.position,
                    company = Experiences.company,
                    description = Experiences.description,
                    jobTypes = Experiences.jobTypes,
                    startDate = Experiences.startDate,
                    endDate = Experiences.endDate,
                    userId = Experiences.userId
                }).Where(x => x.userId == userId).ToList();
                if (experiences == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Experiences not found");
                    //return null;
                }
                return experiences;
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
