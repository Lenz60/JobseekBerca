using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly MyContext _myContext;

        public SkillsRepository(MyContext myContext)
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

        public int CreateSkill(Skills skill)
        {
            var check = CheckUserId(skill.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                _myContext.Skills.Add(skill);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }

        public int UpdateSkill(Skills skill)
        {
            var check = CheckUserId(skill.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkSkill = _myContext.Skills.Find(skill.skillId);
                if (checkSkill == null)
                {
                    return FAIL;
                }
                var newSkill = new Skills
                {
                    skillId = skill.skillId,
                    skillName = skill.skillName,
                    userId = skill.userId
                };
                _myContext.Entry(checkSkill).State = EntityState.Detached;
                _myContext.Entry(newSkill).State = EntityState.Modified;
                return _myContext.SaveChanges();

            }
        }

        public int DeleteSkill(DetailsVM.DeleteVM skill)
        {
            var check = CheckUserId(skill.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkSkill = _myContext.Skills.Find(skill.id);
                if (checkSkill == null)
                {
                    return FAIL;
                }
                _myContext.Skills.Remove(checkSkill);
                return _myContext.SaveChanges();
            }
        }

        public IEnumerable<Skills> GetSkillById(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                return null;
            }
            else
            {
                var skills = _myContext.Skills.Select(Skills => new Skills
                {
                    skillId = Skills.skillId,
                    skillName = Skills.skillName,
                    userId = Skills.userId
                }).Where(x => x.userId == userId).ToList();
                if (skills == null)
                {
                    return null;
                }
                return skills;
            }
        }
    }
}
