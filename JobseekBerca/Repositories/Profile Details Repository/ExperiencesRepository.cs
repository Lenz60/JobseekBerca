using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;

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
    }
}
