using JobseekBerca.Helper;
using JobseekBerca.Repositories;
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
        public readonly UsersRepository _usersRepository;

        public SessionsController(IConfiguration config, UsersRepository usersRepository)
        {
            _config = config;
            _usersRepository = usersRepository;
        }

        [HttpPost("Validate")]
        public IActionResult Validate(PayloadVM.ValidateVM validateVM)
        {
            // Check the body if it has null or empty properties return error response
            if (Whitespace.HasNullOrEmptyStringProperties(validateVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                // Validate the token if its still good to use or not
                var token = validateVM.token;
                var payloadList = JWTHelper.Decode(token, _config);
                return ResponseHTTP.CreateResponse(200, payloadList);
            }
            catch (InvalidTokenPartsException e)
            {
                return ResponseHTTP.CreateResponse(403, "Invalid token");
            }
            catch(FormatException e)
            {
                return ResponseHTTP.CreateResponse(403, "Invalid token");
            }
            catch (TokenNotYetValidException e)
            {
                return ResponseHTTP.CreateResponse(403, e.Message);
            }
            catch (TokenExpiredException e)
            {
                // If token expired, refresh the token by checking the Refresh Token in database
                try
                {
                    var token = validateVM.token;
                    // Decode the token to get the payload
                    // Token decoded and returned as a dictionary object
                    var decodedToken = JWTHelper.DecodeAsObject(token, _config);
                    // Get the dictionary object and set it to the new Object
                    var payload = new PayloadVM.GenerateVM
                    {
                        userId = decodedToken["uid"].ToString(),
                        roleName = decodedToken["role"].ToString(),
                        email = decodedToken["email"].ToString()
                    };
                    // Check the token in database based on its userId
                    var refreshToken = _usersRepository.CheckRefreshToken(payload.userId);
                    // If the token is good to go then generate a new token for Access Token
                    var accessToken = _usersRepository.GenerateToken(payload);
                    return ResponseHTTP.CreateResponse(201, "Token refreshed", accessToken);
                }
                catch(HttpResponseExceptionHelper ex)
                {
                    // Return the error response if the Refresh Token is expired in database
                    return ResponseHTTP.CreateResponse(ex.StatusCode, ex.Message);
                }
            }
            catch (SignatureVerificationException e)
            {
                return ResponseHTTP.CreateResponse(403, e.Message);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }
    }
}
