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
        public class RegisterGoogleVM
        {
            public string? oauthId { get; set; }
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? profileImage { get; set; }
            public string? email { get; set; }
            public string? password { get; set; }
        }
        public class LoginVM
        {
            public string? email { get; set; }
            public string? password { get; set; }
        }
        public class LoginGoogleVM
        {
            public string? oauthId { get; set; }
            public string? email { get; set; }
        }
        public class ChangePasswordVM
        {
            public string? userId { get; set; }
            public string? oldPassword { get; set; }
            public string? newPassword { get; set; }
        }
    }
}
