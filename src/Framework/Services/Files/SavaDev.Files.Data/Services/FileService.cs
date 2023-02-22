using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Files.Data.Contract;
using SavaDev.Files.Data.Contract.Models;
using X.PagedList;

namespace SavaDev.Files.Data.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly FilesContext _dbContext;

        protected readonly AllSelector<Guid, Entities.File> selectorManager;

        public FileService(FilesContext dbContext, IMapper mapper, ILogger<FileService> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;

            selectorManager = new AllSelector<Guid, Entities.File>(dbContext, mapper, logger);
        }

        public async Task<FileModel> Create(FileModel model)
        {
            var entity = _mapper.Map<Entities.File>(model);
            entity.Id = new Guid();
            entity.Ext = "";// TODO

            _dbContext.Files.Add(entity);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var t = ex.Message;
            }

            return _mapper.Map<FileModel>(entity);
        }

        public async Task<FileModel> GetOne(Guid id)
        {
            var entity = await _dbContext.Files.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<FileModel>(entity);
        }

        public async Task<RegistryPage<FileModel>> GetRegistryPage(RegistryQuery<FileFilterModel> query)
        {
            var page = await selectorManager.GetRegistryPage<FileFilterModel, FileModel>(query);
            return page;
        }

        public async Task<bool> AnyByMd5(string md5hash)
        {
            var found = await _dbContext.Files.AnyAsync(m => m.Md5 == md5hash);
            return found;
        }

        public async Task<IEnumerable<FileModel>> GetAllByMd5(string md5hash)
        {
            var found = _dbContext.Files.Where(m => m.Md5 == md5hash)
                .ProjectTo<FileModel>(_mapper.ConfigurationProvider);

            return await found.ToListAsync();
        }

        public async Task<bool> AnyBySha1(string sha1hash)
        {
            var found = await _dbContext.Files.AnyAsync(m => m.Sha1 == sha1hash);
            return found;
        }
    }
}
