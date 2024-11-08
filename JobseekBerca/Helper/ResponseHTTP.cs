using Microsoft.AspNetCore.Mvc;

namespace JobseekBerca.Helper
{
    public static class ResponseHTTP
    {
        public static IActionResult CreateResponse(int statusCode, string message, object data = null)
        {
            var response = new Dictionary<string, object>
            {
                { "statusCode", statusCode },
                { "message", message }
            };

            if (data != null)
            {
                response.Add("data", data);
            }

            if (statusCode >= 200 && statusCode < 300)
            {
                return new OkObjectResult(response);
            }
            else if (statusCode >= 400 && statusCode < 500)
            {
                return new BadRequestObjectResult(response);
            }
            else
            {
                return new ObjectResult(response)
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
