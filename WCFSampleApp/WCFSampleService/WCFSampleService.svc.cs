using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DatabaseAccessLib;
using DatabaseAccessLib.APIImplementations;

namespace WCFSampleApp
{
    /// <summary>
    /// This is the implementation of the IWCFSampleService
    /// </summary>
    public class WCFSampleService : IWCFSampleService
    {
        static DataAccessAPIDB DatabaseAPI = null;

        private DataAccessAPIDB GetDatabaseAPI()
        {
            if (DatabaseAPI == null)
            {
                DatabaseAPI = new DataAccessAPIDB();
            }
            return DatabaseAPI;
        }



        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var employees = DatabaseAPI.GetAllEmployees();

            return DTOConversions.ConvertToEmployeesDTO(employees);
        }

        public IEnumerable<CategoryDTO> GetAllProductCategories()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetAllProductCategories();

            return DTOConversions.ConvertToCategoriesDTO(categories);
        }

        public IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetProductCategoriesByID(CategoryID);

            return DTOConversions.ConvertToCategoriesDTO(categories);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetAllProducts();

            return DTOConversions.ConvertToProductsDTO(products);
        }

        public IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsBySupplier(SupplierID);

            return DTOConversions.ConvertToProductsDTO(products);
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsByCategoryID(CategoryID);

            return DTOConversions.ConvertToProductsDTO(products);
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var customers = DatabaseAPI.GetAllCustomers();

            return DTOConversions.ConvertToCustomersDTO(customers);
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrders();

            return DTOConversions.ConvertToOrdersDTO(orders);
        }

 
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersWithSubtotals();

            return DTOConversions.ConvertToOrderWithSubtotalsDTO(orders);
        }

  

        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersWithSubtotalsByCustomerID(CustomerID);

            return DTOConversions.ConvertToOrderWithSubtotalsDTO(orders);
        }

        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByShipVia(ShipVia);

            return DTOConversions.ConvertToOrdersDTO(orders);
        }

        public IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByEmployeeID(EmployeeID);

            return DTOConversions.ConvertToOrdersDTO(orders);
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetOrdersByCustomerID(CustomerID);

            return DTOConversions.ConvertToOrdersDTO(orders);
        }

        public IEnumerable<Orders_QryDTO> GetAllOrdersQry()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orders = DatabaseAPI.GetAllOrdersQry();

            return DTOConversions.ConvertToOrdersQrysDTO(orders);
        }

        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var suppliers = DatabaseAPI.GetAllSuppliers();

            return DTOConversions.ConvertToSuppliersDTO(suppliers);
        }

        public IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var suppliers = DatabaseAPI.GetSuppliersByID(SupplierID);

            return DTOConversions.ConvertToSuppliersDTO(suppliers);
        }

        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var shippers = DatabaseAPI.GetAllShippers();

            return DTOConversions.ConvertToShippersDTO(shippers);
        }

 
        public IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orderDetails = DatabaseAPI.GetOrderDetailsByProductID(ProductID);

            return DTOConversions.ConvertToOrderDetailsDTO(orderDetails);
        }

        public IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var orderDetails = DatabaseAPI.GetOrderDetailsByOrderID(OrderID);

            return DTOConversions.ConvertToOrderDetailsExtendedsDTO(orderDetails);
        }

 
  



    }
}
