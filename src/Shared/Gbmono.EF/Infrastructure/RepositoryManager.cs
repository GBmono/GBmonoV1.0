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
        private IRepository<Tag> _tagRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductImage> _productImageRepository;
        private IRepository<ProductTag> _productTagRepository;

        private IRepository<Brand> _brandRepository;
        private IRepository<BrandCollection> _brandCollectionRepository;

        private IRepository<Article> _articleRepository;
        private IRepository<ArticleTag> _articleTagRepository;
        private IRepository<ArticleImage> _articleImageRepository;

        private IRepository<Retailer> _retailerRepository;
        private IRepository<RetailerShop> _retailerShopRepository;
        private IRepository<City> _cityRepository;
        private IRepository<State> _stateRepository;

        private IRepository<UserProduct> _userProductRepository;
        private IRepository<UserArticle> _userArticleRepository;
        private IRepository<UserVisit> _userVisitRepository;

        // public accessors
        #region category & product
        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new Repository<Category>(Context)); }
        }
        public IRepository<BrandCollection> BrandCollectionRepository
        {
            get { return _brandCollectionRepository ?? (_brandCollectionRepository = new Repository<BrandCollection>(Context)); }
        }

        public IRepository<Tag> TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new Repository<Tag>(Context)); }
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new Repository<Product>(Context)); }
        }

        public IRepository<ProductImage> ProductImageRepository
        {
            get { return _productImageRepository ?? (_productImageRepository = new Repository<ProductImage>(Context)); }
        }

        public IRepository<ProductTag> ProductTagRepository
        {
            get { return _productTagRepository ?? (_productTagRepository = new Repository<ProductTag>(Context)); }
        }

        #endregion

        #region brands
        public IRepository<Brand> BrandRepository
        {
            get { return _brandRepository ?? (_brandRepository = new Repository<Brand>(Context)); }
        }
        #endregion

        #region articles
        public IRepository<Article> ArticleRepository
        {
            get { return _articleRepository ?? (_articleRepository = new Repository<Article>(Context)); }
        }

        public IRepository<ArticleImage> ArticleImageRepository
        {
            get { return _articleImageRepository ?? (_articleImageRepository = new Repository<ArticleImage>(Context)); }
        }

        public IRepository<ArticleTag> ArticleTagRepository
        {
            get { return _articleTagRepository ?? (_articleTagRepository = new Repository<ArticleTag>(Context)); }
        }
        #endregion

        #region retailer & shops

        public IRepository<Retailer> RetailerRepository
        {
            get { return _retailerRepository ?? (_retailerRepository = new Repository<Retailer>(Context)); }
        }

        public IRepository<RetailerShop> RetailerShopRepository
        {
            get { return _retailerShopRepository ?? (_retailerShopRepository = new Repository<RetailerShop>(Context)); }
        }

        #endregion

        #region state & city
        public IRepository<City> CityRepository
        {
            get { return _cityRepository ?? (_cityRepository = new Repository<City>(Context)); }
        }

        public IRepository<State> StateRepository
        {
            get { return _stateRepository ?? (_stateRepository = new Repository<State>(Context)); }
        }

        #endregion

        #region users
        public IRepository<UserProduct> UserProductRepository
        {
            get { return _userProductRepository ?? (_userProductRepository = new Repository<UserProduct>(Context)); }
        }

        public IRepository<UserArticle> UserArticleRepository
        {
            get { return _userArticleRepository ?? (_userArticleRepository = new Repository<UserArticle>(Context)); }
        }

        public IRepository<UserVisit> UserVisitRepository
        {
            get { return _userVisitRepository ?? (_userVisitRepository = new Repository<UserVisit>(Context)); }
        }
        #endregion
    }
}
