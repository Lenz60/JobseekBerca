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
        public IActionResult Validate(PayloadVM.ValidateVM validateVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(validateVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var token = validateVM.token;
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
                return ResponseHTTP.CreateResponse(401, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }
    }
}
