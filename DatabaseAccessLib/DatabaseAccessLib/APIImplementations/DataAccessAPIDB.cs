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

        public IEnumerable<Category> GetProductCategoriesByID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.Where(t=>t.CategoryID == CategoryID).ToArray();
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

        public IEnumerable<Product> GetProductsBySupplier(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t=>t.SupplierID == SupplierID).ToArray();
                return Products;
            }
        }

        public IEnumerable<Product> GetProductsByCategoryID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t => t.CategoryID == CategoryID).ToArray();
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

        public IEnumerable<Order> GetOrdersByShipVia(int ShipVia)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include("Employee").
                        Include("Customer").
                        Include("Order_Details").
                        Where(t=>t.ShipVia == ShipVia).
                        ToArray();

                return Orders;
            }
        }

        public IEnumerable<Order> GetOrdersByEmployeeID(int EmployeeID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include("Employee").
                        Include("Customer").
                        Include("Order_Details").
                        Include("Shipper").
                        Where(t => t.EmployeeID == EmployeeID).
                        ToArray();

                return Orders;
            }
        }
        public IEnumerable<Order> GetOrdersByCustomerID(string CustomerID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include("Employee").
                        Include("Customer").
                        Include("Order_Details").
                        Include("Shipper").
                        Where(t=>t.CustomerID == CustomerID).
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

        public IEnumerable<Supplier> GetSuppliersByID(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.Where(t=>t.SupplierID == SupplierID).ToArray();
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

        public IEnumerable<Order_Detail> GetOrderDetailsByProductID(int ProductID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var OrderDetails = dbContext.Order_Details.
                    Include("Order").
                    Include("Product").
                    Where(t=>t.ProductID == ProductID).ToArray();
                return OrderDetails;
            }
        }


    }
}
