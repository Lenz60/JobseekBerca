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
    }
}
