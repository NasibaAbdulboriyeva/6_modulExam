using UserContacts.Bll.Dtos;
using UserContacts.Dal.Entities;

namespace UserContacts.Bll.Services
{
    public interface IUserRoleService
    {
        Task<long> AddRoleAsync(UserRoleCreateDto role);
        Task<List<UserGetDto>> GetAllUsersByRoleAsync(string role);
        Task<List<UserRole>> GetAllRolesAsync();
        Task<long> GetRoleIdAsync(string role);
        Task DeleteRoleAsync(string role);
    }
}