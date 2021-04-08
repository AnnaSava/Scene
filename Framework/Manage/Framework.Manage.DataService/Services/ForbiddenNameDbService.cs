using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Exceptions;
using Framework.Manage.DataService.Contract.Interfaces;
using Framework.Manage.DataService.Contract.Models;
using Framework.Manage.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Manage.DataService.Services
{
    public class ForbiddenNameDbService : IForbiddenNameDbService
    {
        private readonly IForbiddenNameContext _dbContext;
        private readonly IMapper _mapper;

        public ForbiddenNameDbService(IForbiddenNameContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ForbiddenNameModel> Create(ForbiddenNameModel model)
        {
            var entity = _mapper.Map<ForbiddenName>(model);
            _dbContext.ForbiddenNames.Add(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ForbiddenNameModel>(entity);
        }

        public async Task<string> Remove(string text)
        {
            var entity = _dbContext.ForbiddenNames.FirstOrDefault(m => m.Text == text);
            _dbContext.ForbiddenNames.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return text;
        }

        public async Task<ForbiddenNameModel> Update(ForbiddenNameModel model)
        {
            var currentEntity = await GetEntityByText(model.Text);

            _mapper.Map(model, currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ForbiddenNameModel>(currentEntity);
        }

        public async Task<IEnumerable<ForbiddenNameModel>> GetAll()
        {
            return await _dbContext.ForbiddenNames.ProjectTo<ForbiddenNameModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> CheckExists(string text)
        {
            var normalizedText = text.ToLower();
            return await _dbContext.ForbiddenNames
                  .Select(m => m.Text)
                  .Union(_dbContext.ForbiddenNames
                  .Where(m => m.IncludePlural)
                  .Select(m => m.Text + "s"))
                  .AnyAsync(m => m == normalizedText);
        }

        /// <summary>
        /// Поиск запрещенного наименования по тексту
        /// </summary>
        /// <param name="text">Наименование для поиска</param>
        /// <returns>Возвращает Task of <see cref="ForbiddenName"/></returns>
        protected async Task<ForbiddenName> GetEntityByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(GetEntityByText),
                    nameof(text),
                    text);

            var entity = await _dbContext.ForbiddenNames
                .FirstOrDefaultAsync(m => m.Text == text);

            if (entity == null)
                throw new EntityNotFoundException(
                          GetType(),
                          nameof(GetEntityByText),
                          typeof(ForbiddenName).FullName,
                          text);
            return entity;
        }
    }
}
