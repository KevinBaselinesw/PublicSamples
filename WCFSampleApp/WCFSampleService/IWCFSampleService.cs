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

    [DataContract]
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {
   
        }

        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string TitleOfCourtesy { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? HireDate { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string HomePhone { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public int? ReportsTo { get; set; }

        [DataMember]
        public string PhotoPath { get; set; }

        [DataMember]
        public List<EmployeeDTO> Employees1 { get; set; }

        [DataMember]
        public EmployeeDTO Employee1 { get; set; }

        [DataMember]
        public List<OrderDTO> Orders { get; set; }

        [DataMember]
        public List<TerritoryDTO> Territories { get; set; }

    }

    [DataContract]
    public class CategoryDTO
    {
        public CategoryDTO()
        {
        }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte[] Picture { get; set; }

        [DataMember]
        public List<ProductDTO> Products { get; set; }
    }

    [DataContract]
    public class ProductDTO
    {
        public ProductDTO()
        {
        }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int? SupplierID { get; set; }

        [DataMember]
        public int? CategoryID { get; set; }

        [DataMember]
        public string QuantityPerUnit { get; set; }

        [DataMember]
        public decimal? UnitPrice { get; set; }

        [DataMember]
        public short? UnitsInStock { get; set; }

        [DataMember]
        public short? UnitsOnOrder { get; set; }

        [DataMember]
        public short? ReorderLevel { get; set; }

        [DataMember]
        public bool Discontinued { get; set; }

        [DataMember]
        public CategoryDTO Category { get; set; }

        [DataMember]
        public List<Order_DetailDTO> Order_Details { get; set; }

        [DataMember]
        public SupplierDTO Supplier { get; set; }

    }

    [DataContract]
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactTitle { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public List<OrderDTO> Orders { get; set; }

        [DataMember]
        public List<CustomerDemographicDTO> CustomerDemographics { get; set; }

    }

    [DataContract]
    public class CustomerDemographicDTO
    {
        public CustomerDemographicDTO()
        {
        }

        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string CustomerDesc { get; set; }

        [DataMember]
        public List<CustomerDTO> Customers { get; set; }
    }


    [DataContract]
    public class OrderDTO
    {
        public OrderDTO()
        {
        }

        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? RequiredDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }

        [DataMember]
        public int? ShipVia { get; set; }

        [DataMember]
        public decimal? Freight { get; set; }

        [DataMember]
        public string ShipName { get; set; }

        [DataMember]
        public string ShipAddress { get; set; }

        [DataMember]
        public string ShipCity { get; set; }

        [DataMember]
        public string ShipRegion { get; set; }

        [DataMember]
        public string ShipPostalCode { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public CustomerDTO Customer { get; set; }

        [DataMember]
        public EmployeeDTO Employee { get; set; }

        [DataMember]
        public List<Order_DetailDTO> Order_Details { get; set; }
        
        [DataMember]
        public ShipperDTO Shipper { get; set; }
    }

    [DataContract]
    public class OrderWithSubtotalDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public CustomerDTO Customer { get; set; }
        [DataMember]
        public EmployeeDTO Employee { get; set; }
        [DataMember]
        public ShipperDTO Shipper { get; set; }
        [DataMember]
        public Order_SubtotalDTO Subtotal { get; set; }
        [DataMember]
        public DateTime? OrderDate { get; set; }
        [DataMember]
        public DateTime? RequiredDate { get; set; }
        [DataMember]
        public DateTime? ShippedDate { get; set; }
    }

    [DataContract]
    public class Order_SubtotalDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public decimal? Subtotal { get; set; }
    }

    [DataContract]
    public class Orders_QryDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? RequiredDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }

        [DataMember]
        public int? ShipVia { get; set; }

        [DataMember]
        public decimal? Freight { get; set; }

        [DataMember]
        public string ShipName { get; set; }

        [DataMember]
        public string ShipAddress { get; set; }

        [DataMember]
        public string ShipCity { get; set; }

        [DataMember]
        public string ShipRegion { get; set; }

        [DataMember]
        public string ShipPostalCode { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }
    }

    [DataContract]
    public class SupplierDTO
    {
        public SupplierDTO()
        {
        }

        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactTitle { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string HomePage { get; set; }

        [DataMember]
        public List<ProductDTO> Products { get; set; }
    }

    [DataContract]
    public class ShipperDTO
    {
        public ShipperDTO()
        {
        }

        [DataMember]
        public int ShipperID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public List<OrderDTO> Orders { get; set; }
    }

    [DataContract]
    public class Order_DetailDTO
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }
        [DataMember]
        public short Quantity { get; set; }
        [DataMember]
        public float Discount { get; set; }
        [DataMember]
        public OrderDTO Order { get; set; }
        [DataMember]
        public ProductDTO Product { get; set; }
    }

    [DataContract]
    public partial class Order_Details_ExtendedDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short Quantity { get; set; }

        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public decimal? ExtendedPrice { get; set; }
    }

    [DataContract]
    public class TerritoryDTO
    {
        public TerritoryDTO()
        {
        }

        [DataMember]
        public string TerritoryID { get; set; }
        [DataMember]
        public string TerritoryDescription { get; set; }

        [DataMember]
        public int RegionID { get; set; }

        [DataMember]
        public RegionDTO Region { get; set; }

        [DataMember]
        public List<EmployeeDTO> Employees { get; set; }
    }

    [DataContract]
    public class RegionDTO
    {
        public RegionDTO()
        {
        }

        [DataMember]
        public int RegionID { get; set; }


        [DataMember]
        public string RegionDescription { get; set; }

        [DataMember]
        public List<TerritoryDTO> Territories { get; set; }
    }
}
