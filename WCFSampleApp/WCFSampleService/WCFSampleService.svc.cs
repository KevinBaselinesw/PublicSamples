using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DatabaseAccessLib;

namespace WCFSampleApp
{
    /// <summary>
    /// This is the implementation of the IWCFSampleService
    /// </summary>
    public class WCFSampleService : IWCFSampleService
    {
        static DataAccessAPIDB DatabaseAPI = null;

        private DataAccessAPIDB GetDatabaseAPI()
        {
            if (DatabaseAPI == null)
            {
                DatabaseAPI = new DataAccessAPIDB();
            }
            return DatabaseAPI;
        }



        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            DataAccessAPIDB DatabaseAPI = GetDatabaseAPI();

            var employees = DatabaseAPI.GetAllEmployees();

            return ConvertToDTO(employees);
        }

        private IEnumerable<EmployeeDTO> ConvertToDTO(IEnumerable<Employee> employees)
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
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
                };

                employeeDTOs.Add(covertedEmployee);
            }

            return employeeDTOs;
        }
   

    }
}
