using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Work.Api.Helpers.AuthHelper;
using Work.Api.Models;

namespace Work.Api.Helpers
{
    public class JwtHelper
    {
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.Now;

            //实例化JwtSecurityToken
            SecurityToken jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                signingCredentials: permissionRequirement.SigningCredentials,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration)
                );

            //生成token
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //打包返回前台
            var responseJson = new 
            {
                success = true,
                token = encodeJwt,
                expires_in = permissionRequirement.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return responseJson;
        }

        //Jwt解析
        public static TokenModel SerializeJwt(string jwtStr)
        {
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtStr);
            object userId = new object();
            object role = new object();
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtToken.Payload.TryGetValue("UserId", out userId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModel
            {
                Role = (Role)Enum.Parse(typeof(Role), role.ToString()),
                UserId = Convert.ToInt32(userId),
            };
            return tm;
        }
    }
}
