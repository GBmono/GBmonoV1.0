using System.Data.Entity;
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
        private IRepository<ProductEvent> _productEventRepository;
        private IRepository<ProductTag> _productTagRepository;
        private IRepository<UserFavorite> _userFavoriteRepository;

        private IRepository<Brand> _brandRepository;
        
        private IRepository<Retailer> _retailerRepository;
        private IRepository<RetailShop> _retailerShopRepository;
        private IRepository<City> _cityRepository;
        private IRepository<State> _stateRepository;
        private IRepository<RetailShopSaleProductCategory> _retailShopSaleProductCategoryRepository;
        private IRepository<SaleProductCategory> _saleProductCategoryRepository;




        // public accessors
        #region category & product
        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new Repository<Category>(Context)); }
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

        public IRepository<ProductEvent> ProductEventRepository
        {
            get { return _productEventRepository ?? (_productEventRepository = new Repository<ProductEvent>(Context)); }
        }

        public IRepository<ProductTag> ProductTagRepository
        {
            get { return _productTagRepository ?? (_productTagRepository = new Repository<ProductTag>(Context)); }
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

        public IRepository<Retailer> RetailerRepository
        {
            get { return _retailerRepository ?? (_retailerRepository = new Repository<Retailer>(Context)); }
        }

        public IRepository<RetailShop> RetailerShopRepository
        {
            get { return _retailerShopRepository ?? (_retailerShopRepository = new Repository<RetailShop>(Context)); }
        }
        public IRepository<City> CityRepository
        {
            get { return _cityRepository ?? (_cityRepository = new Repository<City>(Context)); }
        }
        public IRepository<State> StateRepository
        {
            get { return _stateRepository ?? (_stateRepository = new Repository<State>(Context)); }
        }
        public IRepository<RetailShopSaleProductCategory> RetailShopSaleProductCategoryRepository
        {
            get { return _retailShopSaleProductCategoryRepository ?? (_retailShopSaleProductCategoryRepository = new Repository<RetailShopSaleProductCategory>(Context)); }
        }
        public IRepository<SaleProductCategory> SaleProductCategoryRepository
        {
            get { return _saleProductCategoryRepository ?? (_saleProductCategoryRepository = new Repository<SaleProductCategory>(Context)); }
        }




    }
}
