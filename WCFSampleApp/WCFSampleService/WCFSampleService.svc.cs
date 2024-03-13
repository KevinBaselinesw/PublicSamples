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

            return ConvertToDTO(employees);
        }

        public IEnumerable<CategoryDTO> GetAllProductCategories()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetAllProductCategories();

            return ConvertToDTO(categories);
        }

        public IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var categories = DatabaseAPI.GetProductCategoriesByID(CategoryID);

            return ConvertToDTO(categories);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetAllProducts();

            return ConvertToDTO(products);
        }

        public IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsBySupplier(SupplierID);

            return ConvertToDTO(products);
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID)
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var products = DatabaseAPI.GetProductsByCategoryID(CategoryID);

            return ConvertToDTO(products);
        }

        private IEnumerable<ProductDTO> ConvertToDTO(IEnumerable<Product> products)
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var convertedProduct = new ProductDTO()
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                };
                productDTOs.Add(convertedProduct);
            }
            return productDTOs;
        }

        private IEnumerable<EmployeeDTO> ConvertToDTO(IEnumerable<Employee> employees)
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                var covertedEmployee = new EmployeeDTO()
                {
                    EmployeeID = employee.EmployeeID,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Title = employee.Title,
                    TitleOfCourtesy = employee.TitleOfCourtesy,
                    BirthDate = employee.BirthDate,
                    HireDate = employee.HireDate,
                    Address = employee.Address,
                    City = employee.City,
                    Region = employee.Region,
                    PostalCode = employee.PostalCode,
                    Country = employee.Country,
                    HomePhone = employee.HomePhone,
                    Extension = employee.Extension,
                    Photo = employee.Photo,
                    Notes = employee.Notes,
                    ReportsTo = employee.ReportsTo,
                    PhotoPath = employee.PhotoPath,
                };

                employeeDTOs.Add(covertedEmployee);
            }

            return employeeDTOs;
        }

        private IEnumerable<CategoryDTO> ConvertToDTO(IEnumerable<Category> categories)
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                var convertedCategory = new CategoryDTO()
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture,
                };
                categoryDTOs.Add(convertedCategory);
            }
            return categoryDTOs;
        }


    }
}
