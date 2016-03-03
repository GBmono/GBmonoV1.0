﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Gbmono.EF.Models;
using Gbmono.EF.DataContext;

namespace Gbmono.EF.Infrastructure
{
    public class RepositoryManager
    {
        // db context
        public DbContext Context { get; private set; }

        #region ctors
        // default constructor
        public RepositoryManager()
        {
            Context = new GbmonoSqlContext(); // create new instance of sql context with default settings
        }

        // constructor with extra settings
        public RepositoryManager(int commandTimeout, bool enableAutoDetectChanges, bool enableValidateOnSave)
        {
            Context = new GbmonoSqlContext();

            Context.Configuration.AutoDetectChangesEnabled = enableAutoDetectChanges;
            Context.Configuration.ValidateOnSaveEnabled = enableValidateOnSave;

            // command timeout
            var objectContext = ((IObjectContextAdapter)Context).ObjectContext;

            objectContext.CommandTimeout = commandTimeout; // value in seconds
        }

        #endregion

        // entity repositories
        private IRepository<Category> _categoryRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductImage> _productImageRepository;
        private IRepository<ProductEvent> _productEventRepository;

        private IRepository<UserFavorite> _userFavoriteRepository;

        private IRepository<Brand> _brandRepository;
        private IRepository<Country> _countryRepository;
        private IRepository<Retailer> _retailerRepository;
        // private IRepository<Banner> _bannerRepository;

        // public accessors
        #region category & product
        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new Repository<Category>(Context)); }
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new Repository<Product>(Context)); }
        }

        public IRepository<ProductImage> ProductImageRepository
        {
            get { return _productImageRepository ?? (_productImageRepository = new Repository<ProductImage>(Context)); }
        }

        public IRepository<ProductEvent> ProductEventRepository
        {
            get { return _productEventRepository ?? (_productEventRepository = new Repository<ProductEvent>(Context)); }
        }
        #endregion

        public IRepository<UserFavorite> UserFavoriteRepository
        {
            get { return _userFavoriteRepository ?? (_userFavoriteRepository = new Repository<UserFavorite>(Context)); }

        }
        public IRepository<Brand> BrandRepository
        {
            get { return _brandRepository ?? (_brandRepository = new Repository<Brand>(Context)); }
        }

        public IRepository<Country> CountryRepository
        {
            get { return _countryRepository ?? (_countryRepository = new Repository<Country>(Context)); }
        }

        public IRepository<Retailer> RetailerRepository
        {
            get { return _retailerRepository ?? (_retailerRepository = new Repository<Retailer>(Context)); }
        }

        //public IRepository<Banner> BannerRepository
        //{
        //    get { return _bannerRepository ?? (_bannerRepository = new Repository<Banner>(Context)); }
        //}

    }
}
