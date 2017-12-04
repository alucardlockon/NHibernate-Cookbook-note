﻿using NH4CookbookHelpers;
using NH4CookbookHelpers.Queries;
using NHibernate;

namespace QueryRecipes.Futures
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new Queries(session);
            var result = queries.GetPageOfProducts(1, 2);
            var heading = string.Format("Page {0} of {1}",
                result.PageNumber,
 result.PageCount);
            Show(heading, result.PageOfResults);
        }
    }
}
