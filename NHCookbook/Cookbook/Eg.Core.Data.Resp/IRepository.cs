using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eg.Core.Data.Resp
{
    public interface IRepository<T> : IEnumerable<T>
 where T : Entity
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool Remove(T item);
    }
}
