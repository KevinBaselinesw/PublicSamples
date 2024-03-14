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

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var customers = DatabaseAPI.GetAllCustomers();

            return ConvertToDTO(customers);
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orders_QryDTO> GetAllOrdersQry()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID)
        {
            throw new NotImplementedException();
        }

        #region convert collections of data structures
        private List<CustomerDTO> ConvertToDTO(IEnumerable<Customer> customers)
        {
            if (customers == null)
                return null;

            List<CustomerDTO> customerDTOs = new List<CustomerDTO>();

            foreach (var customer in customers)
            {
                CustomerDTO convertedCustomer = ToCustomerDTO(customer);

                if (convertedCustomer != null)
                {
                    customerDTOs.Add(convertedCustomer);
                }
            }
            return customerDTOs;
        }

        private List<ProductDTO> ConvertToDTO(IEnumerable<Product> products)
        {
            if (products == null)
                return null;

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                ProductDTO convertedProduct = ToProductDTO(product);

                if (convertedProduct != null)
                {
                    productDTOs.Add(convertedProduct);
                }
            }
            return productDTOs;
        }


        private List<EmployeeDTO> ConvertToDTO(IEnumerable<Employee> employees)
        {
            if (employees == null)
                return null;

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                EmployeeDTO covertedEmployee = ToEmployeeDTO(employee);
                if (covertedEmployee != null)
                {
                    employeeDTOs.Add(covertedEmployee);
                }
            }

            return employeeDTOs;
        }

        private List<CategoryDTO> ConvertToDTO(IEnumerable<Category> categories)
        {
            if (categories == null)
                return null;

            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                CategoryDTO convertedCategory = ToCategoryDTO(category);

                if (convertedCategory != null)
                {
                    categoryDTOs.Add(convertedCategory);
                }
            }
            return categoryDTOs;
        }

        private List<CustomerDemographicDTO> ConvertToDTO(ICollection<CustomerDemographic> customerDemographics)
        {
            if (customerDemographics == null)
                return null;

            List<CustomerDemographicDTO> demographicDTOs = new List<CustomerDemographicDTO>();

            foreach (var demographic in customerDemographics)
            {
                CustomerDemographicDTO convertedDemograhic = ToCustomerDemographicDTO(demographic);

                if (convertedDemograhic != null)
                {
                    demographicDTOs.Add(convertedDemograhic);
                }
            }
            return demographicDTOs;
        }


        private List<OrderDTO> ConvertToDTO(ICollection<Order> orders)
        {
            if (orders == null)
                return null;

            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            foreach (var order in orders)
            {
                OrderDTO convertedOrder = ToOrderDTO(order);
                if (convertedOrder != null)
                {
                    orderDTOs.Add(convertedOrder);
                }
            }
            return orderDTOs;
        }

        private List<Order_DetailDTO> ConvertToDTO(ICollection<Order_Detail> order_details)
        {
            if (order_details == null)
                return null;

            List<Order_DetailDTO> orderDetailDTOs = new List<Order_DetailDTO>();

            foreach (var orderDetail in order_details)
            {
                Order_DetailDTO convertedOrderDetail = ToOrder_DetailDTO(orderDetail);

                if (convertedOrderDetail != null)
                {
                    orderDetailDTOs.Add(convertedOrderDetail);
                }
            }
            return orderDetailDTOs;
        }

        private List<TerritoryDTO> ConvertToDTO(ICollection<Territory> territories)
        {
            if (territories == null)
                return null;

            List<TerritoryDTO> territoryDTOs = new List<TerritoryDTO>();

            foreach (var territory in territories)
            {
                TerritoryDTO convertedTerritory = ToTerritoryDTO(territory);

                if (convertedTerritory != null)
                {
                    territoryDTOs.Add(convertedTerritory);
                }

            }
            return territoryDTOs;
        }

        #endregion

        #region convert individual data structures

        private CustomerDTO ToCustomerDTO(Customer customer)
        {
            if (customer == null)
                return null;

            var convertedCustomer = new CustomerDTO()
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                CustomerDemographics = ConvertToDTO(customer.CustomerDemographics),
                Orders = ConvertToDTO(customer.Orders)
            };

            return convertedCustomer;
        }

        private ProductDTO ToProductDTO(Product product)
        {
            if (product == null)
                return null;

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
                Category = ToCategoryDTO(product.Category),
                Order_Details = ConvertToDTO(product.Order_Details),
                Supplier = ToSupplierDTO(product.Supplier),
            };

            return convertedProduct;
        }

        private EmployeeDTO ToEmployeeDTO(Employee employee)
        {
            if (employee == null)
                return null;

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
                Employee1 = ToEmployeeDTO(employee.Employee1),
                Employees1 = ConvertToDTO(employee.Employees1),
                Orders = ConvertToDTO(employee.Orders),
                Territories = ConvertToDTO(employee.Territories),
            };

            return covertedEmployee;
        }

        private CategoryDTO ToCategoryDTO(Category category)
        {
            if (category == null)
                return null;

            var convertedCategory = new CategoryDTO()
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture,
                Products = ConvertToDTO(category.Products),
            };

            return convertedCategory;
        }
        

        private CustomerDemographicDTO ToCustomerDemographicDTO(CustomerDemographic demographic)
        {
            if (demographic == null)
                return null;

            var convertedDemograhic = new CustomerDemographicDTO()
            {
                CustomerTypeID = demographic.CustomerTypeID,
                CustomerDesc = demographic.CustomerDesc,
                Customers = ConvertToDTO(demographic.Customers),
            };
            return convertedDemograhic;
        }

        private OrderDTO ToOrderDTO(Order order)
        {
            if (order == null)
                return null;

            var convertedOrder = new OrderDTO()
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                EmployeeID = order.EmployeeID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
                Customer = ToCustomerDTO(order.Customer),
                Employee = ToEmployeeDTO(order.Employee),
                Order_Details = ConvertToDTO(order.Order_Details),
                Shipper = ToShipperDTO(order.Shipper),
            };

            return convertedOrder;

        }

        private ShipperDTO ToShipperDTO(Shipper shipper)
        {
            if (shipper == null)
                return null;

            var convertedShipper = new ShipperDTO()
            {
                ShipperID = shipper.ShipperID,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone,
                Orders = ConvertToDTO(shipper.Orders),
            };

            return convertedShipper;
        }

        private SupplierDTO ToSupplierDTO(Supplier supplier)
        {
            if (supplier == null)
                return null;

            var convertedSupplier = new SupplierDTO()
            {
                SupplierID = supplier.SupplierID,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
                HomePage = supplier.HomePage,
                Products = ConvertToDTO(supplier.Products),
            };

            return convertedSupplier;
        }

        private Order_DetailDTO ToOrder_DetailDTO(Order_Detail orderDetail)
        {
            if (orderDetail == null)
                return null;

            var convertedOrder = new Order_DetailDTO()
            {
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity,
                Discount = orderDetail.Discount,
                Order = ToOrderDTO(orderDetail.Order),
                Product = ToProductDTO(orderDetail.Product),
            };

            return convertedOrder;
        }

        private TerritoryDTO ToTerritoryDTO(Territory territory)
        {
            if (territory == null)
                return null;

            var convertedTerritory = new TerritoryDTO()
            {
                TerritoryID = territory.TerritoryID,
                TerritoryDescription = territory.TerritoryDescription,
                RegionID = territory.RegionID,
                Region = ConvertToDTO(territory.Region),
                Employees = ConvertToDTO(territory.Employees),
            };

            return convertedTerritory;
        }

        private RegionDTO ConvertToDTO(Region region)
        {
            if (region == null)
                return null;

            RegionDTO convertedRegion = new RegionDTO()
            {
                RegionID = region.RegionID,
                RegionDescription = region.RegionDescription,
                Territories = ConvertToDTO(region.Territories),
            };

            return convertedRegion;
        }

        #endregion


   
    }
}
