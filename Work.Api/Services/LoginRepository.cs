using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Work.Api.Data;
using Work.Api.Entities;
using Work.Api.Models;

namespace Work.Api.Services
{
    public class LoginRepository: ILoginRepository
    {
        private readonly WorkDbContext _context;

        public LoginRepository(WorkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> UserExistsAsync(string loginName, string loginPass)
        {
            if(loginName == string.Empty)
            {
                throw new ArgumentNullException(nameof(loginName));
            }
            if(loginPass == string.Empty)
            {
                throw new ArgumentNullException(nameof(loginPass));
            }
            return await _context.Users.AnyAsync(x => x.UserName == loginName && x.PassWord == loginPass);
        }

        public async Task<User> GetUserAsync(string loginName, string loginPass)
        {
            if (loginName == string.Empty)
            {
                throw new ArgumentNullException(nameof(loginName));
            }
            if (loginPass == string.Empty)
            {
                throw new ArgumentNullException(nameof(loginPass));
            }
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginName && x.PassWord == loginPass);
        }

        public async Task<IEnumerable<TokenModel>> GetTokenModelsAsync()
        {
            return await _context.TokenModels.ToListAsync();
        }

        public async Task<TokenModel> GetTokenModelAsync(int userId)
        {
            return await _context.TokenModels.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public List<RoleToUrl> GetRoleToUrl()
        {
            return  _context.Urls.Join(_context.LoginRoles, url => url.RoleId, role => role.Id, (url, role) => new RoleToUrl
            {
                Role = role.RoleName.ToString(),
                Url = url.LinkUrl
            }).ToList();
        }
    }
}
