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
    public class ActorRoleMapping : ClassMapping<ActorRole>
    {
        public ActorRoleMapping()
        {
            Id(x => x.Id, x =>
                x.Generator(Generators.GuidComb));
            Property(x => x.Actor);
            Property(x => x.Role);
        }
    }
}