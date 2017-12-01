using NH4CookbookHelpers;
using NH4CookbookHelpers.Queries;
using NHibernate;

namespace QueryRecipes.QueryByQueryOver
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new QueryOverQueries(session);
            ShowQueryResults(queries);
        }
    }
}
