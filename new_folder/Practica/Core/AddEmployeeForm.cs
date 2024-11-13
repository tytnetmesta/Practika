using System;
using System.Windows.Forms;

namespace EmployeeManagementApp
{
    public partial class AddEmployeeForm : Form
    {
        private Database _database;

        // Изменяем конструктор, чтобы принимать ComboBox
        public AddEmployeeForm(Database database, ComboBox comboBoxPositions)
        {
            InitializeComponent();
            _database = database;

            // Устанавливаем данные для comboBoxPositions на форме
            comboBoxPositions.DataSource = _database.GetPositions(); // Загружаем должности
            comboBoxPositions.DisplayMember = "Title"; // Отображаем название должности
            comboBoxPositions.ValueMember = "Id"; // Значение для ID должности
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var employee = new Employee
            {
                Name = txtName.Text,
                PositionId = (int)comboBoxPositions.SelectedValue, // Используем выбранную должность
                Department = txtDepartment.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                HireDate = dateTimePickerHireDate.Value,
                WorkMode = txtWorkMode.Text
            };

            _database.AddEmployee(employee);
            DialogResult = DialogResult.OK; // Возвращаем результат OK для подтверждения
            Close(); // Закрываем форму

    }
}