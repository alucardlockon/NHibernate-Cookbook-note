using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryRecipes.MultiQueries
{
    public struct PageOf<T>
    {
        public int PageCount;
        public int PageNumber;
        public IEnumerable<T> PageOfResults;
    }
}
