using DatabaseAccessLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DatabaseAccessLibUnitTests
{
    [TestClass]
    public class CustomerUnitTests
    {

        [TestMethod]
        public void ReadAllCustomersFromDatabase()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Customers = dbContext.Customers.ToArray();

                foreach (var customer in Customers)
                {
                    Console.WriteLine(customer.ContactName);
                }


            }

        }

        [TestMethod]
        public void ReadCustomersAndSuppliersByCity()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Customers = dbContext.Customer_and_Suppliers_by_Cities.ToArray();

                foreach (var customer in Customers)
                {
                    Console.WriteLine(string.Format($"City: {customer.City}, {customer.CompanyName}"));
                }


            }

        }

        [TestMethod]
        public void ReadAlphabeticalListOfProducts()
        {
            using (var dbContext = new NorthWindsModel())
            {
                var Products = dbContext.Alphabetical_list_of_products.OrderBy(t=>t.ProductName);

                foreach (var product in Products)
                {
                    Console.WriteLine(string.Format($"ID: {product.ProductID}, {product.ProductName}"));
                }


            }

        }
    }
}
