using System;
using System.Windows.Forms;

namespace EmployeeManagementApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Инициализация базы данных
            string connectionString = "Data Source=employees.db;Version=3;";
            Database database = new Database(connectionString);
            database.Open();
            database.InitializeDatabase();
            database.Close();

            // Запуск главной формы
            Application.Run(new MainForm(database));
        }
    }
}
