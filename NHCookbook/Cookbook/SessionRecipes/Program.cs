using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NH4CookbookHelpers;
using NHibernate.Cfg;

namespace SessionRecipes
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            RecipeLoader.DefaultConfiguration = new Configuration()
            .DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<Sql2008ClientDriver>();
                db.ConnectionString =
                @"Server=.\SQLEXPRESS;Database=NHCookbook;
            Trusted_Connection=True;";
            });
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WindowsFormsRunner());
            
        }
    }
}
