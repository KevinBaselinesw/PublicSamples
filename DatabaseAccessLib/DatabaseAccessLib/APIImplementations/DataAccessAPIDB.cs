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
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib
{
    /// <summary>
    /// This is the SQL server/entityframework implementation of the IDataAccessAPI
    /// </summary>
    public class DataAccessAPIDB : IDataAccessAPI
    {

        /// <inheritdoc/>
        public bool DatabaseValidation()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Employee = dbContext.Employees.FirstOrDefault();
                return Employee != null;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Employees = dbContext.Employees.ToArray();

                return DTOConversions.ConvertToEmployeesDTO(Employees);
            }
        }

        /// <inheritdoc />
        public IEnumerable<CategoryDTO> GetAllProductCategories()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.ToArray();
                return DTOConversions.ConvertToCategoriesDTO(Categories);
            }
        }
        /// <inheritdoc />
        public IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.Where(t=>t.CategoryID == CategoryID).ToArray();
                return DTOConversions.ConvertToCategoriesDTO(Categories);
            }
        }

        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.ToArray();
                return DTOConversions.ConvertToProductsDTO(Products);
            }
        }
        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t=>t.SupplierID == SupplierID).ToArray();
                return DTOConversions.ConvertToProductsDTO(Products);
            }
        }
        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t => t.CategoryID == CategoryID).ToArray();
                return DTOConversions.ConvertToProductsDTO(Products);
            }
        }

        /// <inheritdoc />
        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Customers = dbContext.Customers.ToArray();
                return DTOConversions.ConvertToCustomersDTO(Customers);
            }
        }

        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Include(nameof(Order.Shipper)).
                        ToArray();
                return DTOConversions.ConvertToOrdersDTO(Orders);
            }
        }

        public OrderDTO CreateNewOrder(OrderDTO newOrder)
        {
            if (newOrder == null)
                return null;

            Order order = new Order();
            order.CustomerID = newOrder.CustomerID;
            order.EmployeeID = newOrder.EmployeeID;
            order.OrderDate = newOrder.OrderDate;
            order.RequiredDate = newOrder.RequiredDate;
            order.ShippedDate = newOrder.ShippedDate;
            order.ShipVia = newOrder.ShipVia;

            // Fill out the rest of the order.  Doesn't appear to be used
            order.Freight = newOrder.Freight;
            order.ShipName = newOrder.ShipName;
            order.ShipAddress = newOrder.ShipAddress;
            order.ShipCity = newOrder.ShipCity;
            order.ShipRegion = newOrder.ShipRegion;
            order.ShipPostalCode = newOrder.ShipPostalCode;
            order.ShipCountry = newOrder.ShipCountry;
            // Fill out the rest of the order.  Doesn't appear to be used

            if (newOrder.Order_Details != null)
            {
                foreach (var odDTO in newOrder.Order_Details)
                {
                    Order_Detail od = new Order_Detail();
                    od.ProductID = odDTO.ProductID;
                    od.Quantity = odDTO.Quantity;
                    od.UnitPrice = odDTO.UnitPrice;
                    od.Discount = odDTO.Discount;

                    order.Order_Details.Add(od);
                }
            }
   

            using (var dbContext = new NorthWindsModel())
            {
                var MyOrder = dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                     
                var DTOArray = DTOConversions.ConvertToOrdersDTO(new[] { MyOrder });
                return DTOArray[0];
            }
        }

        /// <inheritdoc />
        public int GetOrdersCount()
        {
            using (var dbContext = new NorthWindsModel())
            {
                return dbContext.Orders.Count();
            }
        }

        /// <inheritdoc />
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals()
        {
            using (var dbContext = new NorthWindsModel())
            {

                var Orders = (from order in dbContext.Orders
                              join employee in dbContext.Employees on order.EmployeeID equals employee.EmployeeID
                              join customer in dbContext.Customers on order.CustomerID equals customer.CustomerID
                              join orderSubtotals in dbContext.Order_Subtotals on order.OrderID equals orderSubtotals.OrderID
                              join shipper in dbContext.Shippers on order.ShipVia equals shipper.ShipperID
                              select new OrderWithSubtotal
                              {
                                  OrderID = order.OrderID,
                                  OrderDate = order.OrderDate,
                                  RequiredDate = order.RequiredDate,
                                  ShippedDate = order.ShippedDate,
                                  Customer = customer,
                                  Employee = employee,
                                  Shipper = shipper,
                                  Subtotal = orderSubtotals,
                              }
                              ).ToArray();

                return DTOConversions.ConvertToOrderWithSubtotalsDTO(Orders);
            }
        }

        /// <inheritdoc />
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID)
        {
            using (var dbContext = new NorthWindsModel())
            {

                var Orders = (from order in dbContext.Orders where order.CustomerID == CustomerID
                              join employee in dbContext.Employees on order.EmployeeID equals employee.EmployeeID
                              join customer in dbContext.Customers on order.CustomerID equals customer.CustomerID
                              join orderSubtotals in dbContext.Order_Subtotals on order.OrderID equals orderSubtotals.OrderID
                              join shipper in dbContext.Shippers on order.ShipVia equals shipper.ShipperID
                              
                              select new OrderWithSubtotal
                              {
                                  OrderID = order.OrderID,
                                  OrderDate = order.OrderDate,
                                  RequiredDate = order.RequiredDate,
                                  ShippedDate = order.ShippedDate,
                                  Customer = customer,
                                  Employee = employee,
                                  Shipper = shipper,
                                  Subtotal = orderSubtotals,
                              } 
                              ) .ToArray();

                return DTOConversions.ConvertToOrderWithSubtotalsDTO(Orders);
            }
        }


        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Where(t=>t.ShipVia == ShipVia).
                        ToArray();

                return DTOConversions.ConvertToOrdersDTO(Orders);
            }
        }

        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Include(nameof(Order.Shipper)).
                        Where(t => t.EmployeeID == EmployeeID).
                        ToArray();

                return DTOConversions.ConvertToOrdersDTO(Orders);
            }
        }
        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Include(nameof(Order.Shipper)).
                        Where(t=>t.CustomerID == CustomerID).
                        ToArray();

                return DTOConversions.ConvertToOrdersDTO(Orders);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Orders_QryDTO> GetAllOrdersQry()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders_Qries.ToArray();
                return DTOConversions.ConvertToOrdersQrysDTO(Orders);
            }
        }

        /// <inheritdoc />
        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.ToArray();
                return DTOConversions.ConvertToSuppliersDTO(Suppliers);
            }
        }
        /// <inheritdoc />
        public IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.Where(t=>t.SupplierID == SupplierID).ToArray();
                return DTOConversions.ConvertToSuppliersDTO(Suppliers);
            }
        }

        /// <inheritdoc />
        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Shippers = dbContext.Shippers.ToArray();
                return DTOConversions.ConvertToShippersDTO(Shippers);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var OrderDetails = dbContext.Order_Details.
                    Include(nameof(Order_Detail.Order)).
                    Include(nameof(Order_Detail.Product)).
                    Where(t=>t.ProductID == ProductID).ToArray();
                return DTOConversions.ConvertToOrderDetailsDTO(OrderDetails);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var OrderDetails = dbContext.Order_Details_Extendeds.
                    Where(t => t.OrderID == OrderID).ToArray();
                return DTOConversions.ConvertToOrderDetailsExtendedsDTO(OrderDetails);
            }
        }


        /// <inheritdoc />
        public DateTime GetLatestDateInDatabase()
        {
            using (var dbContext = new NorthWindsModel())
            {
                List<DateTime?> AllDates = new List<DateTime?>();

                AllDates.AddRange(dbContext.Employees.Select(t => t.BirthDate).ToList());
                AllDates.AddRange(dbContext.Employees.Select(t => t.HireDate).ToList());
                AllDates.AddRange(dbContext.Orders.Select(t => t.OrderDate).ToList());
                AllDates.AddRange(dbContext.Orders.Select(t => t.RequiredDate).ToList());
                AllDates.AddRange(dbContext.Orders.Select(t => t.ShippedDate).ToList());

                AllDates.Sort();

                var LastDate = AllDates.LastOrDefault();

                return LastDate.Value;
            }
        }

        /// <inheritdoc />
        public void AdjustAllDatesInDatabaseByDays(int days)
        {
            if (days == 0)
                return;

            using (var dbContext = new NorthWindsModel())
            {
                var EmployeeDates = dbContext.Employees;
                foreach (var ed in EmployeeDates)
                {
                    if (ed.BirthDate != null)
                        ed.BirthDate = ed.BirthDate?.AddDays(days);
                    if (ed.HireDate != null)
                        ed.HireDate = ed.HireDate?.AddDays(days);
                }

                var OrderDates = dbContext.Orders;
                foreach (var od in OrderDates)
                {
                    if (od.OrderDate != null)
                        od.OrderDate = od.OrderDate?.AddDays(days);
                    if (od.RequiredDate != null)
                        od.RequiredDate = od.RequiredDate?.AddDays(days);
                    if (od.ShippedDate != null)
                        od.ShippedDate = od.ShippedDate?.AddDays(days);
                }

                dbContext.SaveChanges();
            }
        }

        public DatabaseBackup GetDatabaseBackup()
        {
            DatabaseBackup db = new DatabaseBackup();

            using (var dbContext = new NorthWindsModel())
            {
                var productCategories = dbContext.Categories.ToArray();
                db.AllProductCategories = DTOConversions.ConvertToCategoriesDTO(productCategories);

                var customers = dbContext.Customers.ToArray();
                db.AllCustomers = DTOConversions.ConvertToCustomersDTO(customers);

                var Employees = dbContext.Employees.ToArray();
                db.AllEmployees = DTOConversions.ConvertToEmployeesDTO(Employees);

                var OrderDetails = dbContext.Order_Details.ToArray();
                db.AllOrderDetails = DTOConversions.ConvertToOrderDetailsDTO(OrderDetails);

                var Orders = dbContext.Orders.ToArray();
                db.AllOrders = DTOConversions.ConvertToOrdersDTO(Orders);

                var Products = dbContext.Products.ToArray();
                db.AllProducts = DTOConversions.ConvertToProductsDTO(Products);

                var Regions = dbContext.Regions.ToArray();
                db.AllRegions = DTOConversions.ConvertToRegionsDTO(Regions);

                var Shippers = dbContext.Shippers.ToArray();
                db.AllShippers = DTOConversions.ConvertToShippersDTO(Shippers);

                var Suppliers = dbContext.Suppliers.ToArray();
                db.AllSuppliers = DTOConversions.ConvertToSuppliersDTO(Suppliers);

                var Territories = dbContext.Territories.ToArray();
                db.AllTerritories = DTOConversions.ConvertToTerritoriesDTO(Territories);

                var Order_Subtotals = dbContext.Order_Subtotals.ToArray();
                db.All_OrderSubtotals = DTOConversions.ConvertToOrderSubTotalsDTO(Order_Subtotals);

                var Order_Details_Extended = dbContext.Order_Details_Extendeds.ToArray();
                db.All_OrderDetailsExtended = DTOConversions.ConvertToOrderDetailsExtendedsDTO(Order_Details_Extended);


            }

            return db;
        }
    }
}
