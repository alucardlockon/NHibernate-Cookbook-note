using NH4CookbookHelpers;
using NH4CookbookHelpers.Queries;
using NHibernate;

namespace QueryRecipes.QueryByCriteria
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new CriteriaQueries(session);
            ShowQueryResults(queries);
        }
    }
}
