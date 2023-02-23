using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this USerTokens userAccount, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Email, userAccount.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };

            if(userAccount.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            } else if(userAccount.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "USer 1"));
            }

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this USerTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static USerTokens GenTokenKey(USerTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new USerTokens();
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //obtain SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                //Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //VAlidity of our token
                userToken.validity = expireTime.TimeOfDay;

                // GENERATE OUR TOKEN
                var jwTokwn = new JwtSecurityToken(
                        issuer: jwtSettings.validIssuer,
                        audience: jwtSettings.validAudience,
                        claims: GetClaims(model, out Id),
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(expireTime).DateTime,
                        signingCredentials: new SigningCredentials(
                                new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256
                            )
                 );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken( jwTokwn );
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidID = Id;

                return userToken;
            }
            catch (Exception exception)
            {
                throw new Exception("Error Generating the JWT", exception);
            }
        }

    }
}
