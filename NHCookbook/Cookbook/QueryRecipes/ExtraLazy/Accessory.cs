using NH4CookbookHelpers.Queries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryRecipes.ExtraLazy
{
    public class Accessory : Entity
    {
        public virtual string Name { get; set; }
    }
}
