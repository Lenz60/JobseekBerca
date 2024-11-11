namespace JobseekBerca.Helper
{
    public class HttpResponseExceptionHelper :Exception
    {
        public int StatusCode { get; }

        public HttpResponseExceptionHelper(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
