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
    public class OrderWithSubtotal
    {
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Shipper Shipper { get; set; }
        public Order_Subtotal Subtotal { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
    }

    /// <summary>
    /// This interface defines the database access of this system
    /// </summary>
    public interface IDataAccessAPI
    {
        /// <summary>
        /// Returns a collection containg all employees in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetAllEmployees();

        /// <summary>
        /// Returns a collection of all the product categories in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetAllProductCategories();
        /// <summary>
        /// Returns a collection of product categories matching a specific category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        IEnumerable<Category> GetProductCategoriesByID(int CategoryID);

        /// <summary>
        /// Returns a collection of all product records in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetAllProducts();


        /// <summary>
        /// Returns a collection of products that are from a specific supplier
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        IEnumerable<Product> GetProductsBySupplier(int SupplierID);
        /// <summary>
        /// Returns a collection of products that are from a specific category of product types
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        IEnumerable<Product> GetProductsByCategoryID(int categoryID);

        /// <summary>
        /// Returns a collection of all the customers in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetAllCustomers();

        /// <summary>
        /// Returns a collection of all orders in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetAllOrders();


        IEnumerable<OrderWithSubtotal> GetAllOrdersWithSubtotals();

        /// <summary>
        /// Returns a collection of orders that have been shipped by a specific shipper
        /// </summary>
        /// <param name="ShipVia"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrdersByShipVia(int ShipVia);
        /// <summary>
        /// Returns a collection of orders that were sold by a specific employee
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrdersByEmployeeID(int EmployeeID);
        /// <summary>
        /// Returns a collection of orders that have been purchased by a specific customer
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrdersByCustomerID(string CustomerID);

        /// <summary>
        /// Returns a collection or Orders Qry
        /// </summary>
        /// <returns></returns>
        IEnumerable<Orders_Qry> GetAllOrdersQry();

        /// <summary>
        /// Returns a collection of all suppliers in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Supplier> GetAllSuppliers();
        /// <summary>
        /// Returns a collection or suppliers with a specific ID
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        IEnumerable<Supplier> GetSuppliersByID(int SupplierID);

        /// <summary>
        /// Returns a collection of all shippers in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Shipper> GetAllShippers();

        /// <summary>
        /// Returns a collection of Order Details matching a specific product ID
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        IEnumerable<Order_Detail> GetOrderDetailsByProductID(int ProductID);

        /// <summary>
        /// Returns the most recent date value from the database.  Used when trying to adjust the data to different dates.
        /// </summary>
        /// <returns></returns>
        DateTime GetLatestDateInDatabase();
        /// <summary>
        /// Updates the database by adjusting date fields a specific number of days.  Can be negative number.
        /// </summary>
        /// <param name="days"></param>
        void AdjustAllDatesInDatabaseByDays(int days);
    }
}
