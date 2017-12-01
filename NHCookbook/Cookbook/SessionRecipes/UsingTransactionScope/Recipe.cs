using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NH4CookbookHelpers;

namespace SessionRecipes.UsingTransactionScope
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISessionFactory sessionFactory)
        {
            var catalog = new ProductCatalog(sessionFactory);
            var warehouse = new WarehouseFacade();

            var p = new ProductApp(catalog, warehouse);

            var sprockets = new Product()
            {
                Name = "Sprockets",
                Description = "12 pack, metal",
                UnitPrice = 14.99M
            };

            p.AddProduct(sprockets);

            sprockets.UnitPrice = 9.99M;
            p.UpdateProduct(sprockets);

            p.RemoveProduct(sprockets);
        }
    }
}
