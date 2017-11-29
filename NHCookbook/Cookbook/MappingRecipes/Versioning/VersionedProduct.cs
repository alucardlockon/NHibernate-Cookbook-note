using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.Versioning
{
    public class VersionedProduct
    {
        public virtual int Id { get; protected set; }
        public virtual int Version { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
