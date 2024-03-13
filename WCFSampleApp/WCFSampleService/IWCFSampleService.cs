using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFSampleApp
{
    /// <summary>
    /// This is the definition of the WCF Sample application
    /// </summary>
    [ServiceContract]
    public interface IWCFSampleService
    {
        [OperationContract]
        IEnumerable<EmployeeDTO> GetAllEmployees();
  
    }

    [DataContract]
    public class EmployeeDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

    }

}
