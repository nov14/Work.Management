using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Work.Api.Helpers;
using Work.Api.Helpers.AuthHelper;
using Work.Api.Models;
using Work.Api.Services;

namespace Work.Api.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [ApiController]
    [Route("api/login")]
    public class LoginController: ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly PermissionRequirement _permissionRequirement;

        public LoginController(ILoginRepository loginRepository, PermissionRequirement permissionRequirement)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _permissionRequirement = permissionRequirement ?? throw new ArgumentNullException(nameof(permissionRequirement));
        }

        [HttpGet]
        public async Task<object> GetJwtToken(string name="", string pass="")
        {
            string jwtStr = string.Empty;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                return new JsonResult(new 
                {
                    status = false,
                    message = "用户名或密码不能为空!"
                });
            }

            if (!await _loginRepository.UserExistsAsync(name, pass))
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "用户名或密码不正确"
                });
            }

            var user = await _loginRepository.GetUserAsync(name, pass);
            int userId = user.Id;
            TokenModel userModel = await _loginRepository.GetTokenModelAsync(userId);
            Role userRole = userModel.Role;

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", userId.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString())

            };
            

            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);

            var token = JwtHelper.BuildJwtToken(claims, _permissionRequirement);
            return new JsonResult(token);
        }

    }
}
