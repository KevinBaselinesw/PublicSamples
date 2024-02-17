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
    }
}
