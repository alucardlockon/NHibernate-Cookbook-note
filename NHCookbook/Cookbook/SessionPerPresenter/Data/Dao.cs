using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionPerPresenter.Data
{
    public class Dao<TEntity> : IDao<TEntity>
 where TEntity : class
    {
        private readonly ISessionProvider _sessionProvider;
        public Dao(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }
        public void Dispose()
        {
            _sessionProvider.Dispose();
        }
        public IEnumerable<TEntity> GetAll()
        {
            var session = _sessionProvider.GetCurrentSession();
            IEnumerable<TEntity> results;
            using (var tx = session.BeginTransaction())
            {
                results = session.QueryOver<TEntity>()
                .List<TEntity>();
                tx.Commit();
            }
            return results;
        }
    }
}
