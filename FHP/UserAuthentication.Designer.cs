namespace FHP
{
    partial class UserAuthentication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAuthentication));
            this.btn_logIn = new System.Windows.Forms.Button();
            this.btn_userId = new System.Windows.Forms.Label();
            this.userPassword = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_ShowPwd = new System.Windows.Forms.CheckBox();
            this.password_TextBox = new System.Windows.Forms.TextBox();
            this.userId_TextBox = new System.Windows.Forms.TextBox();
            this.usertypeDropDown = new System.Windows.Forms.ComboBox();
            this.userType_Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_logIn
            // 
            this.btn_logIn.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_logIn.Location = new System.Drawing.Point(328, 400);
            this.btn_logIn.Name = "btn_logIn";
            this.btn_logIn.Size = new System.Drawing.Size(162, 38);
            this.btn_logIn.TabIndex = 0;
            this.btn_logIn.Text = "Log In";
            this.btn_logIn.UseVisualStyleBackColor = true;
            this.btn_logIn.Click += new System.EventHandler(this.btn_logIn_Click);
            // 
            // btn_userId
            // 
            this.btn_userId.AutoSize = true;
            this.btn_userId.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_userId.Location = new System.Drawing.Point(129, 226);
            this.btn_userId.Name = "btn_userId";
            this.btn_userId.Size = new System.Drawing.Size(75, 21);
            this.btn_userId.TabIndex = 3;
            this.btn_userId.Text = "User Id ";
            // 
            // userPassword
            // 
            this.userPassword.AutoSize = true;
            this.userPassword.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPassword.Location = new System.Drawing.Point(117, 288);
            this.userPassword.Name = "userPassword";
            this.userPassword.Size = new System.Drawing.Size(87, 21);
            this.userPassword.TabIndex = 4;
            this.userPassword.Text = "Password";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FHP.Properties.Resources.profile_user_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(363, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox_ShowPwd
            // 
            this.checkBox_ShowPwd.AutoSize = true;
            this.checkBox_ShowPwd.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ShowPwd.Location = new System.Drawing.Point(248, 338);
            this.checkBox_ShowPwd.Name = "checkBox_ShowPwd";
            this.checkBox_ShowPwd.Size = new System.Drawing.Size(159, 25);
            this.checkBox_ShowPwd.TabIndex = 6;
            this.checkBox_ShowPwd.Text = "Show Password";
            this.checkBox_ShowPwd.UseVisualStyleBackColor = true;
            this.checkBox_ShowPwd.CheckedChanged += new System.EventHandler(this.checkBox_ShowPwd_CheckedChanged);
            // 
            // password_TextBox
            // 
            this.password_TextBox.Location = new System.Drawing.Point(248, 274);
            this.password_TextBox.Multiline = true;
            this.password_TextBox.Name = "password_TextBox";
            this.password_TextBox.Size = new System.Drawing.Size(365, 35);
            this.password_TextBox.TabIndex = 7;
            this.password_TextBox.TextChanged += new System.EventHandler(this.password_TextBox_TextChanged);
            // 
            // userId_TextBox
            // 
            this.userId_TextBox.Location = new System.Drawing.Point(248, 213);
            this.userId_TextBox.Multiline = true;
            this.userId_TextBox.Name = "userId_TextBox";
            this.userId_TextBox.Size = new System.Drawing.Size(365, 34);
            this.userId_TextBox.TabIndex = 8;
            // 
            // usertypeDropDown
            // 
            this.usertypeDropDown.FormattingEnabled = true;
            this.usertypeDropDown.Location = new System.Drawing.Point(248, 151);
            this.usertypeDropDown.Name = "usertypeDropDown";
            this.usertypeDropDown.Size = new System.Drawing.Size(365, 24);
            this.usertypeDropDown.TabIndex = 9;
            // 
            // userType_Label
            // 
            this.userType_Label.AutoSize = true;
            this.userType_Label.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userType_Label.Location = new System.Drawing.Point(110, 154);
            this.userType_Label.Name = "userType_Label";
            this.userType_Label.Size = new System.Drawing.Size(94, 21);
            this.userType_Label.TabIndex = 10;
            this.userType_Label.Text = "User Type";
            // 
            // UserAuthentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.userType_Label);
            this.Controls.Add(this.usertypeDropDown);
            this.Controls.Add(this.userId_TextBox);
            this.Controls.Add(this.password_TextBox);
            this.Controls.Add(this.checkBox_ShowPwd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.userPassword);
            this.Controls.Add(this.btn_userId);
            this.Controls.Add(this.btn_logIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserAuthentication";
            this.Text = "User Authentication Form";
            this.Load += new System.EventHandler(this.UserAuthentication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_logIn;
        private System.Windows.Forms.Label btn_userId;
        private System.Windows.Forms.Label userPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox_ShowPwd;
        private System.Windows.Forms.TextBox password_TextBox;
        private System.Windows.Forms.TextBox userId_TextBox;
        private System.Windows.Forms.ComboBox usertypeDropDown;
        private System.Windows.Forms.Label userType_Label;
    }
}