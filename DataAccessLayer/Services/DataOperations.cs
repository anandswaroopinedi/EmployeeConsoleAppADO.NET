using DataAccessLayer.Interface;
using Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
namespace DataAccessLayer.Services
{
    public class DataOperations : IDataOperations
    {
        private String ConnectionString = "data source=SQL-DEV; database=ConsoleAppDB; integrated security=SSPI; ENCRYPT=False";
        public static void AddParameters(Employee employee, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", employee.Id);
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            if (!string.IsNullOrEmpty(employee.DateOfBirth))
                cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(employee.DateOfBirth).Date);
            cmd.Parameters.AddWithValue("@JoiningDate", DateTime.Parse(employee.JoinDate).Date);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            if (!string.IsNullOrEmpty(employee.MobileNumber))
                cmd.Parameters.AddWithValue("@MobileNo", employee.MobileNumber);
            cmd.Parameters.AddWithValue("@RoleID", employee.JobTitleId);
            cmd.Parameters.AddWithValue("@LocationID", employee.LocationId);
            cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentId);
            if (!string.IsNullOrEmpty(employee.ManagerId))
                cmd.Parameters.AddWithValue("@ManagerID", employee.ManagerId);
            cmd.Parameters.AddWithValue("@ProjectID", employee.ProjectId);
        }
        public async Task<bool> AddEmployeeToDb(Employee employee)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddParameters(employee, cmd);
                    await Task.FromResult(cmd.ExecuteNonQuery());
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in creating an employee.\n" + e);
                return false;
            }
        }
        public async Task<bool> AddDepartmentToDb(Department department)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertDepartment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", department.Id);
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in creating an department.\n" + e);
                return false;
            }
        }
        public async Task<bool> AddProjectToDb(Project project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertProject", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", project.Id);
                    cmd.Parameters.AddWithValue("@Name", project.Name);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in creating an project.\n" + e);
                return false;
            }
        }
        public async Task<bool> AddLocationToDb(Location location)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertLocation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", location.Id);
                    cmd.Parameters.AddWithValue("@Name", location.Name);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in creating an location.\n" + e);
                return false;
            }
        }
        public async Task<bool> AddRoleToDb(Roles role)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", role.Id);
                    cmd.Parameters.AddWithValue("@Name", role.Name);
                    if (role.Description != null)
                        cmd.Parameters.AddWithValue("@Description", role.Description);
                    cmd.Parameters.AddWithValue("@DepartmentId", role.DepartmentId);
                    cmd.Parameters.AddWithValue("@LocationId", role.LocationId);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in creating an role.\n" + e);
                return false;
            }
        }
        public async Task<Employee> GetEmployeeById(string Id)
        {
            Employee employee = new Employee();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectOneEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        mapEmployeeReaderObjects(employee, reader);
                    }
                    return employee;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving an employee.\n" + e);
                return employee;
            }
        }
        public static void mapEmployeeReaderObjects(Employee employee, SqlDataReader reader)
        {
            employee.Id = Convert.ToString(reader[0]);
            employee.FirstName = Convert.ToString(reader[1]);
            employee.LastName = Convert.ToString(reader[2]);
            employee.DateOfBirth = Convert.ToString(reader[3]);
            employee.Email = Convert.ToString(reader[4]);
            employee.MobileNumber = Convert.ToString(reader[5]);
            employee.JoinDate = Convert.ToString(reader[6]).Substring(0, 10);
            employee.JobTitleId = Convert.ToInt32(reader[7]);
            employee.LocationId = Convert.ToInt32(reader[8]);
            employee.DepartmentId = Convert.ToInt32(reader[9]);
            employee.ManagerId = Convert.ToString(reader[10]);
            employee.ProjectId = Convert.ToInt32(reader[11]);
        }
        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> Employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectAllEmployees", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        mapEmployeeReaderObjects(employee, reader);
                        Employees.Add(employee);
                    }
                    return Employees;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving employees.\n" + e);
                return Employees;
            }
        }
        public async Task<List<Department>> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectAllDepartments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Department dept = new Department();
                        dept.Id = Convert.ToInt32(reader[0]);
                        dept.Name = Convert.ToString(reader[1]);
                        departments.Add(dept);
                    }
                    return departments;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving an Department.\n" + e);
                return departments;
            }
        }
        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectAllProjects", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.Id = Convert.ToInt32(reader[0]);
                        p.Name = Convert.ToString(reader[1]);
                        projects.Add(p);
                    }
                    return projects;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving an Projects.\n" + e);
                return projects;
            }
        }
        public async Task<List<Location>> GetLocations()
        {
            List<Location> locations = new List<Location>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectAllLocations", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Location location = new Location();
                        location.Id = Convert.ToInt32(reader[0]);
                        location.Name = Convert.ToString(reader[1]);
                        locations.Add(location);
                    }
                    return locations;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving an Location.\n" + e);
                return locations;
            }
        }
        public async Task<List<Roles>> GetRoles()
        {
            List<Roles> roles = new List<Roles>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspSelectAllRoles", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Roles role = new Roles();
                        role.Id = Convert.ToInt32(reader[0]);
                        role.Name = Convert.ToString(reader[1]);
                        role.DepartmentId = Convert.ToInt32(reader[2]);
                        role.LocationId = Convert.ToInt32(reader[3]);
                        role.Description = Convert.ToString(reader[4]);
                        roles.Add(role);
                    }
                    return roles;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in retrieving an role.\n" + e);
                return roles;
            }
        }
        public async Task<bool> DeleteEmployee(string id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspDeleteEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in deleting an employee.\n" + e);
                return false;
            }
        }
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("uspUpdateEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddParameters(employee, cmd);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong in updating an employee.\n" + e);
                return false;
            }
        }

    public async Task<List<T>> Read<T>(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonData =await reader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        return JsonSerializer.Deserialize<List<T>>(jsonData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing project data: {ex.Message}");
            }
            return new List<T>();
        }
        public async Task<bool> Write<T>(List<T> t, string filePath)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(t, new JsonSerializerOptions
                {
                    WriteIndented = true // Optional: Format the JSON for better readability
                });
                await File.WriteAllTextAsync(filePath, jsonData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing project data: {ex.Message}");
                return false;
            }
        }
    }
}
