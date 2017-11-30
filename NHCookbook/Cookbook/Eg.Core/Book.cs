using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eg.Core
{
    public class Book : Product
    {
        public virtual string ISBN { get; set; }
        public virtual string Author { get; set; }
    }
}