using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories
{
    public class EducationsRepository : IEducationsRepository
    {
        private readonly MyContext _myContext;

        public EducationsRepository(MyContext myContext)
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

        public int CreateEducation(Educations education)
        {
            var check = CheckUserId(education.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                _myContext.Educations.Add(education);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }

        public int UpdateEducation(Educations education)
        {
            var check = CheckUserId(education.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkEducation = _myContext.Educations.Find(education.educationId);
                if (checkEducation == null)
                {
                    return FAIL;
                }
                var newEducation = new Educations
                {
                    userId = education.userId,
                    educationId = education.educationId,
                    universityName = education.universityName,
                    degree = education.degree,
                    description = education.description,
                    gpa = education.gpa,
                    startDate = education.startDate,
                    endDate = education.endDate
                };
                _myContext.Entry(checkEducation).State = EntityState.Detached;
                _myContext.Entry(newEducation).State = EntityState.Modified;
                return _myContext.SaveChanges();

            }
        }

        public int DeleteEducation(DetailsVM.DeleteVM education)
        {
            var check = CheckUserId(education.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkEducation = _myContext.Educations.Find(education.id);
                if (checkEducation == null)
                {
                    return FAIL;
                }
                _myContext.Educations.Remove(checkEducation);
                return _myContext.SaveChanges();
            }
        }

        public IEnumerable<Educations> GetEducationById(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                return null;
            }
            else
            {
                var educations = _myContext.Educations.Select(Educations => new Educations
                {
                    userId = Educations.userId,
                    educationId = Educations.educationId,
                    universityName = Educations.universityName,
                    degree = Educations.degree,
                    description = Educations.description,
                    gpa = Educations.gpa,
                    startDate = Educations.startDate,
                    endDate = Educations.endDate
                }).Where(x => x.userId == userId).ToList();
                if (educations == null)
                {
                    return null;
                }
                return educations;
            }
        }
    }
}
