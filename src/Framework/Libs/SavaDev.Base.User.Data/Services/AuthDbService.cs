using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.User.Data.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public class AuthDbService : IAuthDbService
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;

        public AuthDbService(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;

            _mapper = mapper;
        }

        public async Task<AuthTokenModel> CreateToken(AuthTokenModel model)
        {
            var newEntity = _mapper.Map<AuthToken>(model);
            _dbContext.Set<AuthToken>().Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<AuthTokenModel>(newEntity);
        }

        public async Task<AuthTokenModel> GetTokenByRefreshJti(string refreshJti)
        {
            var token = await _dbContext.Set<AuthToken>().FirstOrDefaultAsync(m => m.RefreshJti == refreshJti);
            if (token == null) throw new Exception("refresh token not found"); // TODO: кидать типизированную ошибку
            return _mapper.Map<AuthTokenModel>(token);
        }

        public async Task<bool> CheckRefreshTokenExists(string refreshJti)
        {
            return await _dbContext.Set<AuthToken>().AnyAsync(m => m.RefreshJti == refreshJti);
        }

        public async Task UpdateToken(string refreshJti, string newAuthJti, string newRefreshJti, DateTime updated)
        {
            var token = await _dbContext.Set<AuthToken>().FirstOrDefaultAsync(m => m.RefreshJti == refreshJti);

            token.AuthJti = newAuthJti;
            token.RefreshJti = newRefreshJti;
            token.DateUpdated = updated;

            await _dbContext.SaveChangesAsync();
        }
    }
}
