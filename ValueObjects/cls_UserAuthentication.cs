using FHP_Resources;
using System.ComponentModel.DataAnnotations;

namespace ValueObjects
{
    public class cls_UserAuthentication
    {
        [Required]
        public userType userId;
        /// <summary>
        /// Array storing user identification and password information.
        /// </summary>
        private string[] IdPasswords;

        /// <summary>
        /// String storing the user's name.
        /// </summary>
        private string userName;

        /// <summary>
        /// String storing the user's Password.
        /// </summary>
        private string userPassword;

        /// <summary>
        /// boolean to know sourcetype
        /// </summary>
        public bool useDatabase = true;
        /// <summary>
        /// Error message occur while saving the file
        /// </summary>
        public Messages Errormessage;

        public MessageTitle messageTitle;
        /// <summary>
        /// // Public property to access and modify the user credentials array.
        /// </summary>
        public string[] credentials
        {
            get { return IdPasswords; }
            set { IdPasswords = value; }
        }

        /// <summary>
        ///Public property providing controlled access to the user credentials array.
        /// </summary>
        public userType userType
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// Public property to access and modify the user's name.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Public property to access and modify the user's password.
        /// </summary>
        public string UserPwd
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
    }
}
