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
        [WebInvoke(Method = "GET", UriTemplate = "GetProductCategoriesByID/?id={CategoryID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllProducts", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<ProductDTO> GetAllProducts();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetProductsBySupplier/?id={SupplierID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetProductsByCategoryID/?id={CategoryID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllCustomers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<CustomerDTO> GetAllCustomers();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllOrders", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<OrderDTO> GetAllOrders();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllOrdersWithSubtotals", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetAllOrdersWithSubtotalsByCustomerID/?id={CustomerID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
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
        [WebInvoke(Method = "GET", UriTemplate = "GetAllSuppliers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<SupplierDTO> GetAllSuppliers();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetSuppliersByID/?id={SupplierID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID);

        [OperationContract]
        IEnumerable<ShipperDTO> GetAllShippers();

        [OperationContract]
        IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetOrderDetailsByOrderID/?id={OrderID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID);

    }

 
}
