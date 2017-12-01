using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NH4CookbookHelpers;

namespace SessionRecipes.SavingEntities
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISessionFactory
        sessionFactory)
        {
            PerformSave(sessionFactory);
            PerformUpdate(sessionFactory);

            TestFlushMode(sessionFactory, FlushMode.Auto);
            TestFlushMode(sessionFactory, FlushMode.Commit);
            TestFlushMode(sessionFactory, FlushMode.Never);
        }

        private void PerformSave(ISessionFactory sessionFactory)
        {
            Console.WriteLine("PerformSave");

            var product = new Product { Name = "PerformSave" };

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {

                    session.Save(product);
                    Console.WriteLine("Id:{0}", product.Id);
                    tx.Commit();
                }
            }
        }

        private void PerformUpdate(ISessionFactory
        sessionFactory)
        {
            Console.WriteLine("PerformUpdate");
            Product product;

            using (var firstSession = sessionFactory
            .OpenSession())
            {
                using (var tx = firstSession.BeginTransaction())
                {
                    product = firstSession.Get<Product>(1);
                    tx.Commit();
                }
            }

            product.Description += "-Updated by PerformUpdate";

            using (var secondSession = sessionFactory
            .OpenSession())
            {
                using (var tx = secondSession.BeginTransaction())
                {
                    secondSession.Update(product);
                    tx.Commit();
                }
            }
        }

        private void TestFlushMode(ISessionFactory
        sessionFactory, FlushMode flushMode)
        {
            var name = "TestPublisher" + flushMode;

            using (var session = sessionFactory.OpenSession())
            {
                session.FlushMode = flushMode;

                using (var tx = session.BeginTransaction())
                {
                    var publisher = new Publisher { Name = name };
                    Console.WriteLine("Saving {0}", name);
                    session.Save(publisher);

                    Console.WriteLine("Searching for {0}", name);

                    var searchPublisher = session
                    .QueryOver<Publisher>()
                    .Where(x => x.Name == name)
                    .SingleOrDefault();

                    Console.WriteLine(searchPublisher != null ?
                   "Found it!" : "Didn't find it!");

                    tx.Commit();
                }

                using (var tx = session.BeginTransaction())
                {
                    Console.WriteLine(
                   "Searching for {0} again", name);

                    var searchPublisher = session
                    .QueryOver<Publisher>()
                    .Where(x => x.Name == name)
                    .SingleOrDefault();

                    Console.WriteLine(searchPublisher != null ?
                   "Found it!" : "Didn't find it!");
                }
            }
        }
    }
}
