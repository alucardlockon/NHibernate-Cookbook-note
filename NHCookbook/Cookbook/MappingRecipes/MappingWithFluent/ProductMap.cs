using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Mapping.Model;
using FluentNHibernate.Mapping;

namespace MappingRecipes.MappingWithFluent
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(p => p.Id).GeneratedBy.GuidComb();
            Version(x => x.Version);
            NaturalId().Property(p => p.Name).Not.ReadOnly();
            DiscriminateSubClassesOnColumn("ProductType");
            Map(p => p.Description);
            Map(p => p.UnitPrice);
        }
    }
}
