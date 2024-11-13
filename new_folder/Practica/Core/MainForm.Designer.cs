namespace EmployeeManagementApp
{
    partial class MainForm
    {
        
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxPositions;
        private System.Windows.Forms.DataGridView dataGridViewSchedules;

        /// <summary>
        /// Освободите все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">true, если освобождаются управляемые ресурсы; иначе — false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный Windows Form Designer

        /// <summary>
        /// Необходимая переменная конструктора.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnEditEmployee = new System.Windows.Forms.Button();
            this.btnDeleteEmployee = new System.Windows.Forms.Button();
            this.comboBoxPositions = new System.Windows.Forms.ComboBox(); // Добавляем ComboBox

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.SuspendLayout();

            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.Size = new System.Drawing.Size(776, 396);
            this.dataGridViewEmployees.TabIndex = 0;

            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(12, 414);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnAddEmployee.TabIndex = 1;
            this.btnAddEmployee.Text = "Добавить";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);

            // 
            // btnEditEmployee
            // 
            this.btnEditEmployee.Location = new System.Drawing.Point(93, 414);
            this.btnEditEmployee.Name = "btnEditEmployee";
            this.btnEditEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnEditEmployee.TabIndex = 2;
            this.btnEditEmployee.Text = "Редактировать";
            this.btnEditEmployee.UseVisualStyleBackColor = true;
            this.btnEditEmployee.Click += new System.EventHandler(this.btnEditEmployee_Click);

            // 
            // btnDeleteEmployee
            // 
            this.btnDeleteEmployee.Location = new System.Drawing.Point(174, 414);
            this.btnDeleteEmployee.Name = "btnDeleteEmployee";
            this.btnDeleteEmployee.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEmployee.TabIndex = 3;
            this.btnDeleteEmployee.Text = "Удалить";
            this.btnDeleteEmployee.UseVisualStyleBackColor = true;
            this.btnDeleteEmployee.Click += new System.EventHandler(this.btnDeleteEmployee_Click);

            // 
            // comboBoxPositions
            // 
            this.comboBoxPositions.FormattingEnabled = true; // Включаем возможность выбора
            this.comboBoxPositions.Location = new System.Drawing.Point(12, 450); // Установите нужные координаты
            this.comboBoxPositions.Name = "comboBoxPositions"; // Имя комбобокса
            this.comboBoxPositions.Size = new System.Drawing.Size(121, 21); // Установите нужный размер
            this.comboBoxPositions.TabIndex = 4; // Укажите порядок табуляции

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDeleteEmployee);
            this.Controls.Add(this.btnEditEmployee);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Controls.Add(this.comboBoxPositions); // Добавляем comboBox на форму
            this.Name = "MainForm";
            this.Text = "Управление сотрудниками";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.ResumeLayout(false);
            
            
        }

        
        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnEditEmployee;
        private System.Windows.Forms.Button btnDeleteEmployee;
    }
}
