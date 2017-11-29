using NH4CookbookHelpers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.ManyToMany
{
    public class Recipe : HbmMappingRecipe
    {
        private Guid _frenchId;

        protected override void AddInitialData(
        ISession session)
        {
            var anna = new Student { Name = "Anna" };
            var george = new Student { Name = "George" };

            var english = new Course { Name = "English" };
            var french = new Course { Name = "French" };

            english.Students.Add(anna);

            french.Students.Add(anna);
            french.Students.Add(george);

            session.Save(anna);
            session.Save(george);

            session.Save(english);
            session.Save(french);
            _frenchId = french.Id;
        }

        public override void RunQueries(ISession session)
        {
            var course2 = session.Get<Course>(_frenchId);
            Console.WriteLine("Course name: " + course2.Name);
            Console.WriteLine("Student count: " +
            course2.Students.Count());
        }
    }
}
