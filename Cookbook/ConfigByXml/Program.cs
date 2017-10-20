using System;
using ConfigByXml.MappingWithXml;
using NHibernate;
using NHibernate.Cfg;

namespace ConfigByXml
{
    internal static class Program
    {
        public static void Main()
        {
            Configuration nhConfig = new Configuration().Configure();
            ISessionFactory sessionFactory = nhConfig.BuildSessionFactory();
            Console.WriteLine("NHibernate Configured!");
            
            Console.ReadKey();
        }
    }
}