using System.Linq;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NHibernate.Linq;
using NH4CookbookHelpers;

namespace QueryRecipes.EagerLoadingWithLinq
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var book = session.Query<Book>()
            .Fetch(x => x.Publisher)
            .FirstOrDefault();

            Show("Book:", book);

            var movies = session.Query<Movie>()
            .FetchMany(x => x.Actors)
            .ToList();
            Show("Movies:", movies);
        }
    }
}
