/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib
{
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
        public string PhotoBase64 { get; set; }  // reserved for web applications that require base64 encoding

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
        public string PictureBase64 { get; set; }

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

        public OrderDTO(OrderDTO orig)
        {
            OrderID = orig.OrderID;
            CustomerID = orig.CustomerID;
            EmployeeID = orig.EmployeeID;
            OrderDate = orig.OrderDate;
            RequiredDate = orig.RequiredDate;
            ShippedDate = orig.ShippedDate;
            ShipVia = orig.ShipVia;
            Freight = orig.Freight;
            ShipName = orig.ShipName;
            ShipAddress = orig.ShipAddress;
            ShipCity = orig.ShipCity;
            ShipRegion = orig.ShipRegion;
            ShipPostalCode = orig.ShipPostalCode;
            ShipCountry = orig.ShipCountry;

            Employee = orig.Employee;
            Customer = orig.Customer;
            Order_Details = orig.Order_Details;
            Shipper = orig.Shipper;
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
        public Order_DetailDTO()
        {

        }
        public Order_DetailDTO(Order_DetailDTO orig)
        {
            OrderID = orig.OrderID;
            ProductID = orig.ProductID;
            UnitPrice = orig.UnitPrice;
            Quantity = orig.Quantity;
            Discount = orig.Discount;

            Order = null;
            Product = null;
        }

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

    public class DTOConversions
    {
        #region convert collections of data structures
        public static List<CustomerDTO> ConvertToCustomersDTO(IEnumerable<Customer> customers)
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

        public static List<ProductDTO> ConvertToProductsDTO(IEnumerable<Product> products)
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


        public static List<EmployeeDTO> ConvertToEmployeesDTO(IEnumerable<Employee> employees)
        {
            if (employees == null)
                return null;

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees.ToArray())
            {
                EmployeeDTO covertedEmployee = ToEmployeeDTO(employee);
                if (covertedEmployee != null)
                {
                    employeeDTOs.Add(covertedEmployee);
                }
            }

            return employeeDTOs;
        }

        public static List<CategoryDTO> ConvertToCategoriesDTO(IEnumerable<Category> categories)
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

        public static List<CustomerDemographicDTO> ConvertToCustomerDemographicDTO(IEnumerable<CustomerDemographic> customerDemographics)
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


        public static List<OrderDTO> ConvertToOrdersDTO(IEnumerable<Order> orders)
        {
            if (orders == null)
                return null;

            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            foreach (var order in orders.ToArray())
            {
                OrderDTO convertedOrder = ToOrderDTO(order);
                if (convertedOrder != null)
                {
                    orderDTOs.Add(convertedOrder);
                }
            }
            return orderDTOs;
        }


        public static List<Order_DetailDTO> ConvertToOrderDetailsDTO(IEnumerable<Order_Detail> order_details)
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

        

        public static List<RegionDTO> ConvertToRegionsDTO(IEnumerable<Region> regions)
        {
            if (regions == null)
                return null;

            List<RegionDTO> regionDTOs = new List<RegionDTO>();

            foreach (var territory in regions.ToArray())
            {
                RegionDTO convertedRegion = ToRegionDTO(territory);

                if (convertedRegion != null)
                {
                    regionDTOs.Add(convertedRegion);
                }

            }
            return regionDTOs;
        }

        public static List<TerritoryDTO> ConvertToTerritoriesDTO(IEnumerable<Territory> territories)
        {
            if (territories == null)
                return null;

            List<TerritoryDTO> territoryDTOs = new List<TerritoryDTO>();

            foreach (var territory in territories.ToArray())
            {
                TerritoryDTO convertedTerritory = ToTerritoryDTO(territory);

                if (convertedTerritory != null)
                {
                    territoryDTOs.Add(convertedTerritory);
                }

            }
            return territoryDTOs;
        }

        public static List<Order_SubtotalDTO> ConvertToOrderSubTotalsDTO(IEnumerable<Order_Subtotal> SubTotals)
        {
            if (SubTotals == null)
                return null;

            List<Order_SubtotalDTO> SubTotalDTOs = new List<Order_SubtotalDTO>();

            foreach (var subTotal in SubTotals.ToArray())
            {
                Order_SubtotalDTO convertedSubTotal = ToOrder_SubTotalDTO(subTotal);

                if (convertedSubTotal != null)
                {
                    SubTotalDTOs.Add(convertedSubTotal);
                }

            }
            return SubTotalDTOs;
        }


        


        public static IEnumerable<OrderWithSubtotalDTO> ConvertToOrderWithSubtotalsDTO(IEnumerable<OrderWithSubtotal> orders)
        {
            if (orders == null)
                return null;

            List<OrderWithSubtotalDTO> orderDTOs = new List<OrderWithSubtotalDTO>();

            foreach (var order in orders)
            {
                OrderWithSubtotalDTO convertedOrder = ToOrderWithSubtotalDTO(order);

                if (convertedOrder != null)
                {
                    orderDTOs.Add(convertedOrder);
                }

            }
            return orderDTOs;
        }

        public static List<Orders_QryDTO> ConvertToOrdersQrysDTO(IEnumerable<Orders_Qry> orders)
        {
            if (orders == null)
                return null;

            List<Orders_QryDTO> orderDTOs = new List<Orders_QryDTO>();

            foreach (var order in orders)
            {
                Orders_QryDTO convertedOrder = ToOrders_QryDTO(order);

                if (convertedOrder != null)
                {
                    orderDTOs.Add(convertedOrder);
                }

            }
            return orderDTOs;
        }


        public static List<SupplierDTO> ConvertToSuppliersDTO(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                return null;

            List<SupplierDTO> supplierDTOs = new List<SupplierDTO>();

            foreach (var supplier in suppliers)
            {
                SupplierDTO convertedSupplier = ToSupplierDTO(supplier);

                if (convertedSupplier != null)
                {
                    supplierDTOs.Add(convertedSupplier);
                }

            }
            return supplierDTOs;
        }

        public static List<ShipperDTO> ConvertToShippersDTO(IEnumerable<Shipper> shippers)
        {
            if (shippers == null)
                return null;

            List<ShipperDTO> shipperDTOs = new List<ShipperDTO>();

            foreach (var shipper in shippers)
            {
                ShipperDTO convertedShipper = ToShipperDTO(shipper);

                if (convertedShipper != null)
                {
                    shipperDTOs.Add(convertedShipper);
                }

            }
            return shipperDTOs;
        }
        public static List<Order_Details_ExtendedDTO> ConvertToOrderDetailsExtendedsDTO(IEnumerable<Order_Details_Extended> orderDetailsExtended)
        {
            if (orderDetailsExtended == null)
                return null;

            List<Order_Details_ExtendedDTO> orderDetailsDTO = new List<Order_Details_ExtendedDTO>();

            foreach (var orderExtended in orderDetailsExtended)
            {
                Order_Details_ExtendedDTO convertedShipper = ToOrderDetailsExtendedDTO(orderExtended);

                if (convertedShipper != null)
                {
                    orderDetailsDTO.Add(convertedShipper);
                }

            }
            return orderDetailsDTO;
        }



        #endregion

        #region convert individual data structures

        private static CustomerDTO ToCustomerDTO(Customer customer)
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
                //CustomerDemographics = ConvertToCustomerDemographicDTO(customer.CustomerDemographics),
                //Orders = ConvertToOrdersDTO(customer.Orders)
            };

            return convertedCustomer;
        }

        private static ProductDTO ToProductDTO(Product product)
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
                //Order_Details = ConvertToOrderDetailsDTO(product.Order_Details),
                Supplier = ToSupplierDTO(product.Supplier),
            };

            return convertedProduct;
        }

        private static EmployeeDTO ToEmployeeDTO(Employee employee)
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
                // these conversions end up causing stack overrun as each conversion ends up with more employees/orders to process
                //Employee1 = ToEmployeeDTO(employee.Employee1),
                //Employees1 = ConvertToEmployeesDTO(employee.Employees1),
                //Orders = ConvertToOrdersDTO(employee.Orders),
                //Territories = ConvertToTerritoryDTO(employee.Territories),
            };

            return covertedEmployee;
        }

        private static CategoryDTO ToCategoryDTO(Category category)
        {
            if (category == null)
                return null;

            var convertedCategory = new CategoryDTO()
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture,
                //Products = ConvertToProductsDTO(category.Products),
            };

            return convertedCategory;
        }


        private static CustomerDemographicDTO ToCustomerDemographicDTO(CustomerDemographic demographic)
        {
            if (demographic == null)
                return null;

            var convertedDemograhic = new CustomerDemographicDTO()
            {
                CustomerTypeID = demographic.CustomerTypeID,
                CustomerDesc = demographic.CustomerDesc,
                Customers = ConvertToCustomersDTO(demographic.Customers),
            };
            return convertedDemograhic;
        }

        private static OrderDTO ToOrderDTO(Order order)
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
                Order_Details = ConvertToOrderDetailsDTO(order.Order_Details),
                //Shipper = ToShipperDTO(order.Shipper),
            };

            return convertedOrder;

        }

        private static ShipperDTO ToShipperDTO(Shipper shipper)
        {
            if (shipper == null)
                return null;

            var convertedShipper = new ShipperDTO()
            {
                ShipperID = shipper.ShipperID,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone,
                //Orders = ConvertToOrdersDTO(shipper.Orders),
            };

            return convertedShipper;
        }

        private static SupplierDTO ToSupplierDTO(Supplier supplier)
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
                //Products = ConvertToProductsDTO(supplier.Products),
            };

            return convertedSupplier;
        }

        private static Order_DetailDTO ToOrder_DetailDTO(Order_Detail orderDetail)
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
                //Order = ToOrderDTO(orderDetail.Order),
                Product = ToProductDTO(orderDetail.Product),
            };

            return convertedOrder;
        }

        private static TerritoryDTO ToTerritoryDTO(Territory territory)
        {
            if (territory == null)
                return null;

            var convertedTerritory = new TerritoryDTO()
            {
                TerritoryID = territory.TerritoryID,
                TerritoryDescription = territory.TerritoryDescription,
                RegionID = territory.RegionID,
                Region = ToRegionDTO(territory.Region),
                Employees = ConvertToEmployeesDTO(territory.Employees),
            };

            return convertedTerritory;
        }

        private static RegionDTO ToRegionDTO(Region region)
        {
            if (region == null)
                return null;

            RegionDTO convertedRegion = new RegionDTO()
            {
                RegionID = region.RegionID,
                RegionDescription = region.RegionDescription,
                //Territories = ConvertToTerritoriesDTO(region.Territories),
            };

            return convertedRegion;
        }

        private static OrderWithSubtotalDTO ToOrderWithSubtotalDTO(OrderWithSubtotal order)
        {
            if (order == null)
                return null;

            OrderWithSubtotalDTO convertedOrder = new OrderWithSubtotalDTO()
            {
                OrderID = order.OrderID,
                Customer = ToCustomerDTO(order.Customer),
                Employee = ToEmployeeDTO(order.Employee),
                Shipper = ToShipperDTO(order.Shipper),
                Subtotal = ToOrder_SubTotalDTO(order.Subtotal),
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
            };

            return convertedOrder;
        }

        private static Order_SubtotalDTO ToOrder_SubTotalDTO(Order_Subtotal subtotal)
        {
            if (subtotal == null)
                return null;

            Order_SubtotalDTO convertedSubTotal = new Order_SubtotalDTO()
            {
                OrderID = subtotal.OrderID,
                Subtotal = subtotal.Subtotal,
            };

            return convertedSubTotal;

        }

        private static Order_Details_ExtendedDTO ToOrderDetailsExtendedDTO(Order_Details_Extended orderExtended)
        {
            if (orderExtended == null)
                return null;

            Order_Details_ExtendedDTO orderDetailsConverted = new Order_Details_ExtendedDTO()
            {
                OrderID = orderExtended.OrderID,
                ProductID = orderExtended.ProductID,
                ProductName = orderExtended.ProductName,
                UnitPrice = orderExtended.UnitPrice,
                Quantity = orderExtended.Quantity,
                Discount = orderExtended.Discount,
                ExtendedPrice = orderExtended.ExtendedPrice,
            };

            return orderDetailsConverted;
        }

        private static Orders_QryDTO ToOrders_QryDTO(Orders_Qry order)
        {
            if (order == null)
                return null;

            Orders_QryDTO orderQryDTO = new Orders_QryDTO()
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
                CompanyName = order.CompanyName,
                Address = order.Address,
                City = order.City,
                Region = order.Region,
                PostalCode = order.PostalCode,
                Country = order.Country,
            };

            return orderQryDTO;
        }


        #endregion


        public static string ConvertPhotoToBase64(byte [] Photo)
        {
            string PhotoBase64 = "";
            if (Photo != null)
            {
                byte[] adjusted = new byte[Photo.Length - 78];
                Array.Copy(Photo, 78, adjusted, 0, Photo.Length - 78);


                PhotoBase64 = Convert.ToBase64String(adjusted);
            }

            return PhotoBase64;
        }

    }

}
