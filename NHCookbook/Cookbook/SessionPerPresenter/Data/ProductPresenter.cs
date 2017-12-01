using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH4CookbookHelpers.Queries.Model;
using SessionPerPresenter.Data;

namespace SessionPerPresenter.Data
{
    public class ProductPresenter : IPresenter
    {
        private readonly IDao<Product> _productDao;
        public ProductPresenter(IDao<Product> productDao)
        {
            _productDao = productDao;
        }
        public ProductListView ShowAllProducts()
        {
            return new ProductListView("All Products",
            _productDao.GetAll());
        }
        public virtual void Dispose()
        {
            _productDao.Dispose();
        }
    }
}
