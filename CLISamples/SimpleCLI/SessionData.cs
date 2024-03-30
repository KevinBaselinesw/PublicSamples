using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    internal class SessionData
    {
        string _userName;
        string _password;
        string _sessionData;
        public SessionData(string userName, string passWord, string sessionData)
        {
            _userName = userName;
            _password = passWord;   
            _sessionData = sessionData;
        }

        public override string ToString()
        {
            return string.Format($"User:{UserName}, Pass:{Password}, Session:{sessionData}");
        }

        public string sessionData
        {
            get { return _sessionData; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
        }

    }
}
