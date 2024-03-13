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

    }

    [DataContract]
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public string CustomerID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

    }

}
