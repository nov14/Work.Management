using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Work.Api.Entities;
using Work.Api.Models;
using Work.Api.Services;

namespace Work.Api.Helpers.AuthHelper
{
    /// <summary>
    /// token授权处理器
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILoginRepository _loginRepository;
        public IAuthenticationSchemeProvider Schemes { get; set; }

        public PermissionHandler(IAuthenticationSchemeProvider schemes, IHttpContextAccessor accessor, ILoginRepository loginRepository)
        {
            Schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //获取不同Role所对应的Url
            var roleToUrls = new List<RoleToUrl>();
            {
                roleToUrls = _loginRepository.GetRoleToUrl();
            }
            var httpContext = _accessor.HttpContext;
            if(httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();
                httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });

                //判断当前是否需要远程验证, 如果是就进行远程验证
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach(var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
                    if(handler != null && await handler.HandleRequestAsync())
                    {
                        return;
                    }
                }

                //判断是否登录
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if(defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if(result?.Principal != null)
                    {
                        httpContext.User = result.Principal;
                        //获取当前角色信息
                        var currentRoles = new List<string>();
                        {
                            currentRoles = (from item in httpContext.User.Claims
                                            where item.Type == requirement.ClaimType
                                            select item.Value).ToList();
                        }
                        var perssionRoles = roleToUrls.Where(x => currentRoles.Contains(x.Role.ToString()));
                        bool isMatchRole = false;
                        foreach(var item in perssionRoles)
                        {
                            //var tokenHeader = httpContext.Request.Headers["Authorization"];
                            //tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();
                            //TokenModel tm = JwtHelper.SerializeJwt(tokenHeader);
                            try
                            {
                                if(Regex.Match(questUrl, item.Url?.ToString().Trim().ToLower())?.Value == questUrl)
                                {
                                    isMatchRole = true;
                                    break;
                                }
                            }
                            catch(Exception)
                            {

                            }
                        }
                        if(currentRoles.Count <= 0 || !isMatchRole)
                        {
                            context.Fail();
                            return;
                        }

                        bool isExp = false;
                        {
                            isExp = (httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Expiration)?.Value) != null &&
                                DateTime.Parse(httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now;
                        }
                        if(isExp)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                    }
                }
            }
        }
    }
}
