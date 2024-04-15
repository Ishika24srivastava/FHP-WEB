using System;
using FHP_Resources;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FHP_VO
{ 
    public class cls_ValueObject
    {
        public EmployeeData EmployeeData = new EmployeeData();
        /// <summary>
        /// // Mode for editing the data (New, Edit, Delete, ReadOnly).
        /// </summary>
        public EditMode editMode;

        /// <summary>
        /// Messages related to validation of fields in secondaryform
        /// </summary>
        public ValidationMessages validationMessages;

        /// <summary>
        /// Caption associated with the operation.
        /// </summary>
        public MessageTitle messageTitle;

        public string connectionString;
        public bool useDataBase;

        public bool isDeleted = false;
        /// <summary>
        /// Error message occur while saving the file
        /// </summary>
        public string ErrorMessages;

        /// <summary>
        /// Index of the row to be deleted.
        /// </summary>
        public int deletingRow;

        /// <summary>
        /// Serial number for respective data in datatable
        /// </summary>
        public int serialNumber;

        /// <summary>
        /// Index where data need to be updated
        /// </summary>
        public int updatingIndex;

        /// <summary>
        /// Type of user form. It could be new , update or view 
        /// </summary>
        public string UserFormType = "";
        
        /// <summary>
        /// List For storing Data
        /// </summary>
        public List<EmployeeData> EmployeeDataList = new List<EmployeeData>();
    }


    public class EmployeeData
    {
        /// <summary>
        /// Serial number associated with the data
        /// </summary>
        public long SerialNumber { get; set; }

        /// <summary>
        /// Prefix for the individua
        /// </summary>
        public Prefix Prefix { get; set; }

        /// <summary>
        /// First name of the individual with a length constraint
        /// </summary>

        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Middle name of the individual with a length constraint
        /// </summary>

        [StringLength(25)]
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the individual with a length constraint
        /// </summary>

        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// It stores the Date of birth of the individual.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Current Address of the individual with a length constraint.
        /// </summary>

        [StringLength(500)]
        public string CurrentAddress { get; set; } = string.Empty;

        /// <summary>
        /// Qualification of the individual
        /// </summary>
        public Qualification Qualification { get; set; }

        /// <summary>
        /// Joining date of the individual.
        /// </summary>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Current company of the individual with a length constraint.
        /// </summary>
        [StringLength(255)]
        public string CurrentCompany { get; set; } = string.Empty;

    }
}

