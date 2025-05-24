using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserContacts.Bll.Dtos;
using UserContacts.Core.Errors;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Bll.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ILogger<UserRoleService> _logger;
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;
        public UserRoleService(MainContext mainContext, IMapper mapper, ILogger<UserRoleService> logger)
        {
            _mainContext = mainContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<long> AddRoleAsync(UserRoleCreateDto role)
        {
            var roleEntity = _mapper.Map<UserRole>(role);
            _mainContext.UserRoles.Add(roleEntity);
            await _mainContext.SaveChangesAsync();
            return roleEntity.UserRoleId;
        }

        public async Task DeleteRoleAsync(string role)
        {
            var userRoleEntity = await _mainContext.UserRoles.FirstOrDefaultAsync(r => r.RoleName == role);
            _logger.LogInformation("Delete role with name:", userRoleEntity.RoleName);
            _mainContext.UserRoles.Remove(userRoleEntity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task<List<UserRole>> GetAllRolesAsync()
        {
            var roles = await _mainContext.UserRoles.ToListAsync();
            return roles;
        }

        public async Task<List<UserGetDto>> GetAllUsersByRoleAsync(string role)
        {
            var roleId = await GetRoleIdAsync(role);

            var users = await _mainContext.Users.Where(u => u.UserRoleId == roleId).ToListAsync();

            return _mapper.Map<List<UserGetDto>>(users);
        }

        public async Task<long> GetRoleIdAsync(string role)
        {
            var userRoleById = await _mainContext.UserRoles.FirstOrDefaultAsync(r => r.RoleName == role);
            if (userRoleById == null)
            {
                throw new EntityNotFoundException();
            }

            return userRoleById.UserRoleId;
        }
    }
}
