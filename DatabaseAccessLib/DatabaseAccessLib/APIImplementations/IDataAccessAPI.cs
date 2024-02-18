﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib
{
    public interface IDataAccessAPI
    {
        IEnumerable<Employee> GetAllEmployees();
    }
}