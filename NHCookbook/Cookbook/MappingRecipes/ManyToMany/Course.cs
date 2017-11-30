using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.ManyToMany
{
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ISet<Student> Students { get; set; }
    }
}