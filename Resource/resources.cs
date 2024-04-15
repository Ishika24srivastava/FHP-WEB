using System;
using System.ComponentModel;
using System.Reflection;

namespace FHP_Resources
{
    /// <summary>
    /// Custom attribute to associate an academic level with each qualification.
    /// </summary>
    public class AcademicLevelAttribute : Attribute
    {
        public int LevelofAcademic { get; }

        public AcademicLevelAttribute(int level)
        {
            LevelofAcademic = level;
        }
    }

    /// <summary>
    ///  Enum representing different Qualification for names
    /// </summary>
    public enum Qualification : byte
    {
        [AcademicLevel(0)]
        [Description("Select")]
        Select = 0,

        [AcademicLevel(1)]
        [Description("10th Grade")]
        TenthGrade,

        [AcademicLevel(2)]
        [Description("12th Grade")]
        TwelfthGrade,

        [AcademicLevel(3)]
        [Description("Diploma")]
        Diploma,

        [AcademicLevel(3)]
        [Description("Postgraduate Diploma")]
        PGDiploma,

        [AcademicLevel(4)]
        [Description("Bachelor of Science")]
        BSc,

        [AcademicLevel(4)]
        [Description("Bachelor of Arts")]
        BA,

        [AcademicLevel(4)]
        [Description("Bachelor of Technology")]
        BTech,

        [AcademicLevel(4)]
        [Description("Bachelor of Commerce")]
        BCom,

        [AcademicLevel(4)]
        [Description("Bachelor of Business Administration")]
        BBA,

        [AcademicLevel(4)]
        [Description("Bachelor of Computer Applications")]
        BCA,

        [AcademicLevel(4)]
        [Description("Bachelor of Architecture")]
        BArch,

        [AcademicLevel(4)]
        [Description("Bachelor of Dental Surgery")]
        BDS,

        [AcademicLevel(4)]
        [Description("Bachelor of Pharmacy")]
        BPharm,

        [AcademicLevel(4)]
        [Description("Bachelor of Education")]
        BEd,

        [AcademicLevel(4)]
        [Description("Bachelor of Engineering")]
        BE,

        [AcademicLevel(4)]
        [Description("Bachelor of Commerce (Hons)")]
        BComHons,

        [AcademicLevel(4)]
        [Description("Bachelor of Commerce LLB")]
        BComLLB,

        // Master's level qualifications

        [AcademicLevel(5)]
        [Description("Master of Science")]
        MSc,

        [AcademicLevel(5)]
        [Description("Master of Arts")]
        MA,

        [AcademicLevel(5)]
        [Description("Master of Technology")]
        MTech,

        [AcademicLevel(5)]
        [Description("Master of Commerce")]
        MCom,

        [AcademicLevel(5)]
        [Description("Master of Business Administration")]
        MBA,

        [AcademicLevel(5)]
        [Description("Master of Computer Applications")]
        MCA,

        [AcademicLevel(5)]
        [Description("Master of Architecture")]
        MArch,

        [AcademicLevel(5)]
        [Description("Master of Dental Surgery")]
        MDS,

        [AcademicLevel(5)]
        [Description("Master of Pharmacy")]
        MPharm,

        [AcademicLevel(5)]
        [Description("Master of Education")]
        MEd,

        [AcademicLevel(5)]
        [Description("Master of Social Work")]
        MSW,

        [AcademicLevel(5)]
        [Description("Master of Fine Arts")]
        MFA,

        [AcademicLevel(5)]
        [Description("Master of Design")]
        MDes,

        [AcademicLevel(5)]
        [Description("Master of Engineering")]
        MEng,

        [AcademicLevel(5)]
        [Description("Master of Laws (LLM)")]
        LLM,

        [AcademicLevel(5)]
        [Description("Doctor of Medicine (MD)")]
        MD,

        [AcademicLevel(5)]
        [Description("Master of Optometry (MOptom)")]
        MOptom,

        [AcademicLevel(5)]
        [Description("Master of Veterinary Science (MVSc)")]
        MVSc,

        [AcademicLevel(5)]
        [Description("Master of Library Science (MLib)")]
        MLib,

        [AcademicLevel(5)]
        [Description("Master of Public Health (MPH)")]
        MPH,

        [AcademicLevel(5)]
        [Description("Master of Physiotherapy (MPT)")]
        MPT,

        [AcademicLevel(5)]
        [Description("Master of Music (MMus)")]
        MMus,

        [AcademicLevel(5)]
        [Description("Master of Management (MMgt)")]
        MMgt,

        [AcademicLevel(5)]
        [Description("Master of Human Resource Management (MHRM)")]
        MHRM,

        [AcademicLevel(5)]
        [Description("Master of Journalism (MJ)")]
        MJ,

        [AcademicLevel(5)]
        [Description("Master of Elementary Education (MElEd)")]
        MElEd,

        [AcademicLevel(5)]
        [Description("Master of Physical Education (MPEd)")]
        MPEd,

        [AcademicLevel(5)]
        [Description("Master of Economics (ME)")]
        ME,

        [AcademicLevel(5)]
        [Description("Master of Commerce (Hons)")]
        MComHons,

        [AcademicLevel(5)]
        [Description("Master of Commerce LL.M.")]
        MComLLM,

        [AcademicLevel(5)]
        [Description("Master of Information Technology (MIT)")]
        MIT,

        [AcademicLevel(5)]
        [Description("Master of Computer Science (MCS)")]
        MCS,

        [AcademicLevel(6)]
        [Description("Doctor of Philosophy (PhD)")]
        PhD,

        [AcademicLevel(7)]
        [Description("Others")]
        Others
    }

    /// <summary>
    ///  Enum representing different prefixes for names
    /// </summary>
    public enum Prefix : byte
    {
        [Description("Select")]
        Select,
        [Description("Mr.")]
        Mr,
        [Description("Mrs.")]
        Mrs,
        [Description("Miss")]
        Miss,
        [Description("Dr.")]
        Dr,
        [Description("Prof.")]
        Prof
    }

    /// <summary>
    /// enum representing user type 
    /// </summary>
    public enum userType 
    {
        [Description("Admin")]
        admin,
        [Description("Super Admin")]
        superAdmin,
        [Description("Self")]
        self,
        [Description("Guest User")]
        guestUser
    }
    /// <summary>
    /// Enum representing various editmodes 
    /// </summary>
    public enum EditMode
    {
        New,
        Edit,
        ReadOnly
    }
    /// <summary>
    /// Enum representing validation messages for each fields
    /// </summary>
    public enum ValidationMessages
    {
        [Description("First Name is required.")]
        FirstNameRequired,

        [Description("First Name contains invalid characters.")]
        FirstNameInvalidCharacters,

        [Description("Last Name contains invalid characters.")]
        LastNameInvalidCharacters,

        [Description("Middle Name contains invalid characters.")]
        MiddleNameInvalidCharacters,

        [Description("Qualification is required.")]
        QualificationRequired,

        [Description("Qualification Can't be Downgraded")]
        QualificationDowngraded,

        [Description("Invalid Date of Birth. Date Of Birth should be Less than Today's Date")]
        DateOfBirthInvalid,

        [Description("Invalid Date of Birth. Date Of Birth should be Less than Joininng Date")]
        DateOfBirthIsMore,

        [Description("Invalid Joininng Date. Joininng Date show not be more than Today's Date")]
        JoiningDateInvalid,

        [Description("Current Company is required.")]
        CurrentCompanyRequired
    }

    /// <summary>
    /// Enum representing various title of message boxs
    /// </summary>
    public enum MessageTitle
    {
        [Description("Information Required")]
        InvalidFormData
    }

    /// <summary>
    /// Enum representing various messages
    /// </summary>
    public enum Messages
    {
        [Description("Do you want to delete this row?")]
        DeleteRecord,

        [Description("You may lose your data")]
        ClearForm,

        [Description("UserType doesnt match with user id and password")]
        userType,
        
        [Description("Wrong User id or Password")]
        idPw,

    }

    /// <summary>
    /// Enum representing MessageCaptions  of message boxs
    /// </summary>
    public enum MessageCaptions
    {
        [Description("Delete Record")]
        DeleteRecord,

        [Description("Clear Record")]
        ClearForm
    }

    /// <summary>
    ///  Enum representing titles for forms
    /// </summary>
    public enum Title
    {
        [Description("User Profile - Add/Edit")]
        AddEditForm,

        [Description("View User Details")]
        ViewOnlyForm,
    }

   
    public static class QualificationExtensions
    {
        /// <summary>
        /// Gets the academic level associated with a qualification using the AcademicLevelAttribute.
        /// </summary>
        /// <param name="qualification">The qualification enum value.</param>
        /// <returns>The academic level of the qualification.</returns>
        public static int GetAcademicLevel(this Qualification qualification)
        {
            var attribute = (AcademicLevelAttribute)Attribute.GetCustomAttribute(
                qualification.GetType().GetField(qualification.ToString()),
                typeof(AcademicLevelAttribute)
            );
            return attribute?.LevelofAcademic ?? 0;
        }
    }
       
    public class resources
    {
        /// <summary>
        /// Checks if downgrading is happening from currentQualification to newQualification.
        /// </summary>
        public bool isDowngrading(Qualification currentQualification, Qualification newQualification)
        {
            int currentLevel = currentQualification.GetAcademicLevel();
            int newLevel = newQualification.GetAcademicLevel();
            if(currentLevel > newLevel)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the description associated with an enum value.
        /// </summary>
        public string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Gets the string representation of an enum value based on its description.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="description">The description to match.</param>
        /// <returns>The string representation of the matched enum value, or an empty string if not found.</returns>
        public string GetEnumValueFromDescription<T>(string description) where T : Enum
        {
            resources rsc = new resources();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (rsc.GetEnumDescription(value) == description)
                {
                    return value.ToString();
                }
            }
            return string.Empty;
        }

    }
}
