using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingRecipes.MappingByCode
{
    public class ProductMapping : ClassMapping<Product>
    {
        public ProductMapping()
        {
            Table("Product");
            Id(x => x.Id, x => x.Generator(Generators.GuidComb));
            Version(p => p.Version, v => v.UnsavedValue(0));
            Discriminator(p => p.Column("ProductType"));
            Property(p => p.Name);
            Property(p => p.Description);
            Property(p => p.UnitPrice);
        }
    }
}
