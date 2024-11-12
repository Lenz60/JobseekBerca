using JobseekBerca.Context;
using JobseekBerca.Helper;
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
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                //return FAIL;
                throw new HttpResponseExceptionHelper(404, "User is not found");
            }
            return SUCCESS;
        }

        public int CreateEducation(Educations education)
        {

            try
            {
                CheckUserId(education.userId);
                _myContext.Educations.Add(education);
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

        public int UpdateEducation(Educations education)
        {
            try
            {
                CheckUserId(education.userId);
                var checkEducation = _myContext.Educations.Find(education.educationId);
                if (checkEducation == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(403, "Invalid education id");
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
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int DeleteEducation(DetailsVM.DeleteVM education)
        {
            try
            {
                CheckUserId(education.userId);
                var checkEducation = _myContext.Educations.Find(education.id);
                if (checkEducation == null)
                {
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(403, "Invalid education id");
                }
                _myContext.Educations.Remove(checkEducation);
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

        public IEnumerable<Educations> GetEducationById(string userId)
        {
            try
            {
                CheckUserId(userId);
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
                    //return null;
                    throw new HttpResponseExceptionHelper(404, "There is no education");
                }
                return educations;

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
