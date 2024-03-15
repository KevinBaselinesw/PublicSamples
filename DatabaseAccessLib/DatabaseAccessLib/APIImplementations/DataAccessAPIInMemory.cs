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
            using (var dbContext = new NorthWindsModel())
            {

                var Orders = (from order in dbContext.Orders
                              where order.CustomerID == CustomerID
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
        public IEnumerable<OrderDTO> GetOrdersByShipVia(int ShipVia)
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Orders = dbContext.Orders.
                        Include(nameof(Order.Employee)).
                        Include(nameof(Order.Customer)).
                        Include(nameof(Order.Order_Details)).
                        Where(t => t.ShipVia == ShipVia).
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
                        Where(t => t.CustomerID == CustomerID).
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
                var Suppliers = dbContext.Suppliers.Where(t => t.SupplierID == SupplierID).ToArray();
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
                    Where(t => t.ProductID == ProductID).ToArray();
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

            }

            return db;
        }
    }
}
