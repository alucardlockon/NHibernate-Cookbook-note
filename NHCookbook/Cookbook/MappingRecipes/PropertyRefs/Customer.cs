using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.PropertyRefs
{
    public class Customer
    {
        public Customer()
        {
            ContactPersons = new HashSet<ContactPerson>();
        }
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ISet<ContactPerson> ContactPersons
        {
            get;
            set;
        }
        public virtual int CompanyId { get; set; }

    }
}
