using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class UserVisitMap: EntityTypeConfiguration<UserVisit>
    {
        public UserVisitMap()
        {
            ToTable("UserVisit");

            HasKey(m => m.UserVisitId);
        }
    }
}
