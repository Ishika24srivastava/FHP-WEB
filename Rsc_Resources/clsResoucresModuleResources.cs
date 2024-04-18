using FileHandlingSystem.Resources;
using System;
using System.ComponentModel;
using System.Reflection;

namespace FileHandlingSystem.Resources
{
    /// <summary>
    /// Enum to Check the Mode in which record will be changed
    /// </summary>
    public enum EditMode
    {
        Create = 0,
        Update = 1,
        Delete = 2
    }

    /// <summary>
    /// Enum to Define to Column Names
    /// </summary>
    public enum ColumnNamesEnum
    {
        [Description("Serial Number")]
        SerialNumber,

        [Description("Prefix")]
        Prefix,

        [Description("First Name")]
        FirstName,

        [Description("Middle Name")]
        MiddleName,

        [Description("Last Name")]
        LastName,

        [Description("Date of Birth")]
        DateOfBirth,

        [Description("Qualification")]
        Qualification,

        [Description("Current Company")]
        CurrentCompany,

        [Description("Joining Date")]
        JoiningDate,

        [Description("Current Address")]
        CurrentAddress
    }

    /// <summary>
    /// Enum to Select Qualification
    /// </summary>
    public enum Qualification : byte
    {
        [Description("High School")]
        HighSchool,
        [Description("Higher Secondary School")]
        HigherSecondarySchool,
        [Description("Diploma")]
        Diploma,
        [Description("Graduated")]
        Graduation,
        [Description("Post Graduated")]
        PostGraduation,
        [Description("Ph.D")]
        Doctoral,
        [Description("P.G. Diploma")]
        PGDiploma,
        [Description("B.Sc.")]
        BSc,
        [Description("B.C.A.")]
        BCA,
        [Description("B.A.")]
        BA,
        [Description("B.Tech (C.S.E.)")]
        BTechCSE,
        [Description("B.Tech (Civil)")]
        BTechCivil,
        [Description("B.Tech (I.T)")]
        BTechIT,
        [Description("B.Tech (Mechanical)")]
        BTechME,
        [Description("B.E.")]
        BE,
        [Description("M.Sc.")]
        MSc,
        [Description("M.C.A.")]
        MCA
    }

    /// <summary>
    /// Enum to Store Error Messages
    /// </summary>
    public enum ErrorMessage
    {
        [Description("Unknown Error occured While Execution")]
        InvalidError,

        [Description("Value of Date Of Birth show be Less than Today's.")]
        errordateOfBirthGreaterThanToday,

        [Description("Value of Date Of Birth show be Less than Joininng Date")]
        errordateOfBirthGreaterThanjoiningdate,

        [Description("First Name is Required")]
        firstnameEmpty,

        [Description("Qualification is Required")]
        qualificationEmpty,

        [Description("Current Company is Required")]
        currentCompanyEmpty,

        [Description("Qualification can not be Downgraded")]
        qualificationdowngrade,

        [Description("Joining Date can Not Be Empty")]
        emptyJoiningDate
    }

    /// <summary>
    /// Enum to Store Caption
    /// </summary>
    public enum caption
    {
        [Description("Information Required")]
        invalidformdata
    }

    /// <summary>
    /// ENum to store messages that will be displayed to the user
    /// </summary>
    public enum Messages
    {
        [Description("Do you want to delete this row?")]
        deleteRecord,

        [Description("You may lost your changed data")]
        clearForm,

        [Description("Configuration File Does Not Exists")]
        configfilenotexist
    }

    /// <summary>
    /// Enum to store Message Captions
    /// </summary>
    public enum MessageCaptions
    {
        [Description("Delete Record")]
        deleteRecord,

        [Description("Clear Record")]
        clearForm,

        [Description("File Not Exist")]
        configfilenotexist,
    }

    /// <summary>
    /// Store the title for the application
    /// </summary>
    public enum Title
    {
        [Description("Add/Edit User Form")]
        addeditform,

        [Description("View User Data")]
        viewOnlyForm,
    }

    /// <summary>
    /// Enum to store User Types
    /// </summary>
    public enum UserTypes
    {
        SuperAdmin,
        Admin,
        Self,
        GuestUser
    }

    /// <summary>
    /// Class which contains methods for resources
    /// </summary>
    public class clsResoucresModuleResources
    {
        /// <summary>
        /// Get The Description of Enum, If present, otherwise return the Enum name in string
        /// </summary>
        /// <param name="value">ENum of Any TYpe</param>
        /// <returns></returns>
        public string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Function to Get the Enum Name from the Enum Description, If found otherwise return null
        /// </summary>
        /// <typeparam name="T">User Defined Enum Type</typeparam>
        /// <param name="description">Description of that Enum</param>
        /// <returns></returns>
        public string GetEnumIndexFromDescription<T>(string description) where T : Enum
        {
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0 && attributes[0].Description == description)
                {
                    return enumValue.ToString();
                }
            }
            return null;
        }
    }

}