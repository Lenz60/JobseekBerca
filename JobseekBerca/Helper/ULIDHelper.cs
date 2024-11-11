namespace JobseekBerca.Helper
{
    public class ULIDHelper
    {
        public static string GenerateULID()
        {
            return Ulid.NewUlid().ToString();
        }
    }
}
