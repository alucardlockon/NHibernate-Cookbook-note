using System;
using System.Collections.Generic;

namespace QueryRecipes.ExtraLazy
{
    public class Car
    {
        public Car()
        {
            Accessories = new HashSet<Accessory>();
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual ISet<Accessory> Accessories { get; set; }
    }
}
