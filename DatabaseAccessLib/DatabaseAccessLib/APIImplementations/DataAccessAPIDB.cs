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
        public IEnumerable<Employee> GetAllEmployees()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Employees = dbContext.Employees.ToArray();
                return Employees;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Category> GetAllProductCategories()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.ToArray();
                return Categories;
            }
        }
        /// <inheritdoc />
        public IEnumerable<Category> GetProductCategoriesByID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Categories = dbContext.Categories.Where(t=>t.CategoryID == CategoryID).ToArray();
                return Categories;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Product> GetAllProducts()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.ToArray();
                return Products;
            }
        }
        /// <inheritdoc />
        public IEnumerable<Product> GetProductsBySupplier(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t=>t.SupplierID == SupplierID).ToArray();
                return Products;
            }
        }
        /// <inheritdoc />
        public IEnumerable<Product> GetProductsByCategoryID(int CategoryID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Products.Where(t => t.CategoryID == CategoryID).ToArray();
                return Products;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Customers = dbContext.Customers.ToArray();
                return Customers;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order> GetAllOrders()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Include(nameof(Order.Shipper)).
                        ToArray();
                return Orders;
            }
        }

        /// <inheritdoc />
        public IEnumerable<OrderWithSubtotal> GetAllOrdersWithSubtotals()
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

                return Orders;
            }
        }


        /// <inheritdoc />
        public IEnumerable<Order> GetOrdersByShipVia(int ShipVia)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Where(t=>t.ShipVia == ShipVia).
                        ToArray();

                return Orders;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order> GetOrdersByEmployeeID(int EmployeeID)
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

                return Orders;
            }
        }
        /// <inheritdoc />
        public IEnumerable<Order> GetOrdersByCustomerID(string CustomerID)
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

                return Orders;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Orders_Qry> GetAllOrdersQry()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders_Qries.ToArray();
                return Orders;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.ToArray();
                return Suppliers;
            }
        }
        /// <inheritdoc />
        public IEnumerable<Supplier> GetSuppliersByID(int SupplierID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Suppliers = dbContext.Suppliers.Where(t=>t.SupplierID == SupplierID).ToArray();
                return Suppliers;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Shipper> GetAllShippers()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Shippers = dbContext.Shippers.ToArray();
                return Shippers;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order_Detail> GetOrderDetailsByProductID(int ProductID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var OrderDetails = dbContext.Order_Details.
                    Include(nameof(Order_Detail.Order)).
                    Include(nameof(Order_Detail.Product)).
                    Where(t=>t.ProductID == ProductID).ToArray();
                return OrderDetails;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Order_Details_Extended> GetOrderDetailsByOrderID(int OrderID)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var OrderDetails = dbContext.Order_Details_Extendeds.
                    Where(t => t.OrderID == OrderID).ToArray();
                return OrderDetails;
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
    }
}
