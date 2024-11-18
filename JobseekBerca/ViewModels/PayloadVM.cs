namespace JobseekBerca.ViewModels
{
    public class PayloadVM
    {
        public class GenerateVM
        {
            public string? userId { get; set; }
            public string? roleName { get; set; }
            public string? email { get; set; }

        }
        public class RefreshVM
        {
            public string? email { get; set; }
        }
        public class ValidateVM
        {
            public string? token { get; set; }
        }
    }
}
