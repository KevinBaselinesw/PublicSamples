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

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Customers = dbContext.Customers.ToArray();
                return Customers;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include("Employee").
                        Include("Customer").
                        Include("Order_Details").
                        Include("Shipper").
                        ToArray();

                return Orders;
            }
        }


        public IEnumerable<Orders_Qry> GetAllOrdersQry()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders_Qries.ToArray();
                return Orders;
            }
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.ToArray();
                return Suppliers;
            }
        }

        public IEnumerable<Shipper> GetAllShippers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Shippers = dbContext.Shippers.ToArray();
                return Shippers;
            }
        }
    }
}
