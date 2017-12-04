using System.Linq;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NHibernate.Linq;
using NH4CookbookHelpers;

namespace QueryRecipes.AdvancedEagerLoading
{
    public class Recipe : QueryRecipe
    {

        protected override void AddData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    for (var i = 1; i <= 20; i++)
                    {
                        var movie = new Movie
                        {
                            Name = "Movie" + i,
                            UnitPrice = i
                        };
                        movie.AddActor("Actor" + i, "Role" + i);
                        movie.AddActor("Second Actor" + 1, "Second Role" + i);
                        session.Save(movie);
                    }
                    tx.Commit();
                }
            }
        }

        protected override void Run(ISession session)
        {
            var baseQuery = session.Query<Movie>()
            .Where(x => x.Name.StartsWith("Movie"))
            .OrderBy(x => x.Name)
            .Skip(5)
            .Take(5);

            var movies = session.Query<Movie>()
                .Where(x => baseQuery.Contains(x))
 .OrderBy(x => x.Name)
 .FetchMany(x => x.Actors)
 .ToList();

            Show("A page of movies", movies);

            var allProducts = session.Query<Product>()
            .OrderBy(x => x.UnitPrice)
            .ToFuture();

            session.Query<Movie>()
            .FetchMany(x => x.Actors)
            .ToFuture();

            session.Query<Book>()
            .Fetch(x => x.Publisher)
            .ToFuture();

            Show("All products", allProducts);
        }

    }
}
