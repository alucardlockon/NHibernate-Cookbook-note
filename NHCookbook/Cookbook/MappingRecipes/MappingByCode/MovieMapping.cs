using Eg.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.MappingByCode
{
    public class MovieMapping : SubclassMapping<Movie>
    {
        public MovieMapping() 
        {
            DiscriminatorValue("Movie");
            Property(x => x.Director);
            List(x => x.Actors, x =>
            {
                x.Key(k => k.Column("MovieId"));
                x.Index(i => i.Column("ActorIndex"));
                x.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }
            , x => x.OneToMany()
            );
        }
    }
}
