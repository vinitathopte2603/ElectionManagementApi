using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class LoginResponse
    {
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public long AdminContactNumber { get; set; }
        public string AdminEmail { get; set; }
       
    }
}
