using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate;
using NHibernate.Cfg;
using NH4CookbookHelpers;

namespace MappingRecipes.MappingByConvention
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            var mapper = new MyModelMapper();
            var mapping = mapper
                .CompileMappingFor(typeof(Product).Assembly
                    .GetExportedTypes()
                    .Where(x => x.Namespace == typeof(Product).Namespace));

            cfg.AddMapping(mapping);
        }

        protected override void AddInitialData(ISession session)
        {
            session.Save(new Movie
            {
                Name = "Mapping by convention - the movie",
                Description = "An interesting documentary",
                UnitPrice = 300,
                Actors = new List<ActorRole>
                {
                    new ActorRole
                    {
                        Actor = "NHibernate",
                        Role = "The mapper"
                    }
                }
            });
        }
    }
}