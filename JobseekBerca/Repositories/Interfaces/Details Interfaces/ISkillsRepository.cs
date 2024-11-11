using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface ISkillsRepository
    {
        public IEnumerable<Skills> GetSkillById(string userId);
        public int CreateSkill(Skills skill);
        public int UpdateSkill(Skills skill);
        public int DeleteSkill(DetailsVM.DeleteVM skill);
    }
}
