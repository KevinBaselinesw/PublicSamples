using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib
{
    public class DataAccessAPIDB : IDataAccessAPI
    {
        public IEnumerable<Employee> GetAllEmployees()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Employees = dbContext.Employees.ToArray();
                return Employees;
            }
        }

        public IEnumerable<Category> GetAllProductCategories()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.ToArray();
                return Categories;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.ToArray();
                return Products;
            }
        }
    }
}
