using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sava.Media.Data;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using Sava.Media.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Files.Data.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IMapper _mapper;
        private readonly MediaContext _dbContext;

        private readonly HubEntityManager<Guid, Gallery, GalleryModel> entityManager;

        public GalleryService(MediaContext dbContext, IMapper mapper, ILogger<IGalleryService> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;

            entityManager = new HubEntityManager<Guid, Gallery, GalleryModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<GalleryModel>> Create(GalleryModel model) => await entityManager.Create(model);

        public async Task<GalleryModel> GetOne(Guid id) => await entityManager.GetOne<GalleryModel>(id);

        public async Task<PageListModel<GalleryModel>> GetAll(int page, int count)
        {
            var dbSet = _dbContext.Set<Gallery>().AsNoTracking();

            var res = await dbSet.ProjectTo<GalleryModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<GalleryModel>()
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
