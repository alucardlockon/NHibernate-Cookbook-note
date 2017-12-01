using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Queries.Model;
namespace SessionRecipes.UsingTransactionScope
{
    public interface IReceiveProductUpdates
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
