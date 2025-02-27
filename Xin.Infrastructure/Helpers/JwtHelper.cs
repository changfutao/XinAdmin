using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Xin.Infrastructure.Helpers
{
    public class JwtHelper
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string CreateToken(List<Claim> claims)
        {
            var jwtSetting = AppSettings.Get<JwtSettings>("JwtSettings");
            var authClaims = new List<Claim>()
            {
                new Claim("Audience", jwtSetting.Audience),
                new Claim("Issuer", jwtSetting.Issuer),
            };

            authClaims.AddRange(claims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));
            var handler = new JwtSecurityTokenHandler();
            var securityToken = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSetting.Expire),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                notBefore: DateTime.Now
                );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Issuer = jwtSetting.Issuer,
            //    Audience = jwtSetting.Audience,
            //    Expires =DateTime.Now.AddMinutes(jwtSetting.Expire),
            //    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            //};
        }
    }
}
