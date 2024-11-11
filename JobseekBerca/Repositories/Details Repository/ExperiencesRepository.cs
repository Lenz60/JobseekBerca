using JobseekBerca.Context;
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
            var check = _myContext.Profiles.Find(userId);
            if (check == null)
            {
                return FAIL;
            }
            return SUCCESS;
        }

        public int CreateExperience(Experiences experience)
        {
            var check = CheckUserId(experience.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                _myContext.Experiences.Add(experience);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }

        public int UpdateExperience(Experiences experience)
        {
            var check = CheckUserId(experience.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkExperience = _myContext.Experiences.Find(experience.experienceId);
                if (checkExperience == null)
                {
                    return FAIL;
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
        }

        public int DeleteExperience(DetailsVM.DeleteVM experience)
        {
            var check = CheckUserId(experience.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkExperience = _myContext.Experiences.Find(experience.id);
                if (checkExperience == null)
                {
                    return FAIL;
                }
                _myContext.Experiences.Remove(checkExperience);
                return _myContext.SaveChanges();
            }
        }

        public IEnumerable<Experiences> GetExperienceById(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                return null;
            }
            else
            {
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
                    return null;
                }
                return experiences;
            }
        }
    }
}
