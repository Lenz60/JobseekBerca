﻿using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface ISavedJobsRepository
    {
        public IEnumerable<SavedJobVM.GetSaveJob> GetSavedJobs(string userId);
        public int CreateSavedJob(string userId, string jobId);
        public int DeleteSavedJob(SavedJobVM.DeleteSavedJobVM deleteVM);
    }
}
