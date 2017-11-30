using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.LazyProperties
{
    public class Article
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; set; }
        public virtual string Abstract { get; set; }
        public virtual string Author { get; set; }
        public virtual string FullText { get; set; }
    }
}
