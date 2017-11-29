using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.ManyToMany
{
    public class Student
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
    }
}
