using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.MappingSubselects
{
    public class PageHit
    {
        public virtual int Id { get; protected set; }
        public virtual string Url { get; set; }
        public virtual DateTime PageViewDateTime { get; set; }
    }
}
