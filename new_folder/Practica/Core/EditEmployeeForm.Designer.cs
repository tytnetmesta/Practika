namespace EmployeeManagementApp
{
    partial class EditEmployeeForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.comboBoxPositions = new System.Windows.Forms.ComboBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.dateTimePickerHireDate = new System.Windows.Forms.DateTimePicker();
            this.txtWorkMode = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 0;
            // 
            // comboBoxPositions
            // 
            this.comboBoxPositions.FormattingEnabled = true;
            this.comboBoxPositions.Location = new System.Drawing.Point(12, 38);
            this.comboBoxPositions.Name = "comboBoxPositions";
            this.comboBoxPositions.Size = new System.Drawing.Size(200, 21);
            this.comboBoxPositions.TabIndex = 1;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(12, 65);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(200, 20);
            this.txtDepartment.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(12, 91);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(12, 117);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 4;
            // 
            // dateTimePickerHireDate
            // 
            this.dateTimePickerHireDate.Location = new System.Drawing.Point(12, 143);
            this.dateTimePickerHireDate.Name = "dateTimePickerHireDate";
            this.dateTimePickerHireDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerHireDate.TabIndex = 5;
            // 
            // txtWorkMode
            // 
            this.txtWorkMode.Location = new System.Drawing.Point(12, 169);
            this.txtWorkMode.Name = "txtWorkMode";
            this.txtWorkMode.Size = new System.Drawing.Size(200, 20);
            this.txtWorkMode.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditEmployeeForm
            // 
            this.ClientSize = new System.Drawing.Size(224, 231);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtWorkMode);
            this.Controls.Add(this.dateTimePickerHireDate);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.comboBoxPositions);
            this.Controls.Add(this.txtName);
            this.Name = "EditEmployeeForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox comboBoxPositions;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.DateTimePicker dateTimePickerHireDate;
        private System.Windows.Forms.TextBox txtWorkMode;
        private System.Windows.Forms.Button btnSave;
    }
}
