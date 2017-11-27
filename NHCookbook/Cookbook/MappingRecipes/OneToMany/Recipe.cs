using Eg.Core;
using NH4CookbookHelpers;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.OneToMany
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            cfg.AddAssembly(typeof(Product).Assembly);
        }
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Movie
            {
                Name = "Hibernation",
                Description =
            "The countdown for the lift-off has begun",
                UnitPrice = 300,
                Actors = new List<ActorRole>
                 {
                 new ActorRole
                 {
                 Actor = "Adam Quintero",
                 Role = "Joseph Wood"
                 }
                }
            });
        }
    }
}
