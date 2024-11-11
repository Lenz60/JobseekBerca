using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface ISocialMediaRepository
    {
        public IEnumerable<SocialMedias> GetSocialMediasById(string userId);
        public int CreateSocialMedia(SocialMedias socialMedia);
        public int UpdateSocialMedia(SocialMedias socialMedia);
        public int DeleteSocialMedia(DetailsVM.DeleteVM socialMedia);
    }
}
