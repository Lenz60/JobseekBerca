using JobseekBerca.Models;
using JobseekBerca.ViewModels;


namespace JobseekBerca.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        int ChangePassword(UserVM.ChangePasswordVM changePassword);
        int Register(UserVM.RegisterVM registervm);
        UserVM.CheckGoogleVM CheckGoogleUser(string userId);
        int RegisterGoogle(UserVM.RegisterGoogleVM registervm);
        int Login(UserVM.LoginVM login);
        int LoginGoogle(UserVM.LoginGoogleVM login);
        PayloadVM.GenerateVM GetCredsByEmail(string email);
        string GenerateToken(PayloadVM.GenerateVM payload);
    }
}
