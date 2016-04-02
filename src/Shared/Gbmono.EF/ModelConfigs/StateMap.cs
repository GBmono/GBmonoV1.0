using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            ToTable("State");

            HasKey(m => m.StateId);
        }


    }
}
