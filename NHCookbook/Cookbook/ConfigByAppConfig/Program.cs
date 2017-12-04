using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eg.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace ConfigByAppConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create DB
            /*
            var nhConfig = new Configuration().Configure();
            var mapper = new ConventionModelMapper();
            nhConfig.AddMapping(mapper.CompileMappingFor(new[] { typeof(TestClass) }));
            var schemaExport = new SchemaExport(nhConfig);
            schemaExport.Create(false, true);
            Console.WriteLine("The tables have been created");
            //var sessionFactory = nhConfig.BuildSessionFactory();
            //Console.WriteLine("NHibernate Configured!");
            Console.ReadKey();
            */

            // Create Sql
            /*
            var nhConfig = new Configuration().Configure();
            var mapper = new ConventionModelMapper();
            nhConfig.AddMapping(mapper.CompileMappingFor(new[] {typeof(TestClass)}));

            var schemaExport = new SchemaExport(nhConfig);

            schemaExport
                .SetOutputFile(@"db.sql")
                .Execute(false, false, false);

            Console.WriteLine("An sql file has been generated at {0}",
                Path.GetFullPath("db.sql"));
            Console.ReadKey();
            */

            var nhConfig = new ConfigurationBuilder().Build();


        }
    }
}