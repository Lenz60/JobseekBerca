using JobseekBerca.Helper;
using JobseekBerca.ViewModels;
using JWT.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobseekBerca.ViewModels.UserVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class SessionsController : ControllerBase
    {
        public readonly IConfiguration _config;

        public SessionsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Validate")]
        public IActionResult Validate([FromBody] PayloadVM.ValidateVM validateVM)
        {
            try
            {
                var token = validateVM.token;
                if (Whitespace.HasNullOrEmptyStringProperties(token, out string propertyName))
                {
                    return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
                }
                //HttpContext.Session.SetString("JWT", token);
                var payloadList = JWTHelper.Decode(token, _config);
                return ResponseHTTP.CreateResponse(200, payloadList);
            }
            catch (InvalidTokenPartsException e)
            {
                return ResponseHTTP.CreateResponse(401, e.Message);
            }
            catch (TokenNotYetValidException e)
            {
                return ResponseHTTP.CreateResponse(401, e.Message);
            }
            catch (TokenExpiredException e)
            {
                return ResponseHTTP.CreateResponse(401, e.Message);
            }
            catch (SignatureVerificationException e)
            {
                //return Unauthorized(new
                //{
                //    statusCode = StatusCodes.Status403Forbidden,
                //    message = "Signature changed or invalid",
                //    data = null as object,
                //});
                return ResponseHTTP.CreateResponse(401, e.Message);
            }
        }
    }
}
