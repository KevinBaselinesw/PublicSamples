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
        public DataAccessAPIInMemory(DatabaseBackup backup)
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
