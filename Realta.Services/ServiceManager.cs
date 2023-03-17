﻿using Realta.Domain.Base;
using Realta.Domain.Repositories;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPriceItemsPhotoService> _lazyPriceItemsPhotoService;
        private readonly Lazy<ICategoryGroupPhotoService> _lazyCategoryGroupPhotoService;
        private readonly Lazy<IUtilityService> _lazyUtilityService;
        public ServiceManager(IRepositoryManager repositoryManager, IUtilityService _lazyUtilityService)
        {
            _lazyPriceItemsPhotoService = new Lazy<IPriceItemsPhotoService>(()=> new PriceItemsPhotoService(repositoryManager,_lazyUtilityService));

            _lazyCategoryGroupPhotoService = new Lazy<ICategoryGroupPhotoService>(() => new CategoryGroupPhotoService(repositoryManager, _lazyUtilityService));

        }
        
        public IPriceItemsPhotoService PriceItemsPhotoService => _lazyPriceItemsPhotoService.Value;
        public ICategoryGroupPhotoService CategoryGroupPhotoService => _lazyCategoryGroupPhotoService.Value;
    }
}
