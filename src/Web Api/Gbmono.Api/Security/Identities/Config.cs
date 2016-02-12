using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Gbmono.Api.Security.Identities
{
    // Configure the application user manager used in this application. 
    // UserManager is defined in ASP.NET Identity and is used by the application.
    public class GbmonoUserManager : UserManager<GbmonoUser>
    {
        // ctor
        public GbmonoUserManager(IUserStore<GbmonoUser> store) : base(store)
        {

        }

        // create GbmonoUserManager instance
        // single instance for each request
        public static GbmonoUserManager Create(IdentityFactoryOptions<GbmonoUserManager> options, IOwinContext context)
        {
            var manager = new GbmonoUserManager(new UserStore<GbmonoUser>(context.Get<GBmonoUserDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<GbmonoUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<GbmonoUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            // return UserManager instance
            return manager;
        }
    }

    // Role Manager with default role model
    // this role manager may not be used in this project
    // all Gbmonu users don't have role feature
    public class GbmonoRoleManager : RoleManager<IdentityRole>
    {
        public GbmonoRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public static GbmonoRoleManager Create(IdentityFactoryOptions<GbmonoRoleManager> options, IOwinContext context)
        {
            return new GbmonoRoleManager(new RoleStore<IdentityRole>(context.Get<GBmonoUserDbContext>()));
        }
    }
}