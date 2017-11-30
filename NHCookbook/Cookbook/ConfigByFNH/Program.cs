using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace ConfigByFNH
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var config = MsSqlConfiguration.MsSql2012
                .ConnectionString(connstr => connstr.FromConnectionStringWithKey("db"))
                .AdoNetBatchSize(100);
            var nhConfig = Fluently.Configure()
                .Database(config)
                .BuildConfiguration();
            var sessionFactory = nhConfig.BuildSessionFactory();
            Console.WriteLine("NHibernate configured fluently!");
            Console.ReadKey();
        }
    }
}