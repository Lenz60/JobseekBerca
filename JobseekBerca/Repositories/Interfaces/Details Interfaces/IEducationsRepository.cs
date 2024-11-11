using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IEducationsRepository
    {
        public IEnumerable<Educations> GetEducationById(string userId);
        public int CreateEducation(Educations education);
        public int UpdateEducation(Educations education);
        public int DeleteEducation(DetailsVM.DeleteVM education);
    }
}
