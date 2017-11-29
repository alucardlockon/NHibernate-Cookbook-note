using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode.Conformist;
namespace MappingRecipes.MappingByCode
{
    public class BookMapping : SubclassMapping<Book>
    {
        public BookMapping()
        {
            DiscriminatorValue("Book");
            Property(x => x.Author);
            Property(x => x.ISBN);
        }
    }
}
