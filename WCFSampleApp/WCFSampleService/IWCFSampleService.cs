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
        [WebInvoke(Method = "GET", UriTemplate = "GetAllEmployees", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<EmployeeDTO> GetAllEmployees();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllProductCategories", ResponseFormat = WebMessageFormat.Json, RequestFormat =WebMessageFormat.Json, BodyStyle =WebMessageBodyStyle.Bare )]
        IEnumerable<CategoryDTO> GetAllProductCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllProductCategories/?id={CategoryID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
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
        
        [WebInvoke(Method = "GET", UriTemplate = "GetOrdersByEmployeeID/?id={EmployeeID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID);

        [OperationContract]
        IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID);

        [OperationContract]
        IEnumerable<Orders_QryDTO> GetAllOrdersQry();

        [OperationContract]
        IEnumerable<SupplierDTO> GetAllSuppliers();

        [OperationContract]
        IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID);

        [OperationContract]
        IEnumerable<ShipperDTO> GetAllShippers();

        [OperationContract]
        IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID);

        [OperationContract]
        IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID);

    }

 
}
