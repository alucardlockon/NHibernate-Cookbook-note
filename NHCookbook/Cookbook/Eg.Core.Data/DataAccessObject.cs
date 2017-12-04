using Eg.Core.BaseClass;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eg.Core.Data
{
    public class DataAccessObject<T, TId>
 where T : Entity<TId>
    {

        private readonly ISessionFactory _sessionFactory;

        private ISession session
        {
            get
            {
                return _sessionFactory.GetCurrentSession();
            }
        }

        public DataAccessObject(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public T Get(TId id)
        {
            return WithinTransaction(() => session.Get<T>(id));
        }

        public T Load(TId id)
        {
            return WithinTransaction(() => session.Load<T>(id));
        }

        public void Save(T entity)
        {
            WithinTransaction(() => session.SaveOrUpdate(entity));
        }

        public void Delete(T entity)
        {
            WithinTransaction(() => session.Delete(entity));
        }

        private TResult WithinTransaction<TResult>(Func<TResult> func)
        {
            if (!session.Transaction.IsActive)
            {
                // Wrap in transaction
                TResult result;
                using (var tx = session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }

            // Don't wrap;
            return func.Invoke();
        }

        private void WithinTransaction(Action action)
        {
            WithinTransaction<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }

    }

    /*
    public class DataAccessObject<T> : DataAccessObject<T, Guid>
     where T : Entity
    {
    }
    */
}
