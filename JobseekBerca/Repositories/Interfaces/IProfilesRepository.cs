using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IProfilesRepository
    {
        int CreateProfile(ProfileVM.CreateVM create);
        ProfileVM.GetVM GetProfile(string userId);
        int UpdateProfile(ProfileVM.UpdateVM update);

    }
}
