using JobseekBerca.Models;
using JobseekBerca.View;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        int UpdateUser(Users user);
        int DeleteUser(string userId);
        int Register(RegisterVM registervm);
        int Login(LoginVM loginvm)
    }
}
