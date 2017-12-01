using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using NH4CookbookHelpers;
using NHibernate;

namespace MappingRecipes.SerializableValues
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(
       ISessionFactory sessionFactory)
        {
            try
            {
                throw new ApplicationException(
               "Something happened",
                new NullReferenceException("Something was null")
                );
            }
            catch (Exception ex)
            {
                LogError(ex, sessionFactory);
            }
        }

        private void LogError(Exception exception,
       ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory
           .OpenStatelessSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Insert(new Error
                    {
                        ErrorDateTime = DateTime.Now,
                        Exception = exception
                    });
                    tx.Commit();
                }
            }
        }

        public override void RunQueries(ISession session)
        {
            var error = session.QueryOver<Error>()
           .SingleOrDefault();
            if (error.Exception != null)
            {
                ShowException(error.Exception);
                Console.WriteLine("Stack trace:" +
               error.Exception.StackTrace);
            }
        }

        private void ShowException(Exception exception)
        {
            Console.WriteLine("Type: {0}, Message: {1}",
                exception.Message, exception.GetType());
            if (exception.InnerException != null)
            {
                ShowException(exception.InnerException);
            }
        }
    }
}
