﻿/*
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
    public class DataAccessAPIInMemory : IDataAccessAPI
    {
        DatabaseBackup _db = null;

        public DataAccessAPIInMemory()
        {

        }
        public DataAccessAPIInMemory(DatabaseBackup backup)
        {
            _db = backup;
        }
        public void SetDatabase(DatabaseBackup backup)
        {
            _db = backup;
        }

        private void Validate()
        {
            if (_db == null)
                throw new ArgumentException("database has not been initialized");
        }

        /// <inheritdoc/>
        public bool DatabaseValidation()
        {
            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            Validate();

            return _db.AllEmployees;
        }

        /// <inheritdoc />
        public IEnumerable<CategoryDTO> GetAllProductCategories()
        {
            Validate();

            return _db.AllProductCategories;
        }
        /// <inheritdoc />
        public IEnumerable<CategoryDTO> GetProductCategoriesByID(int CategoryID)
        {
            Validate();

            return _db.AllProductCategories.Where(t=>t.CategoryID == CategoryID).ToList();
        }

        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            Validate();

            return _db.AllProducts;

        }
        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetProductsBySupplier(int SupplierID)
        {
            Validate();

            return _db.AllProducts.Where(t=>t.SupplierID == SupplierID).ToList();
        }
        /// <inheritdoc />
        public IEnumerable<ProductDTO> GetProductsByCategoryID(int CategoryID)
        {
            Validate();

            return _db.AllProducts.Where(t => t.CategoryID == CategoryID).ToList();
        }

        /// <inheritdoc />
        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            Validate();

            return _db.AllCustomers;
        }

        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            Validate();

            return _db.AllOrders.Select(a => new OrderDTO(a)
            {
                Employee = _db.AllEmployees.FirstOrDefault(t => t.EmployeeID == a.EmployeeID),
                Customer = _db.AllCustomers.FirstOrDefault(t => t.CustomerID == a.CustomerID),
                Order_Details = _db.AllOrderDetails.Where(t => t.OrderID == a.OrderID).ToList(),
                Shipper = _db.AllShippers.FirstOrDefault(t => t.ShipperID == a.ShipVia)
            });

        }

        /// <inheritdoc />
        public OrderDTO CreateNewOrder(OrderDTO newOrder)
        {
            if (newOrder == null)
                return null;

            _db.AllOrders.Add(NextID(newOrder));
            if (newOrder.Order_Details != null)
            {
                foreach (var od in newOrder.Order_Details)
                {
                    od.OrderID = newOrder.OrderID;
                    _db.AllOrderDetails.Add(od);
                    _db.All_OrderDetailsExtended.Add(Convert(od));
                }
            }
    
            _db.All_OrderSubtotals.Add(ConvertToSubtotals(newOrder));

            return newOrder;
        }

        private Order_SubtotalDTO ConvertToSubtotals(OrderDTO od)
        {
            Order_SubtotalDTO newOST = new Order_SubtotalDTO();
            newOST.OrderID = od.OrderID;
            newOST.Subtotal = 0;

            if (od.Order_Details != null)
            {
                foreach (var detail in od.Order_Details)
                {
                    newOST.Subtotal += detail.UnitPrice * detail.Quantity;
                }
            }

            return newOST;
        }

        private Order_Details_ExtendedDTO Convert(Order_DetailDTO od)
        {
            Order_Details_ExtendedDTO extendedDTO = new Order_Details_ExtendedDTO();
            extendedDTO.OrderID = od.OrderID;
            extendedDTO.ProductID = od.ProductID;
            extendedDTO.ProductName = _db.AllProducts.First(t=>t.ProductID == od.ProductID).ProductName;
            extendedDTO.UnitPrice = od.UnitPrice;
            extendedDTO.Quantity = od.Quantity;
            extendedDTO.Discount = od.Discount;
            extendedDTO.ExtendedPrice = od.UnitPrice - (od.UnitPrice * (decimal)od.Discount);

            return extendedDTO;
        }

        private OrderDTO NextID(OrderDTO newOrder)
        {
            int MaxId = _db.AllOrders.Max(x => x.OrderID);
            newOrder.OrderID = MaxId + 1;
            return newOrder;
        }


        /// <inheritdoc />
        public int GetOrdersCount()
        {
            Validate();

            return _db.AllOrders.Count();
        }

        /// <inheritdoc />
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotals()
        {
            Validate();

            return _db.AllOrders.Select(a => new OrderWithSubtotalDTO()
            {
                OrderID = a.OrderID,
                OrderDate = a.OrderDate,
                RequiredDate = a.RequiredDate,
                ShippedDate = a.ShippedDate,

                Customer = _db.AllCustomers.FirstOrDefault(t=>t.CustomerID == a.CustomerID),
                Employee = _db.AllEmployees.FirstOrDefault(t=>t.EmployeeID == a.EmployeeID),
                Shipper = _db.AllShippers.FirstOrDefault(t=>t.ShipperID == a.ShipVia),
                Subtotal = _db.All_OrderSubtotals.FirstOrDefault(t => t.OrderID == a.OrderID),
            });
    
        }

        /// <inheritdoc />
        public IEnumerable<OrderWithSubtotalDTO> GetAllOrdersWithSubtotalsByCustomerID(string CustomerID)
        {
            Validate();

            return _db.AllOrders.Where(t=>t.CustomerID == CustomerID).Select(a => new OrderWithSubtotalDTO()
            {
                OrderID = a.OrderID,
                OrderDate = a.OrderDate,
                RequiredDate = a.RequiredDate,
                ShippedDate = a.ShippedDate,

                Customer = _db.AllCustomers.FirstOrDefault(t => t.CustomerID == a.CustomerID),
                Employee = _db.AllEmployees.FirstOrDefault(t => t.EmployeeID == a.EmployeeID),
                Shipper = _db.AllShippers.FirstOrDefault(t => t.ShipperID == a.ShipVia),
                Subtotal = _db.All_OrderSubtotals.FirstOrDefault(t => t.OrderID == a.OrderID),
            });
     
        }


        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            Validate();

            return _db.AllOrders.Where(t=>t.ShipVia == ShipVia).Select(a => new OrderDTO(a)
            {
                Employee = _db.AllEmployees.FirstOrDefault(t => t.EmployeeID == a.EmployeeID),
                Customer = _db.AllCustomers.FirstOrDefault(t => t.CustomerID == a.CustomerID),
                Order_Details = _db.AllOrderDetails.Where(t => t.OrderID == a.OrderID).ToList(),
            });

    
        }

        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByEmployeeID(int EmployeeID)
        {
            Validate();

            return _db.AllOrders.Where(t=>t.EmployeeID == EmployeeID).Select(a => new OrderDTO(a)
            {
                Employee = _db.AllEmployees.FirstOrDefault(t => t.EmployeeID == a.EmployeeID),
                Customer = _db.AllCustomers.FirstOrDefault(t => t.CustomerID == a.CustomerID),
                Order_Details = _db.AllOrderDetails.Where(t => t.OrderID == a.OrderID).ToList(),
                Shipper = _db.AllShippers.FirstOrDefault(t => t.ShipperID == a.ShipVia)
            });

        }
        /// <inheritdoc />
        public IEnumerable<OrderDTO> GetOrdersByCustomerID(string CustomerID)
        {
            Validate();

            return _db.AllOrders.Where(t => t.CustomerID == CustomerID).Select(a => new OrderDTO(a)
            {
                Employee = _db.AllEmployees.FirstOrDefault(t => t.EmployeeID == a.EmployeeID),
                Customer = _db.AllCustomers.FirstOrDefault(t => t.CustomerID == a.CustomerID),
                Order_Details = _db.AllOrderDetails.Where(t => t.OrderID == a.OrderID).ToList(),
                Shipper = _db.AllShippers.FirstOrDefault(t => t.ShipperID == a.ShipVia)
            });
  
        }

        /// <inheritdoc />
        public IEnumerable<Orders_QryDTO> GetAllOrdersQry()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            return _db.AllSuppliers;
        }
        /// <inheritdoc />
        public IEnumerable<SupplierDTO> GetSuppliersByID(int SupplierID)
        {
            return _db.AllSuppliers.Where(t=>t.SupplierID == SupplierID).ToList();
        }

        /// <inheritdoc />
        public IEnumerable<ShipperDTO> GetAllShippers()
        {
            return _db.AllShippers;
        }

        /// <inheritdoc />
        public IEnumerable<Order_DetailDTO> GetOrderDetailsByProductID(int ProductID)
        {
            Validate();

            return _db.AllOrderDetails.Where(t => t.ProductID == ProductID).Select(a => new Order_DetailDTO(a)
            {
                Order = _db.AllOrders.FirstOrDefault(t=>t.OrderID == a.OrderID),
                Product = _db.AllProducts.FirstOrDefault(t=>t.ProductID == a.ProductID),
            });
  
        }

        /// <inheritdoc />
        public IEnumerable<Order_Details_ExtendedDTO> GetOrderDetailsByOrderID(int OrderID)
        {
            return _db.All_OrderDetailsExtended.Where(t=>t.OrderID == OrderID);
        }


        /// <inheritdoc />
        public DateTime GetLatestDateInDatabase()
        {
            List<DateTime?> AllDates = new List<DateTime?>();

            AllDates.AddRange(_db.AllEmployees.Select(t => t.BirthDate).ToList());
            AllDates.AddRange(_db.AllEmployees.Select(t => t.HireDate).ToList());
            AllDates.AddRange(_db.AllOrders.Select(t => t.OrderDate).ToList());
            AllDates.AddRange(_db.AllOrders.Select(t => t.RequiredDate).ToList());
            AllDates.AddRange(_db.AllOrders.Select(t => t.ShippedDate).ToList());

            AllDates.Sort();

            var LastDate = AllDates.LastOrDefault();

            return LastDate.Value;

        }

        /// <inheritdoc />
        public void AdjustAllDatesInDatabaseByDays(int days)
        {
            if (days == 0)
                return;

            var EmployeeDates = _db.AllEmployees;
            foreach (var ed in EmployeeDates)
            {
                if (ed.BirthDate != null)
                    ed.BirthDate = ed.BirthDate?.AddDays(days);
                if (ed.HireDate != null)
                    ed.HireDate = ed.HireDate?.AddDays(days);
            }

            var OrderDates = _db.AllOrders;
            foreach (var od in OrderDates)
            {
                if (od.OrderDate != null)
                    od.OrderDate = od.OrderDate?.AddDays(days);
                if (od.RequiredDate != null)
                    od.RequiredDate = od.RequiredDate?.AddDays(days);
                if (od.ShippedDate != null)
                    od.ShippedDate = od.ShippedDate?.AddDays(days);
            }

        }

        public DatabaseBackup GetDatabaseBackup()
        {
            return _db;
        }
    }
}
