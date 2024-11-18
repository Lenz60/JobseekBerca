using JobseekBerca.ViewModels;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JobseekBerca.Helper
{
    public class JWTHelper
    {
        
        public static string GenerateToken(PayloadVM.GenerateVM payload, IConfiguration config)
        {
            var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var key = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var issuedAt = DateTime.UtcNow;
            var expires = issuedAt.AddHours(1); // Token expires in 1 hour

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:API"],
                claims: new[]
                {
                    new System.Security.Claims.Claim("uid", payload.userId),
                    new System.Security.Claims.Claim("role", payload.roleName),
                    new System.Security.Claims.Claim("email", payload.email),
                },
                expires: expires,
                notBefore: issuedAt,
                signingCredentials: key
            );
            //return token;
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenResult;
        }

        public static string GenerateRefreshToken(PayloadVM.RefreshVM payload, IConfiguration config)
        {
            var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var key = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var issuedAt = DateTime.UtcNow;
            var expires = issuedAt.AddHours(720); // Token expires in 30 days

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:API"],
                claims: new[]
                {
                    new System.Security.Claims.Claim("email", payload.email),
                },
                expires: expires,
                notBefore: issuedAt,
                signingCredentials: key
            );
            //return token;
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenResult;
        }

        public static bool ValidateToken(string token, IConfiguration config)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var key = config["Jwt:Key"];

            var payload = decoder.Decode(token, key, verify: true);
            if (payload != null){
                return true;
            }
            return false;

           
            //return payload;
        }
        
        public static string Decode(string token, IConfiguration config)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var key = config["Jwt:Key"];

            var payload = decoder.Decode(token, key, verify: true);
            return payload;
        }

        public static Dictionary<string, object> DecodeAsObject(string token, IConfiguration config, bool skipVerification = false)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var key = config["Jwt:Key"];

            var payloadJson = decoder.Decode(token, key, verify: false);
            var payload = JsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);
            return payload;
        }


    }
}
