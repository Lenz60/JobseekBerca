using JobseekBerca.Models;

namespace JobseekBerca.ViewModels
{
    public class UserVM
    {
        public class RegisterVM
        {
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? email { get; set; }
            public string? password { get; set; }
        }
        public class ChangePasswordVM
        {
            public string? userId { get; set; }
            public string? OldPassword { get; set; }
            public string? NewPassword { get; set; }
        }
    }
}
