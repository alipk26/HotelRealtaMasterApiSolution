﻿using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Repositories;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IRegionsRepository _regionsRepository;
        private ICountryRepository _countryRepository;
        private IProvincesRepository _provincesRepository;
        private IAddressRepository _addressRepository;
        private IMembersRepository _membersRepository;
        private IService_TaskRepository _service_TaskRepository;
        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }



        public IRegionsRepository RegionRepository
        {
            get
            {
                if (_regionsRepository == null)
                {
                    _regionsRepository = new RegionsRepository(_adoContext);
                }
                return _regionsRepository;
            }
        }

        public ICountryRepository CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                {
                    _countryRepository = new CountryRepository(_adoContext);
                }
                return _countryRepository;
            }
        }

        public IProvincesRepository ProvincesRepository
        {
            get
            {
                if (_provincesRepository == null)
                {
                    _provincesRepository = new ProvincesRepository(_adoContext);
                }
                return _provincesRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                {
                    _addressRepository = new AddressRepository(_adoContext);
                }
                return _addressRepository;
            }
        }

        public IMembersRepository MembersRepository
        {
            get
            {
                if (_membersRepository == null)
                {
                    _membersRepository = new MembersRepository(_adoContext);
                }
                return _membersRepository;
            }
        }

        public IService_TaskRepository service_TaskRepository
        {
            get
            {
                if (_service_TaskRepository == null)
                {
                    _service_TaskRepository = new Service_TaskRepository(_adoContext);
                }
                return _service_TaskRepository;
            }
        }
    }
}
