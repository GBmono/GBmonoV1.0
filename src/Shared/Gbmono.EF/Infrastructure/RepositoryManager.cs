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
        private IRepository<Product> _productRepository;
        private IRepository<Manufacturer> _manufacturerRepository;
        private IRepository<Brand> _brandRepository;
        private IRepository<Country> _countryRepository;
        private IRepository<Retailer> _retailerRepository;
        private IRepository<FollowOption> _followOptionRepository;
        private IRepository<Banner> _bannerRepository;

        // public accessors
        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new Repository<Category>(Context)); }
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new Repository<Product>(Context));  }
        }

        public IRepository<Manufacturer> ManufacturerRepository
        {
            get { return _manufacturerRepository ?? (_manufacturerRepository = new Repository<Manufacturer>(Context)); }
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

        public IRepository<FollowOption> FollowOptionRepository
        {
            get { return _followOptionRepository ?? (_followOptionRepository = new Repository<FollowOption>(Context)); }
        }

        public IRepository<Banner> BannerRepository
        {
            get { return _bannerRepository ?? (_bannerRepository = new Repository<Banner>(Context)); }
        }

    }
}
