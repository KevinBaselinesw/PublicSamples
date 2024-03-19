using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DatabaseAccessLib;

namespace WCFSampleApp
{
    /// <summary>
    /// This is the implementation of the IWCFSampleService
    /// </summary>
    public class WCFSampleService : IWCFSampleService
    {
        static IDataAccessAPI DatabaseAPI = null;

        private IDataAccessAPI GetDatabaseAPI()
        {
            if (DatabaseAPI == null)
            {
                DatabaseAPI = new DataAccessAPIDB();
            }
            return DatabaseAPI;
        }
  
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var employees = DatabaseAPI.GetAllEmployees();

            return employees;
        }

        public IEnumerable<CategoryDTO> GetAllProductCategories()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetAllProductCategories();

            return categories;
        }

        public IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetProductCategoriesByID(CategoryID);

            return categories;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetAllProducts();

            return products;
        }

        public IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsBySupplier(SupplierID);

            return products;
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsByCategoryID(CategoryID);

            return products;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var customers = DatabaseAPI.GetAllCustomers();

            return customers;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrders();

            return orders;
        }

 
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersWithSubtotals();

            return orders;
        }

  

        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersWithSubtotalsByCustomerID(CustomerID);

            return orders;
        }

        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByShipVia(ShipVia);

            return orders;
        }

        public IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByEmployeeID(EmployeeID);

            return orders;
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByCustomerID(CustomerID);

            return orders;
        }

        public IEnumerable<Orders_QryDTO> GetAllOrdersQry()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersQry();

            return orders;
        }

        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var suppliers = DatabaseAPI.GetAllSuppliers();

            return suppliers;
        }

        public IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var suppliers = DatabaseAPI.GetSuppliersByID(SupplierID);

            return suppliers;
        }

        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var shippers = DatabaseAPI.GetAllShippers();

            return shippers;
        }

 
        public IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orderDetails = DatabaseAPI.GetOrderDetailsByProductID(ProductID);

            return orderDetails;
        }

        public IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID)
        {
            IDataAccessAPI DatabaseAPI = GetDatabaseAPI();

            var orderDetails = DatabaseAPI.GetOrderDetailsByOrderID(OrderID);

            return orderDetails;
        }

 
  



    }
}
