using System.Threading.Tasks;
using Supershop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Supershop.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
