using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MappingRecipes.DynamicComponents
{
    public class Contact
    {
        public Contact()
        {
            Attributes = new Hashtable();
        }
        public virtual Guid Id { get; protected set; }
        public virtual IDictionary Attributes { get; set; }
    }
}
