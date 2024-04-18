using FileHandlingSystem.Resources;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace FIleHandlingSystem.VO
{
    /// <summary>
    /// Class to allow communication between the various classes and DLL's throught the properties
    /// </summary>
    public class clsEmployeeVO
    {
        public EmployeeData EmployeeData;
        public string dataToStore;
        public EditMode editMode;
        public ErrorMessage error;
        public caption caption;
        public string userDefineError;

        /// <summary>
        /// Non Parameterized Constructor of Value object
        /// </summary>
        public clsEmployeeVO()
        {
            this.dataToStore = "";
            EmployeeData = new EmployeeData();
        }

        /// <summary>
        /// Parameterized Constructor of Value object
        /// </summary>
        /// <param name="data">current record</param>
        public clsEmployeeVO(string data)
        {
            this.dataToStore = data;
            EmployeeData = new EmployeeData();
        }
    }

    /// <summary>
    /// Value object class to allow communication of authentication properties
    /// </summary>
    public class AuthenticatorValueObject
    {
        
        public UserTypes userType;
        public string username;
        public string password;
    }

    /// <summary>
    /// Structure the break down the record into various properties
    /// </summary>
    public class EmployeeData
    {
        public long SerialNumber;
        public string Prefix;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public DateTime DateOfBirth;
        public byte Qualification;
        public string CurrentCompany;
        public DateTime JoiningDate;
        public string CurrentAddress;
    }

    /// <summary>
    /// class to provide static and least changeable data to various DLL's
    /// </summary>
    public class ValueLayerObject 
    {
        public UserTypes userType;
        public string[,] userData;
        public long serialNumber;
        public object[] values;
        public string UserFormType = "";
        public string dataStorageMethod;
        public string UIType;
        public string connectionString;

        /// <summary>
        /// List of records to store the user data
        /// </summary>
        public List<string> records;

        /// <summary>
        /// Assign Data to Employee Data Object from data record
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AssigndataToEMployeeData(EmployeeData vo, string data)
        {
            try
            {
                vo.SerialNumber = Convert.ToInt64(data.Substring(0, 18));
                vo.Prefix = data.Substring(18, 7).TrimEnd();
                vo.FirstName = data.Substring(25, 50).TrimEnd();
                vo.MiddleName = data.Substring(75, 25).TrimEnd();
                vo.LastName = data.Substring(100, 50).TrimEnd();
                vo.DateOfBirth = DateTime.Parse(data.Substring(150, 10));
                vo.Qualification = Byte.Parse(data.Substring(160, 2));
                vo.CurrentCompany = data.Substring(162, 255).TrimEnd();
                vo.JoiningDate = DateTime.Parse(data.Substring(417, 10));
                vo.CurrentAddress = data.Substring(427, 500).TrimEnd();
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Generate Dat Record of fixed length 927 from the Employee Data Object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public string GenerateDataString(EmployeeData vo)
        {
            return vo.SerialNumber.ToString().PadRight(18)
                 + vo.Prefix.PadRight(7)
                 + vo.FirstName.PadRight(50)
                 + vo.MiddleName.PadRight(25)
                 + vo.LastName.PadRight(50)
                 + vo.DateOfBirth.ToString("MM/dd/yyyy").PadRight(10) // Adjust date format as needed
                 + vo.Qualification.ToString().PadRight(2)
                 + vo.CurrentCompany.PadRight(255)
                 + vo.JoiningDate.ToString("MM/dd/yyyy").PadRight(10) // Adjust date format as needed
                 + vo.CurrentAddress.PadRight(500);
        }
    }

}
