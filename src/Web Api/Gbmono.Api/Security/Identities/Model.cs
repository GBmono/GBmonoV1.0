using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gbmono.Api.Security.Identities
{
    // You can add profile data for the user by adding more properties to your GbmonoUser class
    public class GbmonoUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public int? EnableSMS { get; set; }
        public DateTime CreateTime { get; set; }

        // create user instance
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<GbmonoUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

   
    // DbContext which uses a custom user entity with a string primary key
    public class GBmonoUserDbContext : IdentityDbContext<GbmonoUser>
    {
        // constructor with default sql connection string
        public GBmonoUserDbContext() : base("GbmonoUserSqlConnection", throwIfV1Schema: false)
        {
        }
    

        public static GBmonoUserDbContext Create()
        {
            return new GBmonoUserDbContext();
        }
    }
}