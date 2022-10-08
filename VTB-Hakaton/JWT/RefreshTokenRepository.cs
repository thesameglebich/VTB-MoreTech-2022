using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;

namespace WebApi.JWT
{
    public class RefreshTokenRepository
    {
        private readonly DB _ctx;

        public RefreshTokenRepository(DB ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateAsync(RefreshToken token)
        {
            _ctx.RefreshTokens.Add(token);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var token = _ctx.RefreshTokens.SingleOrDefault(t => t.Id == id);
            if (token == null)
                return;

            _ctx.Remove(token);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(int userId)
        {
            _ctx.RefreshTokens.RemoveRange(_ctx.RefreshTokens.Where(t => t.UserId == userId).ToList());
            await _ctx.SaveChangesAsync();
        }

        public RefreshToken FindById(Guid id)
        {
            return _ctx.RefreshTokens.SingleOrDefault(t => t.Id == id);
        }

        public RefreshToken FindByToken(string token)
        {
            return _ctx.RefreshTokens.SingleOrDefault(t => t.Token == token);
        }
    }
}
