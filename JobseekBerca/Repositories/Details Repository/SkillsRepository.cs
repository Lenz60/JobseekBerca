﻿using JobseekBerca.Context;
using JobseekBerca.Helper;
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
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                throw new HttpResponseExceptionHelper(404, "User not found");
            }
            return SUCCESS;
        }

        public int CreateSkill(Skills skill)
        {
            try
            {
                CheckUserId(skill.userId);
                _myContext.Skills.Add(skill);
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

        public int UpdateSkill(Skills skill)
        {
            try
            {
                CheckUserId(skill.userId);
                var checkSkill = _myContext.Skills.Find(skill.skillId);
                if (checkSkill == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Invalid skill id");
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
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int DeleteSkill(DetailsVM.DeleteVM skill)
        {
            try
            {
                CheckUserId(skill.userId);
                var checkSkill = _myContext.Skills.Find(skill.id);
                if (checkSkill == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Invalid skill id");
                    //return FAIL;
                }
                _myContext.Skills.Remove(checkSkill);
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

        public IEnumerable<Skills> GetSkillById(string userId)
        {
            try
            {
                CheckUserId(userId);
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
