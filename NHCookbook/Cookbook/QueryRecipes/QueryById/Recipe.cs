using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;

namespace QueryRecipes.QueryById
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var product1 = session.Get<Product>(1);

            ShowNumberOfQueriesExecuted();
            ShowProduct(product1);
            ShowNumberOfQueriesExecuted();

            var product2 = session.Load<Product>(2);

            ShowProduct(product2);
            ShowNumberOfQueriesExecuted();
            var movie2 = session.Load<Movie>(2);

            ShowProduct(movie2);
            ShowNumberOfQueriesExecuted();
        }
    }
}
