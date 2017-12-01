using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers;
using NHibernate;

namespace MappingRecipes.MappingSubselects
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(
       ISessionFactory sessionFactory)
        {
            var random = new Random();
            using (var session = sessionFactory
           .OpenStatelessSession())
            {
                for (var i = 0; i < 100; i++)
                {
                    session.Insert(new PageHit
                    {
                        Url = random.Next(10).ToString(),
                        PageViewDateTime = DateTime.Now
                    });
                }
            }
        }

        public override void RunQueries(ISession session)
        {
            var stats = session.QueryOver<PageStatisticsEntry>()
            .Where(x => x.ViewCount > 2)
            .OrderBy(x => x.ViewCount).Desc.List();

            foreach (var entry in stats)
            {
                Console.WriteLine("Url: {0}, View count: {1}",
                entry.Url, entry.ViewCount);
            }
        }
    }
}
