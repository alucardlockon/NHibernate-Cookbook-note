using System;

namespace Cookbook.Model
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
    }
}