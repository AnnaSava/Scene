using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sava.Media.Data;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using Sava.Media.Data.Entities;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Media.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Files.Data.Services
{
    public class GalleryService : BaseHubEntityService<Guid, Gallery, GalleryModel>, IGalleryService
    {
        public GalleryService(MediaContext dbContext, IMapper mapper, ILogger<IGalleryService> logger) 
            : base (dbContext, mapper, logger)
        {
            AllSelector = new AllSelector<Guid, Gallery>(GetInftrastructure);
        }

        public async Task<RegistryPage<GalleryModel>> GetRegistryPage(RegistryQuery<GalleryFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<GalleryFilterModel, GalleryModel>(query);
            return page;
        }
    }
}
