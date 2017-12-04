using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eg.Core.Data.Resp.Impl
{
    class NHibernateBase
    {
        protected readonly ISessionFactory _sessionFactory;

        protected virtual ISession session
        {
            get
            {
                return _sessionFactory.GetCurrentSession();
            }
        }

        public NHibernateBase(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected virtual TResult WithinTransaction<TResult>(
        Func<TResult> func)
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

        protected virtual void WithinTransaction(Action action)
        {
            WithinTransaction<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }
    }
}
