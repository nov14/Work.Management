using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;
using Work.Api.Models;

namespace Work.Api.Services
{
    public interface ILoginRepository
    {
        Task<bool> UserExistsAsync(string loginName, string loginPass);
        Task<User> GetUserAsync(string loginName, string loginPass);

        Task<IEnumerable<TokenModel>> GetTokenModelsAsync();
        Task<TokenModel> GetTokenModelAsync(int userId);
        List<RoleToUrl> GetRoleToUrl();
    }
}
