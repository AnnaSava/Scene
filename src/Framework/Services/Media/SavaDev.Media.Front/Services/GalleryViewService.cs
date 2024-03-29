﻿using AutoMapper;
using Sava.Media.Contract.Models;
using Sava.Media.Data.Contract;
using Sava.Media.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Media.Data.Contract.Models;
using SavaDev.Media.Front.Contract;

namespace SavaDev.Media.Front.Services
{
    public class GalleryViewService : IGalleryViewService
    {
        private readonly IGalleryService _galleryService;
        private readonly IMapper _mapper;

        public GalleryViewService(IGalleryService galleryService,
            IMapper mapper)
        {
            _galleryService = galleryService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<GalleryViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<GalleryModel, GalleryFilterModel>(_galleryService, _mapper);
            var vm = await manager.GetRegistryPage<GalleryViewModel>(query);
            return vm;
        }
    }
}
