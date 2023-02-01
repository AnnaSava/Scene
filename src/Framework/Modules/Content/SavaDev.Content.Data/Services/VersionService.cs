﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Savadev.Content.Data.Contract;
using Savadev.Content.Data.Contract.Models;
using Savadev.Content.Data.Services.Filters;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;

namespace Savadev.Content.Data.Services
{
    public class VersionService: IVersionService
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;
        private readonly ILogger<VersionService> _logger;

        private readonly ContentEntityManager<Entities.Version, VersionModel, VersionFilterModel> entityManager;

        public VersionService(ContentContext dbContext, IMapper mapper, ILogger<VersionService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

            entityManager = new ContentEntityManager<Entities.Version, VersionModel, VersionFilterModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<VersionModel>> Create<T>(VersionModel model, T contentModel) => await entityManager.Create(model, contentModel);

        public async Task<ItemsPage<VersionModel>> GetAll(RegistryQuery<VersionFilterModel> query)
        {
            var page = await entityManager.GetAll(query, ApplyFilters);

            return page;
        }

        private IQueryable<Entities.Version> ApplyFilters(IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            return list.ApplyFilters(filter);
        }
    }
}