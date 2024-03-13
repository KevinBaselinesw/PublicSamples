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

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                employeeDTOs.Add(new EmployeeDTO() { FirstName = employee.FirstName, LastName = employee.LastName });
            }
      
            return employeeDTOs;
        }
   
    }
}
