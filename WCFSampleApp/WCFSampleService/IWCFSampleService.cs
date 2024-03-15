using DatabaseAccessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFSampleApp
{
    /// <summary>
    /// This is the definition of the WCF Sample application
    /// </summary>
    [ServiceContract]
    public interface IWCFSampleService
    {
        [OperationContract]
        IEnumerable<EmployeeDTO> GetAllEmployees();

        [OperationContract]
        IEnumerable<CategoryDTO> GetAllProductCategories();

        [OperationContract]
        IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID);

        [OperationContract]
        IEnumerable<ProductDTO> GetAllProducts();

        [OperationContract]
        IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID);

        [OperationContract]
        IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID);

        [OperationContract]
        IEnumerable<CustomerDTO> GetAllCustomers();

        [OperationContract]
        IEnumerable<OrderDTO> GetAllOrders();

        [OperationContract]
        IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals();

        [OperationContract]
        IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID);

        [OperationContract]
        IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia);

        [OperationContract]
        IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID);

        [OperationContract]
        IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID);

        [OperationContract]
        IEnumerable<Orders_QryDTO> GetAllOrdersQry();

        [OperationContract]
        IEnumerable<SupplierDTO> GetAllSuppliers();

        [OperationContract]
        IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID);

        IEnumerable<ShipperDTO> GetAllShippers();

        IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID);

        IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID);
    }

 
}
