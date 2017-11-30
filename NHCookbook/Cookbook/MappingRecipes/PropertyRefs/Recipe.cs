using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Mapping;
using NHibernate;
using NH4CookbookHelpers;

namespace MappingRecipes.PropertyRefs
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            var customer = new Customer
            {
                Name = "The customer",
                CompanyId = 345
            };

            customer.ContactPersons.Add(
            new ContactPerson
            {
                Customer = customer,
                Name = "Person1"
            }
            );

            session.Save(customer);
        }

        public override void RunQueries(ISession session)
        {
            var customer = session.Get<ContactPerson>(1);
            Console.WriteLine("Customer:" + customer.Customer.Name);
        }
    }
}

