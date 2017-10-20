using System;

namespace Cookbook
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
    }
}