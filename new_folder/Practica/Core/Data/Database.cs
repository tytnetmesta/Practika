using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace EmployeeManagementApp
{
    public class Database
    {
        private SQLiteConnection _connection;

        public Database(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            InitializeDatabase(); // Вызов метода для инициализации базы данных
        }

        private bool TableExists(string tableName)
        {
            string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
            using (var command = new SQLiteCommand(query, _connection))
            {
                var result = command.ExecuteScalar();
                return result != null;
            }
        }

        public void InitializeDatabase()
        {
            try
            {
                Open(); // Убедитесь, что соединение открыто

                // Удалите старую таблицу work_schedule, если она существует
                ExecuteNonQuery("DROP TABLE IF EXISTS work_schedule;");

                // Создаем таблицы, если они не существуют
                string createPositionsTableQuery = @"
        CREATE TABLE IF NOT EXISTS positions (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            salary REAL
        );";

                string createEmployeesTableQuery = @"
        CREATE TABLE IF NOT EXISTS employees (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            position_id INTEGER,
            department TEXT,
            email TEXT,
            phone TEXT,
            hire_date DATE,
            work_mode TEXT NOT NULL,
            FOREIGN KEY (position_id) REFERENCES positions(id)
        );";

                string createWorkScheduleTableQuery = @"
        CREATE TABLE IF NOT EXISTS work_schedule (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            employee_id INTEGER,
            date DATE,
            start_time TIME,
            end_time TIME,
            hours_worked REAL,
            shift TEXT,
            arrival_time TIME,
            departure_time TIME,
            days_off TEXT,
            FOREIGN KEY (employee_id) REFERENCES employees(id)
        );";

                ExecuteNonQuery(createPositionsTableQuery);
                ExecuteNonQuery(createEmployeesTableQuery);
                ExecuteNonQuery(createWorkScheduleTableQuery); // Добавьте эту строку

                // Заполнение позиций
                SeedDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации базы данных: {ex.Message}");
            }
            finally
            {
                Close(); // Всегда закрывайте соединение
            }
        }


        public void SeedDatabase()
        {
            // Заполнение позиций
            if (!TableExists("positions"))
            {
                ExecuteNonQuery("INSERT INTO positions (title, salary) VALUES ('Менеджер', 60000);");
                ExecuteNonQuery("INSERT INTO positions (title, salary) VALUES ('Разработчик', 80000);");
                ExecuteNonQuery("INSERT INTO positions (title, salary) VALUES ('Дизайнер', 50000);");
            }

            // Заполнение сотрудников
           
            AddEmployee(new Employee
            {
                Name = "Иван Иванов",
                PositionId = 1, // Менеджер
                Department = "IT",
                Email = "ivan@example.com",
                Phone = "1234567890",
                HireDate = DateTime.Parse("2024-01-01"),
                WorkMode = "Полный рабочий день"
            });

            AddEmployee(new Employee
            {
                Name = "Петр Петров",
                PositionId = 2, // Разработчик
                Department = "IT",
                Email = "petr@example.com",
                Phone = "0987654321",
                HireDate = DateTime.Parse("2024-02-01"),
                WorkMode = "Частичная занятость"
            });

            AddEmployee(new Employee
            {
                Name = "Вадим Пуре",
                PositionId = 2, // Разработчик
                Department = "IT",
                Email = "vadim.pure@example.com",
                Phone = "4567891230",
                HireDate = DateTime.Parse("2024-03-01"),
                WorkMode = "Частичная занятость"
            });

            // Добавим ещё нескольких сотрудников
            AddEmployee(new Employee
            {
                Name = "Анна Смирнова",
                PositionId = 3, // Дизайнер
                Department = "Дизайн",
                Email = "anna.smirnova@example.com",
                Phone = "3216549870",
                HireDate = DateTime.Parse("2024-04-15"),
                WorkMode = "Полный рабочий день"
            });

            AddEmployee(new Employee
            {
                Name = "Дмитрий Орлов",
                PositionId = 1, // Менеджер
                Department = "Маркетинг",
                Email = "dmitry.orlov@example.com",
                Phone = "7894561230",
                HireDate = DateTime.Parse("2024-05-10"),
                WorkMode = "Удаленная работа"
            });

            AddEmployee(new Employee
            {
                Name = "Ольга Кузнецова",
                PositionId = 3, // Дизайнер
                Department = "Дизайн",
                Email = "olga.kuznetsova@example.com",
                Phone = "1472583690",
                HireDate = DateTime.Parse("2024-06-20"),
                WorkMode = "Частичная занятость"
            });

            AddEmployee(new Employee
            {
                Name = "Алексей Сидоров",
                PositionId = 2, // Разработчик
                Department = "IT",
                Email = "aleksey.sidorov@example.com",
                Phone = "2583691470",
                HireDate = DateTime.Parse("2024-07-05"),
                WorkMode = "Полный рабочий день"
            });

            AddEmployee(new Employee
            {
                Name = "Екатерина Петрова",
                PositionId = 1, // Менеджер
                Department = "Продажи",
                Email = "ekaterina.petrova@example.com",
                Phone = "3692581470",
                HireDate = DateTime.Parse("2024-08-25"),
                WorkMode = "Частичная занятость"
            });

            // Создание примера расписания для сотрудников
            var employees = GetEmployees();
            foreach (var employee in employees)
            {
                CreateSampleWorkSchedule(employee.Id);
            }
        }

        public List<Employee> SearchEmployees(string name = null, int? positionId = null, string department = null, string workMode = null)
        {
            var employees = new List<Employee>();
            
            // Начинаем формировать базовый запрос
            string query = "SELECT * FROM employees WHERE 1=1"; // 1=1 - это условие, чтобы упростить добавление дополнительных условий

            // Добавляем условия поиска, если они указаны
            if (!string.IsNullOrEmpty(name))
            {
                query += " AND name LIKE @name";
            }
            if (positionId.HasValue)
            {
                query += " AND position_id = @positionId";
            }
            if (!string.IsNullOrEmpty(department))
            {
                query += " AND department = @department";
            }
            if (!string.IsNullOrEmpty(workMode))
            {
                query += " AND work_mode = @workMode";
            }

            using (var command = new SQLiteCommand(query, _connection))
            {
                // Добавляем параметры в запрос
                if (!string.IsNullOrEmpty(name))
                {
                    command.Parameters.AddWithValue("@name", "%" + name + "%"); // Используем LIKE для поиска по частичному совпадению
                }
                if (positionId.HasValue)
                {
                    command.Parameters.AddWithValue("@positionId", positionId.Value);
                }
                if (!string.IsNullOrEmpty(department))
                {
                    command.Parameters.AddWithValue("@department", department);
                }
                if (!string.IsNullOrEmpty(workMode))
                {
                    command.Parameters.AddWithValue("@workMode", workMode);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PositionId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            Department = reader.GetString(3),
                            Email = reader.GetString(4),
                            Phone = reader.GetString(5),
                            HireDate = reader.GetDateTime(6),
                            WorkMode = reader.GetString(7)
                        });
                    }
                }
            }
            
            return employees;
        }
        
        
        public void CreateSampleWorkSchedule(int employeeId)
        {
            var schedule = new WorkSchedule
            {
                EmployeeId = employeeId,
                Date = DateTime.Now.Date,
                Shift = "Утренний",
                HoursWorked = 8.0, // Например, 8 часов
                DaysOff = "Суббота, Воскресенье" // Пример выходных
            };

            AddWorkSchedule(schedule);
        }


        public void Open()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        private void ExecuteNonQuery(string query)
        {
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // CRUD методы для должностей
        public void AddPosition(Position position)
        {
            string query = "INSERT INTO positions (title, salary) VALUES (@title, @salary)";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@title", position.Title);
                command.Parameters.AddWithValue("@salary", position.Salary);
                command.ExecuteNonQuery();
            }
        }

        public List<Position> GetPositions()
        {
            var positions = new List<Position>();

            if (_connection.State != ConnectionState.Open)
            {
                Open();
            }

            using (var command = new SQLiteCommand("SELECT * FROM positions", _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        positions.Add(new Position
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Salary = reader.GetDouble(2)
                        });
                    }
                }
            }

            return positions;
        }

        public void UpdatePosition(Position position)
        {
            string query = "UPDATE positions SET title = @title, salary = @salary WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@title", position.Title);
                command.Parameters.AddWithValue("@salary", position.Salary);
                command.Parameters.AddWithValue("@id", position.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeletePosition(int id)
        {
            string query = "DELETE FROM positions WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        // CRUD методы для сотрудников
        public void AddEmployee(Employee employee)
        {
            string query = "INSERT INTO employees (name, position_id, department, email, phone, hire_date, work_mode) VALUES (@name, @positionId, @department, @email, @phone, @hireDate, @workMode)";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@name", employee.Name);
                command.Parameters.AddWithValue("@positionId", employee.PositionId.HasValue ? (object)employee.PositionId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@department", employee.Department);
                command.Parameters.AddWithValue("@email", employee.Email);
                command.Parameters.AddWithValue("@phone", employee.Phone);
                command.Parameters.AddWithValue("@hireDate", employee.HireDate);
                command.Parameters.AddWithValue("@workMode", employee.WorkMode);
                command.ExecuteNonQuery();
            }
        }
        

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            string query = "SELECT * FROM employees";
            using (var command = new SQLiteCommand(query, _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PositionId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            Department = reader.GetString(3),
                            Email = reader.GetString(4),
                            Phone = reader.GetString(5),
                            HireDate = reader.GetDateTime(6),
                            WorkMode = reader.GetString(7)
                        });
                    }
                }
            }
            return employees;
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = "UPDATE employees SET name = @name, position_id = @positionId, department = @department, email = @email, phone = @phone, hire_date = @hireDate, work_mode = @workMode WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@name", employee.Name);
                command.Parameters.AddWithValue("@positionId", employee.PositionId.HasValue ? (object)employee.PositionId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@department", employee.Department);
                command.Parameters.AddWithValue("@email", employee.Email);
                command.Parameters.AddWithValue("@phone", employee.Phone);
                command.Parameters.AddWithValue("@hireDate", employee.HireDate);
                command.Parameters.AddWithValue("@workMode", employee.WorkMode);
                command.Parameters.AddWithValue("@id", employee.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            string query = "DELETE FROM employees WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        // CRUD методы для расписания
        public void AddWorkSchedule(WorkSchedule schedule)
        {
            // Использование заглушек для времени
            schedule.StartTime = TimeStubs.GetStartOfWorkDay();
            schedule.EndTime = TimeStubs.GetEndOfWorkDay();
            schedule.ArrivalTime = TimeStubs.GetArrivalTime();
            schedule.DepartureTime = TimeStubs.GetDepartureTime();

            string query = "INSERT INTO work_schedule (employee_id, date, start_time, end_time, hours_worked, shift, arrival_time, departure_time, days_off) VALUES (@employeeId, @date, @startTime, @endTime, @hoursWorked, @shift, @arrivalTime, @departureTime, @daysOff)";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@employeeId", schedule.EmployeeId);
                command.Parameters.AddWithValue("@date", schedule.Date);
                command.Parameters.AddWithValue("@startTime", schedule.StartTime.ToString());
                command.Parameters.AddWithValue("@endTime", schedule.EndTime.ToString());
                command.Parameters.AddWithValue("@hoursWorked", schedule.HoursWorked);
                command.Parameters.AddWithValue("@shift", schedule.Shift);
                command.Parameters.AddWithValue("@arrivalTime", schedule.ArrivalTime.ToString());
                command.Parameters.AddWithValue("@departureTime", schedule.DepartureTime.ToString());
                command.Parameters.AddWithValue("@daysOff", schedule.DaysOff);
                command.ExecuteNonQuery();
            }
        }


        public List<WorkSchedule> GetWorkSchedules()
        {
            var schedules = new List<WorkSchedule>();
            string query = "SELECT * FROM work_schedule";
            using (var command = new SQLiteCommand(query, _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        schedules.Add(new WorkSchedule
                        {
                            Id = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            Date = reader.GetDateTime(2),
                            StartTime = TimeSpan.Parse(reader.GetString(3)),
                            EndTime = TimeSpan.Parse(reader.GetString(4)),
                            HoursWorked = reader.GetDouble(5),
                            Shift = reader.GetString(6),
                            ArrivalTime = TimeSpan.Parse(reader.GetString(7)),
                            DepartureTime = TimeSpan.Parse(reader.GetString(8)),
                            DaysOff = reader.GetString(9)
                        });
                    }
                }
            }
            return schedules;
        }

        public void UpdateWorkSchedule(WorkSchedule schedule)
        {
            string query = "UPDATE work_schedule SET employee_id = @employeeId, date = @date, start_time = @startTime, end_time = @endTime, hours_worked = @hoursWorked, shift = @shift, arrival_time = @arrivalTime, departure_time = @departureTime, days_off = @daysOff WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@employeeId", schedule.EmployeeId);
                command.Parameters.AddWithValue("@date", schedule.Date);
                command.Parameters.AddWithValue("@startTime", schedule.StartTime.ToString());
                command.Parameters.AddWithValue("@endTime", schedule.EndTime.ToString());
                command.Parameters.AddWithValue("@hoursWorked", schedule.HoursWorked);
                command.Parameters.AddWithValue("@shift", schedule.Shift);
                command.Parameters.AddWithValue("@arrivalTime", schedule.ArrivalTime.ToString());
                command.Parameters.AddWithValue("@departureTime", schedule.DepartureTime.ToString());
                command.Parameters.AddWithValue("@daysOff", schedule.DaysOff);
                command.Parameters.AddWithValue("@id", schedule.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteWorkSchedule(int id)
        {
            string query = "DELETE FROM work_schedule WHERE id = @id";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
public static class TimeStubs
{
    // Метод для получения заглушки времени начала рабочего дня
    public static TimeSpan GetStartOfWorkDay()
    {
        return new TimeSpan(9, 0, 0); // 09:00:00
    }

    // Метод для получения заглушки времени конца рабочего дня
    public static TimeSpan GetEndOfWorkDay()
    {
        return new TimeSpan(17, 0, 0); // 17:00:00
    }

    // Метод для получения заглушки времени прибытия
    public static TimeSpan GetArrivalTime()
    {
        return new TimeSpan(8, 45, 0); // 08:45:00
    }

    // Метод для получения заглушки времени отъезда
    public static TimeSpan GetDepartureTime()
    {
        return new TimeSpan(17, 15, 0); // 17:15:00
    }
}


