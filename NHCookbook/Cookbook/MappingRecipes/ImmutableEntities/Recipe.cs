using NH4CookbookHelpers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.ImmutableEntities
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(
        ISession session)
        {
            for (var i = 0; i < 10; i++)
            {
                session.Save(new LogEntry
                {
                    Message = "Message " + i
                });
            }
        }

        public override void RunQueries(
        ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var logEntries =
                    session.QueryOver<LogEntry>().List();
                    foreach (var logEntry in logEntries)
                    {
                        logEntry.Message = "Edited message";
                    }
                    tx.Commit();
                }
            }

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var logEntries = session.QueryOver<LogEntry>().List();
                    foreach (var logEntry in logEntries)
                    {
                        Console.WriteLine(logEntry.Message);
                    }
                }
            }
        }
    }
}
