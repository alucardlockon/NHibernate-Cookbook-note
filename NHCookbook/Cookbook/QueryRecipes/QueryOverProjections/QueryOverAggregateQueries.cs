using System;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace QueryRecipes.QueryOverProjections
{
    public class QueryOverAggregateQueries : IAggregateQueries
    {
        private readonly ISession _session;

        public QueryOverAggregateQueries(ISession session)
        {
            _session = session;
        }

    public IEnumerable<NameAndPrice> GetMoviePriceList()
    {
        return _session.QueryOver<Movie>()
        .Select(m => m.Name, m => m.UnitPrice)
        .List<object[]>()
        .Select(props =>
        new NameAndPrice()
        {
            Name = (string)props[0],
            Price = (decimal)props[1]
        });
    }

    public decimal GetAverageMoviePrice()
    {
        var result = _session.QueryOver<Movie>()
        .Select(Projections.Avg<Movie>(m => m.UnitPrice))
        .SingleOrDefault<double>();
        return Convert.ToDecimal(result);
    }

    public IEnumerable<NameAndPrice> GetAvgDirectorPrice()
    {
        return _session.QueryOver<Movie>()
        .Select(list => list
        //.SelectGroup(m => m.Director)
        //.SelectAvg(m => m.UnitPrice)
        )
        .List<object[]>()
        .Select(props =>
        new NameAndPrice()
        {
            Name = (string)props[0],
            Price = Convert.ToDecimal(props[1])
        });
    }

    }


}
