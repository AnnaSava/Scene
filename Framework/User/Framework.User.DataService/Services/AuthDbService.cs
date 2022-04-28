using AutoMapper;
using Framework.Base.Exceptions;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class AuthDbService : IAuthDbService
    {
        protected readonly IAuthTokenContext _dbContext;
        protected readonly IMapper _mapper;

        public AuthDbService(IAuthTokenContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthDbService),
                nameof(dbContext),
                null);

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                nameof(AuthDbService),
                nameof(mapper),
                null);
        }

        public async Task<AuthTokenModel> CreateToken(AuthTokenModel model)
        {
            var newEntity = _mapper.Map<AuthToken>(model);
            _dbContext.AuthTokens.Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<AuthTokenModel>(newEntity);
        }

        public async Task<AuthTokenModel> GetTokenByRefreshJti(string refreshJti)
        {
            var token = await _dbContext.AuthTokens.FirstOrDefaultAsync(m => m.RefreshJti == refreshJti);
            if (token == null) throw new Exception("refresh token not found"); // TODO: кидать типизированную ошибку
            return _mapper.Map<AuthTokenModel>(token);
        }

        public async Task<bool> CheckRefreshTokenExists(string refreshJti)
        {
            return await _dbContext.AuthTokens.AnyAsync(m => m.RefreshJti == refreshJti);
        }

        public async Task UpdateToken(string refreshJti, string newAuthJti, string newRefreshJti, DateTime updated)
        {
            var token = await _dbContext.AuthTokens.FirstOrDefaultAsync(m => m.RefreshJti == refreshJti);

            token.AuthJti = newAuthJti;
            token.RefreshJti = newRefreshJti;
            token.DateUpdated = updated;

            await _dbContext.SaveChangesAsync();
        }
    }
}
