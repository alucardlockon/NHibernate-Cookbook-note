using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingRecipes.Bidirectional
{
    public class Order
    {
        private ISet<OrderItem> _items;
        private ISet<Project> _projects;

        public virtual Guid Id { get; protected set; }

        public Order()
        {
            _items = new HashSet<OrderItem>();
            _projects = new HashSet<Project>();
        }


        public virtual IEnumerable<OrderItem> Items
        {
            get
            {
                return _items;
            }
        }
        public virtual IEnumerable<Project> Projects
        {
            get
            {
                return _projects;
            }
        }

        public virtual bool AddItem(OrderItem newItem)
        {
            if (newItem != null && _items.Add(newItem))
            {
                newItem.SetOrder(this);
                return true;
            }
            return false;
        }

        public virtual bool RemoveItem(
        OrderItem itemToRemove)
        {
            if (itemToRemove != null &&
            _items.Remove(itemToRemove))
            {
                itemToRemove.SetOrder(null);
                return true;
            }
            return false;
        }
        public virtual bool ConnectProject(Project project)
        {
            if (project != null && _projects.Add(project))
            {
                project.ConnectOrder(this);
                return true;
            }
            return false;
        }

        public virtual bool DisconnectProject(Project project)
        {
            if (project != null && _projects.Contains(project))
            {
                _projects.Remove(project);
                project.DisconnectOrder(this);
                return true;
            }
            return false;
        }
    }
}
