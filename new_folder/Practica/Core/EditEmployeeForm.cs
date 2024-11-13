using System;
using System.Windows.Forms;

namespace EmployeeManagementApp
{
    public partial class EditEmployeeForm : Form
    {
        private Database _database;
        private Employee _employee;

        // Изменяем конструктор, чтобы принимать ComboBox
        public EditEmployeeForm(Database database, Employee employee, ComboBox comboBoxPositions)
        {
            InitializeComponent();
            _database = database;
            _employee = employee;

            // Заполнение полей формы текущими данными сотрудника
            txtName.Text = _employee.Name;
            
            // Теперь comboBoxPositions инициализируется в конструкторе
            // Загрузка должностей из базы данных
            var positions = _database.GetPositions(); // Получаем должности
            comboBoxPositions.DataSource = positions; // Устанавливаем источник данных для comboBox
            comboBoxPositions.DisplayMember = "Title"; // Отображаем название должности
            comboBoxPositions.ValueMember = "Id"; // Значение для ID должности
            comboBoxPositions.SelectedValue = _employee.PositionId; // Устанавливаем выбранную должность

            txtDepartment.Text = _employee.Department;
            txtEmail.Text = _employee.Email;
            txtPhone.Text = _employee.Phone;
            dateTimePickerHireDate.Value = _employee.HireDate;
            txtWorkMode.Text = _employee.WorkMode;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _employee.Name = txtName.Text;
            _employee.PositionId = (int)comboBoxPositions.SelectedValue; // Получаем выбранное значение из comboBox
            _employee.Department = txtDepartment.Text;
            _employee.Email = txtEmail.Text;
            _employee.Phone = txtPhone.Text;
            _employee.HireDate = dateTimePickerHireDate.Value;
            _employee.WorkMode = txtWorkMode.Text;

            _database.UpdateEmployee(_employee);
            DialogResult = DialogResult.OK; // Возвращаем результат OK для подтверждения
            Close(); // Закрываем форму
        }
    }
}
