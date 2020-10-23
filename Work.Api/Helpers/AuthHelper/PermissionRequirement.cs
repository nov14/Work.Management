using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Work.Api.Models;

namespace Work.Api.Helpers.AuthHelper
{
    /// <summary>
    /// 授权参数类
    /// </summary>
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public List<TokenModel> TokenModels { get; set; }
        public string ClaimType { get; set; }
        public string LoginPath { get; set; } = "api/login";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public PermissionRequirement(List<TokenModel> tokenModels, string claimType, string issuer, string audience, TimeSpan expiration, SigningCredentials signingCredentials)
        {
            TokenModels = tokenModels;
            ClaimType = claimType;
            Issuer = issuer;
            Audience = audience;
            Expiration = expiration;
            SigningCredentials = signingCredentials;
        }
    }
}
