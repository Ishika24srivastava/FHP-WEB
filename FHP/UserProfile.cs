using System;
using System.Windows.Forms;
using FHP_Resources;
using FHP_VO;
using FHP_BL;
using System.Collections.Generic;

namespace FHP
{
    public partial class UserProfile : Form
    {
        cls_ValueObject vObj;
        resources rsc;
        DataGridView datagridview;
        BmClass bmObject;
        ValueObjects.cls_UserAuthentication Obj;

        /// <summary>
        /// Constructor for the UserProfile form.
        /// </summary>
        /// <param name="vObj">The value object containing data.</param>
        /// <param name="fhp_datagridView">The DataGridView from the main form.</param>

        public UserProfile(cls_ValueObject vObj,DataGridView fhp_datagridView , ValueObjects.cls_UserAuthentication Obj, BmClass bMObj)
        {
            InitializeComponent();
            this.vObj = vObj;
            rsc = new resources();
            this.datagridview = fhp_datagridView;
            this.Obj= Obj;
            this.bmObject = bMObj;


            foreach (Qualification data in Enum.GetValues(typeof(Qualification)))
            {
                string description = rsc.GetEnumDescription(data);
                qualificationDropdown.Items.Add(description);
            }
            foreach (Prefix data in Enum.GetValues(typeof(Prefix)))
            {
                string description = rsc.GetEnumDescription(data);
                prefixDropDown.Items.Add(description);
            }      
        }


        /// <summary>
        /// Initializes form elements based on the UserFormType.
        /// </summary>


        private void InitializeElement()
        {
            dobDateTimePicker.Format = DateTimePickerFormat.Short;
            joiningdateTimePicker.Format = DateTimePickerFormat.Short;
            dobDateTimePicker.Value = DateTime.Now.Date;
            joiningdateTimePicker.Value = DateTime.Now.Date;

            if (vObj.UserFormType == "New")
            {
                HideViewButtons();
                ResetValues();
                editBtn.Enabled = false;
                SNotextBox.Text = vObj.serialNumber.ToString();
            }
            else if (vObj.UserFormType == "Update")
            {
                HideViewButtons();
                addBtn.Enabled = false;
                SNotextBox.Text = vObj.EmployeeDataList[vObj.updatingIndex-1].SerialNumber.ToString();
                ShowValues(vObj.updatingIndex);
            }
            else if (vObj.UserFormType == "View")
            {
                SNotextBox.Text = vObj.EmployeeDataList[vObj.updatingIndex-1].SerialNumber.ToString();
                enabledBtn(vObj.updatingIndex);
                ShowValues(vObj.updatingIndex);
                SetViewMode();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeElement();
        }

        /// <summary>
        /// Show values in form controls based on the selected index
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void ShowValues(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex <= vObj.EmployeeDataList.Count)
            {
                EmployeeData selectedData = vObj.EmployeeDataList[selectedIndex-1];
                //MessageBox.Show(selectedData.Prefix.ToString());
                prefixDropDown.SelectedItem = rsc.GetEnumDescription(selectedData.Prefix);
                FirstNameTextBox.Text = selectedData.FirstName;
                MiddleNameTextBox.Text = selectedData.MiddleName;
                LastNameTextBox.Text = selectedData.LastName;
                qualificationDropdown.SelectedItem = rsc.GetEnumDescription(selectedData.Qualification);
                joiningdateTimePicker.Value = selectedData.JoiningDate;
                CurrentCompanyTextBox.Text = selectedData.CurrentCompany;
                CurrentAddressTextBox.Text = selectedData.CurrentAddress;
                dobDateTimePicker.Value = selectedData.DateOfBirth;
            }
        }
        /// <summary>
        /// Set view mode by hiding specific buttons and disabling controls
        /// </summary>
        private void SetViewMode()
        {
            addBtn.Visible = false;
            editBtn.Visible = false;
            clearBtn.Visible = false;
            FirstBtn.Visible = true;
            lastBtn.Visible = true;
            nextBtn.Visible = true;
            previousBtn.Visible = true;
            prefixDropDown.Enabled = false;
            FirstNameTextBox.ReadOnly = true;
            MiddleNameTextBox.ReadOnly = true;
            LastNameTextBox.ReadOnly = true;
            qualificationDropdown.Enabled = false;
            joiningdateTimePicker.Enabled = false;
            CurrentCompanyTextBox.ReadOnly = true;
            CurrentAddressTextBox.ReadOnly = true;
            dobDateTimePicker.Enabled = false;
        }
        /// <summary>
        /// Hide buttons related to viewing records
        /// </summary>
        private void HideViewButtons()
        {
            FirstBtn.Visible = false;
            lastBtn.Visible = false;
            nextBtn.Visible = false;
            previousBtn.Visible = false;
        }
        /// <summary>
        /// Adds a new EmployeeData record based on form controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            EmployeeData newIndividualData = new EmployeeData();
            newIndividualData.SerialNumber = vObj.serialNumber;
            newIndividualData.Prefix = Enum.TryParse<Prefix>(rsc.GetEnumValueFromDescription<Prefix>(prefixDropDown.SelectedItem?.ToString()), out Prefix selectedPrefix)
            ? selectedPrefix
            : Prefix.Select;
            newIndividualData.FirstName = FirstNameTextBox.Text;
            newIndividualData.MiddleName = MiddleNameTextBox.Text;
            newIndividualData.LastName = LastNameTextBox.Text;
            newIndividualData.Qualification = Enum.TryParse<Qualification>(rsc.GetEnumValueFromDescription<Qualification>(qualificationDropdown.SelectedItem?.ToString()), out Qualification selectedQualification)
            ? selectedQualification
            : Qualification.Select;
            DateTime joiningDate = joiningdateTimePicker.Value;
            newIndividualData.JoiningDate = joiningDate;
            newIndividualData.CurrentCompany = CurrentCompanyTextBox.Text;
            newIndividualData.CurrentAddress = CurrentAddressTextBox.Text;
            DateTime dob = dobDateTimePicker.Value;
            newIndividualData.DateOfBirth = dob;
            vObj.EmployeeData = newIndividualData;
            vObj.EmployeeDataList.Add(newIndividualData);
            vObj.editMode = EditMode.New;

            //BmClass bmObject = new BmClass();
            if (bmObject.Save(vObj))
            {
                WriteListToDataGridView(datagridview, vObj.EmployeeDataList);
                vObj.serialNumber += 1;       //flat files
                this.Close();
            }
            else
            {
                MessageBox.Show(rsc.GetEnumDescription(vObj.validationMessages), rsc.GetEnumDescription(vObj.messageTitle), MessageBoxButtons.OK, MessageBoxIcon.Information);
                HandleValidationError(vObj.validationMessages);
                vObj.EmployeeDataList.RemoveAt(vObj.EmployeeDataList.Count - 1);
            }
        }
        /// <summary>
        /// Edits an existing EmployeeData record based on form controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            EmployeeData newIndividualData = vObj.EmployeeDataList[(vObj.updatingIndex) - 1];
            bool cantBeDowngradedTrue = false;

            newIndividualData.Prefix = Enum.TryParse<Prefix>(rsc.GetEnumValueFromDescription<Prefix>(prefixDropDown.SelectedItem?.ToString()), out Prefix selectedPrefix)
            ? selectedPrefix
            : Prefix.Select;
            newIndividualData.FirstName = FirstNameTextBox.Text;
            newIndividualData.MiddleName = MiddleNameTextBox.Text;
            newIndividualData.LastName = LastNameTextBox.Text;
            //isdowngradingggggg
            if (Obj.userType == userType.self)
            {
                if (Enum.TryParse(newIndividualData.Qualification.ToString(), out Qualification qualificationFromRow) &&
                Enum.TryParse(rsc.GetEnumValueFromDescription<Qualification>(qualificationDropdown.SelectedItem?.ToString()), out Qualification selectedQual))
                {
                    if (rsc.isDowngrading(qualificationFromRow, selectedQual))
                    {
                        cantBeDowngradedTrue = true;
                        vObj.validationMessages = ValidationMessages.QualificationDowngraded;
                    }
                    else
                    {
                        newIndividualData.Qualification = Enum.TryParse<Qualification>(rsc.GetEnumValueFromDescription<Qualification>(qualificationDropdown.SelectedItem?.ToString()), out Qualification selectedQualification)
                        ? selectedQualification
                        : Qualification.Select;
                    }
                }
            }
            else
            {
                newIndividualData.Qualification = Enum.TryParse<Qualification>(rsc.GetEnumValueFromDescription<Qualification>(qualificationDropdown.SelectedItem?.ToString()), out Qualification selectedQualification)
                ? selectedQualification
                : Qualification.Select;
            }
            DateTime joiningDate = joiningdateTimePicker.Value;
            newIndividualData.JoiningDate = joiningDate;
            newIndividualData.CurrentCompany = CurrentCompanyTextBox.Text;
            newIndividualData.CurrentAddress = CurrentAddressTextBox.Text;
            DateTime dob = dobDateTimePicker.Value;
            newIndividualData.DateOfBirth = dob;
            vObj.EmployeeData = newIndividualData;
            vObj.editMode = EditMode.Edit;
            vObj.isDeleted = false;
            //BmClass bmObject = new BmClass();

            if (cantBeDowngradedTrue == false && bmObject.Save(vObj))
            {
                WriteListToDataGridView(datagridview, vObj.EmployeeDataList);
                vObj.serialNumber += 1;
                this.Close();
            }
            else
            {
                MessageBox.Show(rsc.GetEnumDescription(vObj.validationMessages), rsc.GetEnumDescription(vObj.messageTitle), MessageBoxButtons.OK, MessageBoxIcon.Information);
                HandleValidationError(vObj.validationMessages);
            }
        }
        /// <summary>
        /// Handle validation errors by setting focus to the relevant contr
        /// </summary>
        /// <param name="validationError"></param>
        public void HandleValidationError(ValidationMessages validationError)
        {
            switch (validationError)
            {
                case ValidationMessages.FirstNameRequired:
                    SetFocusToControl(FirstNameTextBox);
                    break;
                case ValidationMessages.FirstNameInvalidCharacters:
                    SetFocusToControl(FirstNameTextBox);
                    break;
                case ValidationMessages.LastNameInvalidCharacters:
                    SetFocusToControl(LastNameTextBox);
                    break;
                case ValidationMessages.MiddleNameInvalidCharacters:
                    SetFocusToControl(MiddleNameTextBox);
                    break;
                case ValidationMessages.QualificationRequired:
                    SetFocusToControl(qualificationDropdown);
                    break;
                case ValidationMessages.QualificationDowngraded:
                    SetFocusToControl(qualificationDropdown);
                    break;
                case ValidationMessages.DateOfBirthInvalid:
                    SetFocusToControl(dobDateTimePicker);
                    break;
                case ValidationMessages.DateOfBirthIsMore:
                    SetFocusToControl(dobDateTimePicker);
                    break;
                case ValidationMessages.JoiningDateInvalid:
                    SetFocusToControl(FirstNameTextBox);
                    break;
                case ValidationMessages.CurrentCompanyRequired:
                    SetFocusToControl(CurrentCompanyTextBox);
                    break;

                default:

                    break;
            }
        }
        /// <summary>
        /// Sets focus to a specific control.
        /// </summary>
        /// <param name="control">The control to set focus to.</param>
        public void SetFocusToControl(Control control)
        {
            if (control is ComboBox comboBox)
            {
                comboBox.DroppedDown = true;
            }
            control.Focus();
        }

        /// <summary>
        /// // Reset values, set updatingIndex to 1, show values, and enable/disable navigation buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstBtn_Click(object sender, EventArgs e)
        {
            ResetValues();
            vObj.updatingIndex = 1;
            SNotextBox.Text = vObj.EmployeeDataList[0].SerialNumber.ToString();
            ShowValues(1);
            enabledBtn(1);
        }
        /// <summary>
       /// Decrement updatingIndex, show values, update serial number textbox, and enable/disable navigation buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousBtn_Click(object sender, EventArgs e)
        {
            vObj.updatingIndex -= 1;
            ShowValues(vObj.updatingIndex);
            SNotextBox.Text = vObj.EmployeeDataList[vObj.updatingIndex - 1].SerialNumber.ToString();
            enabledBtn(vObj.updatingIndex);
        }
        /// <summary>
        ///  Increment updatingIndex, show values, update serial number textbox, and enable/disable navigation buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextBtn_Click(object sender, EventArgs e)
        {
            vObj.updatingIndex += 1;
            ShowValues(vObj.updatingIndex);
            SNotextBox.Text = vObj.EmployeeDataList[vObj.updatingIndex - 1].SerialNumber.ToString();
            enabledBtn(vObj.updatingIndex);
        }
        /// <summary>
        /// Enable or disable "Previous" and "Next" buttons based on the selected index
        /// </summary>
        /// <param name="selectedIndexInDatatable"></param>
        private void enabledBtn(long selectedIndexInDatatable)
        {
            if (selectedIndexInDatatable <= 1)
            {
                previousBtn.Enabled = false;
            }
            else
            {
                previousBtn.Enabled = true;
            }
            if (selectedIndexInDatatable >= vObj.EmployeeDataList.Count)
            {
                nextBtn.Enabled = false;
            }
            else
            {
                nextBtn.Enabled = true;
            }
        }
        /// <summary>
        /// Reset values, set updatingIndex to the last index, show values, and enable/disable navigation buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastBtn_Click(object sender, EventArgs e)
        {
            ResetValues();
            vObj.updatingIndex = vObj.EmployeeDataList.Count;
            SNotextBox.Text = vObj.EmployeeDataList[vObj.updatingIndex - 1].SerialNumber.ToString();
            ShowValues(vObj.updatingIndex);
            enabledBtn(vObj.updatingIndex);
        }
        /// <summary>
        /// Confirm clearing, reset values, set serial number textbox, and enable/disable buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearBtn_Click(object sender, EventArgs e)
        {
            DialogResult wantToClear = MessageBox.Show(rsc.GetEnumDescription(Messages.ClearForm), rsc.GetEnumDescription(MessageCaptions.ClearForm), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (wantToClear == DialogResult.OK)
            {
                ResetValues();
                SNotextBox.Text = vObj.serialNumber.ToString();
                editBtn.Enabled = false;
                addBtn.Enabled = true;
            }
        }
        /// <summary>
        /// Clear values of textboxes, rich textboxes, date pickers, and combo boxes
        /// </summary>
        private void ResetValues()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Clear();
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Checked = false;
                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                    dateTimePicker.CustomFormat = " ";
                }
                else if (control is System.Windows.Forms.ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
            }
        }
        /// <summary>
        /// Shows all elements (buttons, textboxes, etc.) on the form.
        /// </summary>
        private void ShowAllElements()
        {
            addBtn.Visible = true;
            editBtn.Visible = true;
            clearBtn.Visible = true;
            FirstBtn.Visible = true;
            lastBtn.Visible = true;
            nextBtn.Visible = true;
            previousBtn.Visible = true;
            prefixDropDown.Enabled = true;
            FirstNameTextBox.ReadOnly = false;
            MiddleNameTextBox.ReadOnly = false;
            LastNameTextBox.ReadOnly = false;
            qualificationDropdown.Enabled = true;
            joiningdateTimePicker.Enabled = true;
            CurrentCompanyTextBox.ReadOnly = false;
            CurrentAddressTextBox.ReadOnly = false;
            dobDateTimePicker.Enabled = true;
        }
        /// <summary>
        /// Show all the controls and reset values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowAllElements();
            ResetValues();
        }
        /// <summary>
        /// handle joiningdateTimePicker_ValueChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void joiningdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            joiningdateTimePicker.Format = DateTimePickerFormat.Short;
            joiningdateTimePicker.CustomFormat = null;
        }
        /// <summary>
        /// handle dobDateTimePicker_ValueChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            dobDateTimePicker.Format = DateTimePickerFormat.Short;
            dobDateTimePicker.CustomFormat = null;
        }
        /// <summary>
        /// Writes a list of EmployeeData to the DataGridView.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to write to.</param>
        /// <param name="individualDataList">The list of EmployeeData to be written.</param>

        private void WriteListToDataGridView(DataGridView dataGridView, List<EmployeeData> individualDataList)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Add columns with specific names
            dataGridView.Columns.Add("SerialNumber", "Serial Number");
            dataGridView.Columns.Add("Prefix", "Prefix");
            dataGridView.Columns.Add("FirstName", "First Name");
            dataGridView.Columns.Add("MiddleName", "Middle Name");
            dataGridView.Columns.Add("LastName", "Last Name");
            dataGridView.Columns.Add("DateOfBirth", "Date of Birth");
            dataGridView.Columns.Add("CurrentAddress", "Current Address");
            dataGridView.Columns.Add("Qualification", "Qualification");
            dataGridView.Columns.Add("JoiningDate", "Joining Date");
            dataGridView.Columns.Add("CurrentCompany", "Current Company");

            dataGridView.Rows.Add();
            // Iterate through the list and add rows to the DataGridView
            foreach (var individualData in individualDataList)
            {
                int rowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowIndex];

                // Assign values to each cell in the row
                row.Cells["SerialNumber"].Value = individualData.SerialNumber;
                row.Cells["Prefix"].Value = rsc.GetEnumDescription(individualData.Prefix);
                if (row.Cells["Prefix"].Value.ToString() == "Select")
                {
                    row.Cells["Prefix"].Value = "";
                }
                row.Cells["FirstName"].Value = individualData.FirstName;
                row.Cells["MiddleName"].Value = individualData.MiddleName;
                row.Cells["LastName"].Value = individualData.LastName;
                if (individualData.DateOfBirth.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    row.Cells["DateOfBirth"].Value = "";
                }
                else
                {
                    row.Cells["DateOfBirth"].Value = individualData.DateOfBirth.ToShortDateString();
                }               
                row.Cells["CurrentAddress"].Value = individualData.CurrentAddress;
                row.Cells["Qualification"].Value = rsc.GetEnumDescription(individualData.Qualification); 
                if (row.Cells["Qualification"].Value.ToString() == "Select")
                {
                    row.Cells["Qualification"].Value = "";
                }
                row.Cells["JoiningDate"].Value = individualData.JoiningDate.ToShortDateString();
                row.Cells["CurrentCompany"].Value = individualData.CurrentCompany;
            }
        }
    }
}
