using JobseekBerca.Models;
using JobseekBerca.ViewModels;


namespace JobseekBerca.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        int ChangePassword(UserVM.ChangePasswordVM changePassword);
        int Register(UserVM.RegisterVM registervm);
        int Login(Users users);
    }
}
