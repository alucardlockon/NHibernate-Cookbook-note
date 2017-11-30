using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.ImmutableEntities
{
    public class LogEntry
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Message { get; set; }
    }
}
