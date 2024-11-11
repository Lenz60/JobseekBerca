using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IExperiencesRepository
    {
        public IEnumerable<Experiences> GetExperienceById(string userId);
        public int CreateExperience(Experiences experience);
        public int UpdateExperience(Experiences experience);
        public int DeleteExperience(DetailsVM.DeleteVM experience);
    }
}
