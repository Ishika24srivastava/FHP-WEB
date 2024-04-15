using System;
using System.Windows.Forms;
using FHP_BL;
using FHP_Resources;
using FHP_VO;

namespace FHP
{
    public partial class UserAuthentication : Form
    {  
        /// <summary>
        /// Flag indicating whether the user is logged in.
        /// </summary>
        public bool loggedin { get; private set; }
        ValueObjects.cls_UserAuthentication UAobj;
        resources rsc = new resources();
        cls_ValueObject cls_ValueObject;
        BmClass bM;

        public UserAuthentication( ValueObjects.cls_UserAuthentication Obj, cls_ValueObject vObj , BmClass bMObj)
        {
            this.UAobj = Obj;
            this.bM = bMObj;
            cls_ValueObject = vObj;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the UserAuthentication form.
        /// </summary>
        private void UserAuthentication_Load(object sender, EventArgs e)
        {
            checkBox_ShowPwd.Enabled = false;
            password_TextBox.PasswordChar = '*';
            foreach (userType  data in Enum.GetValues(typeof(userType)))
            {
                string description = rsc.GetEnumDescription(data);
                usertypeDropDown.Items.Add(description);
            }
        }

        /// <summary>
        /// Check user authentication using Business Logic class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_logIn_Click(object sender, EventArgs e)
        {
            UAobj.UserName = userId_TextBox.Text.ToLower();
            UAobj.UserPwd = password_TextBox.Text;
            UAobj.userType = Enum.TryParse<userType>(rsc.GetEnumValueFromDescription<userType>(usertypeDropDown.SelectedItem?.ToString()), out userType selectedType)
            ? selectedType
            : userType.guestUser;

            if (bM.UserAuthentication(UAobj))
            {
                loggedin = true;
                MessageBox.Show("Logged In");
                MainForm form = new MainForm(UAobj, cls_ValueObject, bM);
                form.ShowDialog();
                this.Close();
            }
            else
            {
                loggedin = false;
                MessageBox.Show(rsc.GetEnumDescription(UAobj.Errormessage), rsc.GetEnumDescription(UAobj.messageTitle), MessageBoxButtons.OK, MessageBoxIcon.Information); 
                password_TextBox.Clear();
            }
        }

        /// <summary>
        /// It Show or hide password based on checkbox state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_ShowPwd_CheckedChanged(object sender, EventArgs e)
        {
            password_TextBox.PasswordChar = checkBox_ShowPwd.Checked ? '\0' : '*';
        }

        /// <summary>
        /// Enable or disable show password checkbox based on whether the password field is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void password_TextBox_TextChanged(object sender, EventArgs e)
        {
            checkBox_ShowPwd.Enabled = !string.IsNullOrEmpty(password_TextBox.Text);
        }
    }
}
