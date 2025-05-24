using Microsoft.EntityFrameworkCore;
using UserContacts.Core.Errors;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Bll.Services
{
    public class RefreshTokenService : IRefreshTokenService

    {
        private readonly MainContext _mainContext;
        public RefreshTokenService(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            await _mainContext.RefreshTokens.AddAsync(refreshToken);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteRefreshToken(string refreshToken)
        {
            var token = await _mainContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (token == null)
            {
                throw new EntityNotFoundException("Refresh token not found ");
            }
            _mainContext.RefreshTokens.Remove(token);
            await _mainContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshToken(string refreshToken, long userId)
        {
            var tokens = await _mainContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
            return tokens;

        }
    }
}
