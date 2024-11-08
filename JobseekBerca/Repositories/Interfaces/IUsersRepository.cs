using JobseekBerca.Models;
using JobseekBerca.ViewModels;


namespace JobseekBerca.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        int UpdateUser(Users user);
        int DeleteUser(string userId);
        int Register(UserVM.RegisterVM registervm);
        int Login(UserVM.RegisterVM loginvm);
    }
}
