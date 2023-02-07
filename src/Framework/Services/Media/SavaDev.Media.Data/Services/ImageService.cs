using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sava.Media.Data;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using Sava.Media.Data.Entities;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Files.Data.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly MediaContext _dbContext;

        private readonly BaseRestorableEntityService<Guid, Image, ImageModel> entityManager;

        public ImageService(MediaContext dbContext, IMapper mapper, ILogger<ImageService> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;

            entityManager = new BaseRestorableEntityService<Guid, Image, ImageModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult> Create(ImageModel model)
        {
            var res = await entityManager.Create(model,
                entity => entity.Id = new Guid());

            return res;
        }

        public async Task<ImageModel> GetOne(Guid id) => await entityManager.GetOne<ImageModel>(id);

        public async Task<PageListModel<ImageModel>> GetAll(int page, int count)
        {
            var dbSet = _dbContext.Set<Image>().AsNoTracking();

            var res = await dbSet.ProjectTo<ImageModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<ImageModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
        }

        // TODO подумать, как вообще лучше подгружать галереи, прикрепленные к блоговым постам
        public async Task<PageListModel<ImageModel>> GetAllByGallery(Guid galleryId)
        {
            var dbSet = _dbContext.Set<Image>().AsNoTracking().AsQueryable();

            dbSet = dbSet.Where(m => m.GalleryId == galleryId);


            // TODO заменить литералы
            var res = await dbSet.ProjectTo<ImageModel>(_mapper.ConfigurationProvider).ToPagedListAsync(1, 1000);

            var pageModel = new PageListModel<ImageModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
        }
    }
}
