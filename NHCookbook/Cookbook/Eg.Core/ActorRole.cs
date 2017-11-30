using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eg.Core
{
    public class ActorRole : Entity
    {
        public virtual string Actor { get; set; }
        public virtual string Role { get; set; }
    }
}