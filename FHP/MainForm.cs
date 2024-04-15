using System;
using System.Windows.Forms;
using FHP_VO;
using FHP_BL;
using FHP_Resources;
using System.Drawing;
using System.Collections.Generic;

namespace FHP
{
    public partial class MainForm : Form
    {
        cls_ValueObject vObj;
        ValueObjects.cls_UserAuthentication UAobj;
        UserProfile form2;
        resources rsc;
        BmClass bmObject { get; set; }

        /// <summary>
        /// Show user authentication form and close the application if not logged in 
        /// </summary>
        public MainForm(ValueObjects.cls_UserAuthentication Obj, cls_ValueObject vObj, BmClass bMObj)
        {
            this.UAobj = Obj;
            this.vObj = vObj;
            this.bmObject = bMObj;
            InitializeComponent();
        }
       
        /// <summary>
        /// Initialize various objects and components and Set the form title based on the user type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            rsc = new resources();
            //bmObject = new BmClass();
            bmObject.start(vObj);
            int last = vObj.EmployeeDataList.Count - 1;
            if (last >= 0)
            {
                long sNo = (long)vObj.EmployeeDataList[last].SerialNumber;
                vObj.serialNumber = (int)(sNo + 1);
            }
            else
            {
                vObj.serialNumber = 1;
            }
            WriteListToDataGridView(FhpGridView, vObj.EmployeeDataList);
            dELETEToolStripMenuItem.Enabled = false;
            uPDATEToolStripMenuItem.Enabled = false;
            vIEWToolStripMenuItem.Enabled = false;
            rEFRESHToolStripMenuItem.Enabled = false;

            if (UAobj.userType == userType.guestUser)
            {
                nEWToolStripMenuItem.Enabled = false;
                this.Text += " - GuestUser";
            }
            if (UAobj.userType == userType.self)
            {
                this.Text += " - Self (Jashan)";
            }
            if (UAobj.userType == userType.admin)
            {
                this.Text += " - Admin";
            }
            else if (UAobj.userType == userType.superAdmin)
            {
                this.Text += "- SuperAdmin";
            }

        }

        /// <summary>
        /// Set the user form type to "New"
        /// Create a new instance of the UserProfile form'
        /// Set the form title based on the enumeration description
        /// Show the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vObj.UserFormType = "New";
            form2 = new UserProfile(vObj,FhpGridView, UAobj, bmObject);
            form2.Text = rsc.GetEnumDescription(Title.AddEditForm);
            form2.ShowDialog();
        }

        /// <summary>
        /// Set the user form type to "Update"
        /// set current index value to the updating index
        /// Create a new instance of the UserProfile form'
        /// Set the form title based on the enumeration description
        /// Show the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = FhpGridView.SelectedRows[0].Index;
            vObj.updatingIndex = selectedRowIndex;
            vObj.UserFormType = "Update";
            form2 = new UserProfile(vObj, FhpGridView, UAobj, bmObject);
            form2.Text = rsc.GetEnumDescription(Title.AddEditForm);
            form2.Show();
        }
        /// <summary>
        /// Set the user form type to "View"
        /// Create a new instance of the UserProfile form'
        /// Set the form title based on the enumeration description
        /// Show the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = FhpGridView.SelectedRows[0].Index;
            vObj.updatingIndex = selectedRowIndex;
            vObj.UserFormType = "View";
            form2 = new UserProfile(vObj, FhpGridView, UAobj, bmObject);
            if (UAobj.userType == userType.guestUser)
            {
                form2.Text = rsc.GetEnumDescription(Title.ViewOnlyForm);
            }
            else
            {
                form2.Text = rsc.GetEnumDescription(Title.ViewOnlyForm);
            }
            form2.Show();
        }

        /// <summary>
        /// used to delete that selected row
        /// 
        /// Set the form title based on the enumeration description
        /// Show the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vObj.EmployeeDataList != null && vObj.EmployeeDataList.Count >= 1)
            {
                DialogResult wantToDelete = MessageBox.Show(rsc.GetEnumDescription(Messages.DeleteRecord), rsc.GetEnumDescription(MessageCaptions.DeleteRecord), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (wantToDelete == DialogResult.Yes)
                {
                    vObj.editMode = EditMode.Edit;
                    vObj.isDeleted = true;
                    int selectedRowIndex = FhpGridView.SelectedRows[0].Index;
                    if (selectedRowIndex > 0 && selectedRowIndex <= vObj.EmployeeDataList.Count)
                    {
                        vObj.deletingRow = selectedRowIndex-1;
                        if (bmObject.Save(vObj))
                        { 
                            
                            WriteListToDataGridView(FhpGridView, vObj.EmployeeDataList);
                        }
                        else
                        {
                            MessageBox.Show("Record not deleted: " + vObj.ErrorMessages);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Handles the selection change event in the FhpGridView
        ///Check user type and enable/disable menu items accordingly
        ///Update menu item enabled state based on selected row
        ///Handle different cases when rows are selected or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (UAobj.userType == userType.guestUser)
            {
                nEWToolStripMenuItem.Enabled = false;
            }
            if (FhpGridView.SelectedRows.Count > 0)
            {
                int selectedRowIndex = FhpGridView.SelectedRows[0].Index;
                if (selectedRowIndex > 0 && selectedRowIndex <= vObj.EmployeeDataList.Count)
                {
                    string valueInThirdColumn = vObj.EmployeeDataList[selectedRowIndex-1].FirstName;
                    if (UAobj.userType == userType.guestUser)
                    {
                        vIEWToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        dELETEToolStripMenuItem.Enabled = !string.IsNullOrEmpty(valueInThirdColumn);
                        uPDATEToolStripMenuItem.Enabled = !string.IsNullOrEmpty(valueInThirdColumn);
                        vIEWToolStripMenuItem.Enabled = !string.IsNullOrEmpty(valueInThirdColumn);
                        nEWToolStripMenuItem.Enabled = string.IsNullOrEmpty(valueInThirdColumn);
                    }
                }
            }
            else
            {
                dELETEToolStripMenuItem.Enabled = false;
                uPDATEToolStripMenuItem.Enabled = false;
                vIEWToolStripMenuItem.Enabled = false;
                if (UAobj.userType == userType.guestUser)
                {
                    nEWToolStripMenuItem.Enabled = false;
                }
                else
                {
                    nEWToolStripMenuItem.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Handles the double-click event on a cell in the FhpGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = FhpGridView.SelectedCells[0].RowIndex;
            vObj.updatingIndex = selectedRowIndex;
            if (vObj.updatingIndex < vObj.EmployeeDataList.Count && vObj.updatingIndex != 0)
            {
                vObj.UserFormType = "View";
                form2 = new UserProfile(vObj, FhpGridView, UAobj, bmObject);
                form2.Text = rsc.GetEnumDescription(Title.ViewOnlyForm);
                form2.Show();
            }

        }
        /// <summary>
        /// Adjusts the docking style of FhpGridView based on the form's state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                FhpGridView.Dock = DockStyle.Fill;
            }
            else
            {
                FhpGridView.Dock = DockStyle.None;
            }
        }
        /// <summary>
        /// Dispose objects and perform cleanup when the form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            vObj = null;
            form2?.Dispose();
            rsc = null;
            bmObject = null;
        }

        /// <summary>
        /// Handles cell mouse click event and updates cell read-only status if cell index =0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex != -1)
            {
                FhpGridView.ReadOnly = false;
                FhpGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
            }
            else
            {
                foreach (DataGridViewCell cell in FhpGridView.Rows[0].Cells)
                {
                    cell.ReadOnly = true;
                }
            }
        }
        /// <summary>
        /// Handles cell value change event and filters rows based on cell content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {

                string filterText = FhpGridView.Rows[0].Cells[e.ColumnIndex].Value?.ToString()?.ToLower()?.Trim() ?? "";
                if (filterText.Length >= 3)
                {
                    foreach (DataGridViewRow row in FhpGridView.Rows)
                    {
                        if (!row.IsNewRow && row.Visible)
                        {
                            bool rowVisible = true;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.ColumnIndex == e.ColumnIndex)
                                {
                                    rowVisible &= cell.Value != null && cell.Value.ToString().ToLower().Contains(filterText);
                                }
                            }
                            row.Visible = rowVisible;
                        }
                    }
                    rEFRESHToolStripMenuItem.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Refreshes the FhpGridView and resets search-related components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteListToDataGridView(FhpGridView, vObj.EmployeeDataList);
            SearchTextBox.Text = "";
            rEFRESHToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Displays the AboutUs form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aBOUTUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.ShowDialog();
        }

        /// <summary>
        /// Performs search and updates UI based on the entered text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string dataToBeSearched = SearchTextBox.Text;
            if (dataToBeSearched.Length >= 3)
            {
                SearchRows(SearchTextBox.Text);
                rEFRESHToolStripMenuItem.Enabled = true;
            }
        }

        private void SearchRows(string data)
        {
            if (data.ToString().Length > 0)
            {
                if (data.ToString().Length == 1)
                {
                    foreach (DataGridViewCell cell in FhpGridView.Rows[0].Cells)
                    {
                        cell.Value = "";
                    }
                }
                for (int index = 1; index < FhpGridView.Rows.Count - 1; index++)
                {
                    DataGridViewRow selectedRow = FhpGridView.Rows[index];
                    bool rowVisible = false;
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        if ((cell.Value?.ToString().ToUpper()).Contains(data.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    if (!rowVisible)
                    {
                        selectedRow.Visible = false;
                    }
                    else
                    {
                        selectedRow.Visible = true;
                    }
                }
            }
        }

        private void FhpGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (FhpGridView.IsCurrentCellDirty)
            {
                FhpGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        /// <summary>
        /// Customizes cell painting to display filter icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex >= 0 && e.Value != null)
            {
                string filterText = FhpGridView.Rows[0].Cells[e.ColumnIndex].Value?.ToString()?.ToLower()?.Trim() ?? "";

                if (filterText != "" && e.Value.ToString().ToLower().Contains(filterText))
                {
                    int iconWidth = 16;
                    int iconHeight = 16;
                    int iconPadding = 2;
                    int cellPadding = 5;

                    int iconX = e.CellBounds.Right - iconWidth - iconPadding;
                    int iconY = e.CellBounds.Top + (e.CellBounds.Height - iconHeight) / 2;

                    e.PaintBackground(e.CellBounds, true);
                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.Left + cellPadding, e.CellBounds.Top + 2);

                    Icon crossButtonIcon = Properties.Resources.clearFilterIcon;
                    Image crossButtonImage = crossButtonIcon.ToBitmap();
                    e.Graphics.DrawImage(crossButtonImage, new Rectangle(iconX, iconY, iconWidth, iconHeight));
                    e.Handled = true;

                    FhpGridView.Rows[0].Cells[e.ColumnIndex].Tag = "CrossButton";

                }
            }
        }

        /// <summary>
        /// Handles cell click event and performs actions based on the cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FhpGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex >= 0 && FhpGridView.Rows[0].Cells[e.ColumnIndex].Tag?.ToString() == "CrossButton")
            {
                WriteListToDataGridView(FhpGridView, vObj.EmployeeDataList);
                SearchTextBox.Text = "";
                rEFRESHToolStripMenuItem.Enabled = false;
            }
        }
        /// <summary>
        /// Clears and populates the FhpGridView with data from the list
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="individualDataList"></param>
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
