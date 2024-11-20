using JobseekBerca.Models;

namespace JobseekBerca.ViewModels
{
    public class ProfileVM
    {
        public class CreateVM
        {
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? userId { get; set; }
        }
        public class GetVM
        {
            public string? userId { get; set; }
            public string? email { get; set; }

            public string? fullName { get; set; }
            public string? summary { get; set; }
            public string? phoneNumber { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? profileImage { get; set; }
            public string? linkPersonalWebsite { get; set; }
            public string? linkGithub { get; set; }
            public string? linkedin { get; set; }



        }
        public class UpdateVM
        {
            public string? userId { get; set; }
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public string? phoneNumber { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? linkPersonalWebsite {  get; set; }
            public string? profileImage { get; set; }
            public string? linkGithub {  get; set; }
            public string? linkedin { get; set; }

        }
        public class GetAllVM
        {
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? userId { get; set; }
        }
    }
}
