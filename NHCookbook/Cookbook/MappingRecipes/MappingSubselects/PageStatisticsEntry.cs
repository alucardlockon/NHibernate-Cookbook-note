using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.MappingSubselects
{
    public class PageStatisticsEntry
    {
        public virtual string Url { get; protected set; }
        public virtual int ViewCount { get; protected set; }
    }
}
