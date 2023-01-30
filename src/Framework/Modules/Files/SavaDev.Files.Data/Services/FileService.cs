using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Sava.Files.Data.Contract;
using Sava.Files.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Files.Data.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly FilesContext _dbContext;

        public FileService(FilesContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<FileModel> Create(FileModel model)
        {
            var entity = _mapper.Map<Entities.File>(model);
            entity.Id = new Guid();

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

        public async Task<PageListModel<FileModel>> GetAll(int page, int count)
        {
            var dbSet = _dbContext.Set<Entities.File>().AsNoTracking();

            var res = await dbSet.ProjectTo<FileModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<FileModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
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
