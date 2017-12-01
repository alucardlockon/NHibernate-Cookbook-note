using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.SerializableValues
{
    public class Error
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime ErrorDateTime { get; set; }
        public virtual Exception Exception { get; set; }
    }
}
