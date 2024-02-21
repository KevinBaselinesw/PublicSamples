using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib
{
    public interface IDataAccessAPI
    {
        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Category> GetAllProductCategories();
        IEnumerable<Category> GetProductCategoriesByID(int CategoryID);

        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsBySupplier(int SupplierID);
        IEnumerable<Product> GetProductsByCategoryID(int categoryID);

        IEnumerable<Customer> GetAllCustomers();

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByShipVia(int ShipVia);
        IEnumerable<Order> GetOrdersByEmployeeID(int EmployeeID);
        IEnumerable<Order> GetOrdersByCustomerID(string CustomerID);

        IEnumerable<Orders_Qry> GetAllOrdersQry();

        IEnumerable<Supplier> GetAllSuppliers();
        IEnumerable<Supplier> GetSuppliersByID(int SupplierID);

        IEnumerable<Shipper> GetAllShippers();

        IEnumerable<Order_Detail> GetOrderDetailsByProductID(int ProductID);

        DateTime GetLatestDateInDatabase();
        void AdjustAllDatesInDatabaseByDays(int days);
    }
}
