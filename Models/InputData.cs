using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class InputData
    {
        public string SerialNumber { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string CurrentCompany { get; set; }
        public DateTime JoiningDate { get; set; }
        public string CurrentAddress { get; set; }
    }
}