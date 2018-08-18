using DeliveryService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.EntityConfigurations
{
    public class RoutesEntityConfig : EntityTypeConfiguration<Route>
    {
        public RoutesEntityConfig()
        {
            this.HasRequired(r => r.Origin)
                .WithMany(p => p.RoutesOrigin)
                .WillCascadeOnDelete(false);

            this.HasRequired(r => r.Destination)
                .WithMany(p => p.RoutesDestination)
                .WillCascadeOnDelete(false);
        }
    }
}
