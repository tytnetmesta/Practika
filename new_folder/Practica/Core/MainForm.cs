using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EmployeeManagementApp
{
    public partial class MainForm : Form
    {
        private Database _database;
        private Button buttonSearch;

        public MainForm(Database database)
        {
            InitializeComponent();
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _database.Open(); 
            LoadPositions();
            LoadEmployees();
            LoadWorkSchedules();
        }


        private void LoadEmployees()
        {
            var employees = _database.GetEmployees(); // Получаем список сотрудников из базы данных
            dataGridViewEmployees.DataSource = employees.ToList(); // Устанавливаем источник данных для DataGridView
        }
        

        private void LoadPositions()
        {
            var positions = _database.GetPositions();

            if (positions == null || positions.Count == 0)
            {
                MessageBox.Show("Не удалось загрузить должности. Проверьте базу данных.");
                return;
            }

            // Убедитесь, что comboBoxPositions был инициализирован
            if (comboBoxPositions == null)
            {
                MessageBox.Show("Комбобокс для должностей не инициализирован.");
                return;
            }

            comboBoxPositions.DataSource = positions; 
            comboBoxPositions.DisplayMember = "Title"; 
            comboBoxPositions.ValueMember = "Id";
            
        }


        private void LoadWorkSchedules()
        {
            try
            {
                var schedules = _database.GetWorkSchedules(); // Убедитесь, что _database инициализирован
                // Обработка данных расписания
                foreach (var schedule in schedules)
                {
                    // Например, добавление в список или отображение на форме
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки расписаний: {ex.Message}");
            }
        }


        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var addEmployeeForm = new AddEmployeeForm(_database, comboBoxPositions);
            if (addEmployeeForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(); // Перезагрузить список сотрудников после добавления
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                var selectedEmployee = (Employee)dataGridViewEmployees.SelectedRows[0].DataBoundItem;

                var editEmployeeForm = new EditEmployeeForm(_database, selectedEmployee, comboBoxPositions);
                if (editEmployeeForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees(); // Перезагрузить список сотрудников после редактирования
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                var selectedEmployee = (Employee)dataGridViewEmployees.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show("Вы действительно хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    _database.DeleteEmployee(selectedEmployee.Id); // Удаление сотрудника из базы данных
                    LoadEmployees(); // Перезагрузить список сотрудников после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
            }
        }
    }
}
