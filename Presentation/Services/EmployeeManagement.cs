﻿using BusinessLogicLayer.Interfaces;
using Models;
using Presentation.Interfaces;

namespace Presentation.Services
{
    public class EmployeeManagement : IEmployeeManagement
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IEmployeePropertyEntryManager _employeePropertyEntryManager;
        private readonly IProjectManager _projectManager;
        private readonly ILocationManager _locationManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IRoleManager _roleManager;
        private string _OutputFormat = "{0,-8} {1,-24} {2,-12} {3,-24} {4,-18} {5,-24} {6,-24} {7,-24}";
        public EmployeeManagement(IEmployeeManager employee, IEmployeePropertyEntryManager employeePropertyEntryManager, IDepartmentManager departmentManager, ILocationManager locationManager, IProjectManager projectManager, IRoleManager roleManager)
        {
            _employeeManager = employee;
            _employeePropertyEntryManager = employeePropertyEntryManager;
            _projectManager = projectManager;
            _locationManager = locationManager;
            _departmentManager = departmentManager;
            _roleManager = roleManager;
        }
        public async Task<bool> GetUpdateDetails(Employee employee, List<Employee> employeeList)
        {
            _employeePropertyEntryManager.DisplayHeaders();
            Console.Write("\nChoose the details to change:");
            int.TryParse(Console.ReadLine(), out int option);
            bool res = false;
            string value;
            while (option <= 11 && option >= 1)
            {
                bool operation = true;
                switch (option)
                {
                    case 1:

                        value = _employeePropertyEntryManager.GetFirstName();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.FirstName = value;
                        }
                        break;
                    case 2:
                        value = _employeePropertyEntryManager.GetLastName();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.LastName = value;
                        }
                        break;
                    case 3:
                        value = _employeePropertyEntryManager.GetDateOfBirth();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.DateOfBirth = value;
                        }
                        break;
                    case 4:
                        value = _employeePropertyEntryManager.GetEmail();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.Email = value;
                        }
                        break;
                    case 5:
                        value = _employeePropertyEntryManager.GetMobileNo();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.MobileNumber = value;
                        }
                        break;
                    case 6:
                        value = _employeePropertyEntryManager.GetJoiningDate();
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.JoinDate = value;
                        }
                        break;
                    case 7:
                        int roleId =await _employeePropertyEntryManager.ChooseRole();
                        if (roleId == 0)
                        {
                            operation = false;
                            break;
                        }
                        else
                        {
                            employee.JobTitleId = roleId;
                        }
                        int deptId = await _employeePropertyEntryManager.ChooseDepartment(employee);
                        if (deptId == 0)
                        {
                            operation = false;
                            break;
                        }
                        else
                        {
                            employee.DepartmentId = deptId;
                        }
                        int locationId = await _employeePropertyEntryManager.ChooseLocation(employee);
                        if (locationId == 0)
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.LocationId = locationId;
                        }
                        break;
                    case 8:
                        locationId = await _employeePropertyEntryManager.ChooseLocation(employee);
                        if (locationId == 0)
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.LocationId = locationId;
                        }
                        break;
                    case 9:
                        deptId = await _employeePropertyEntryManager.ChooseDepartment(employee);
                        if (deptId == 0)
                        {
                            operation = false;
                            break;
                        }
                        else
                        {
                            employee.DepartmentId = deptId;
                        }
                        break;
                    case 10:
                        value = _employeePropertyEntryManager.ChooseManager(employee, employeeList);
                        if (value == "Abort")
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.ManagerId = value;
                        }
                        break;
                    case 11:
                        int projectId = await _employeePropertyEntryManager.ChooseProject();
                        if (projectId == 0)
                        {
                            operation = false;
                        }
                        else
                        {
                            employee.ProjectId = projectId;
                        }
                        break;
                    default:
                        Console.WriteLine("Modification can only be done on the above properties only");
                        break;
                }
                if (operation == true)
                {
                    res = true;
                    _employeePropertyEntryManager.DisplayHeaders();
                    Console.Write("\nChoose the details to change:");
                    int.TryParse(Console.ReadLine(), out option);
                }
                else
                {
                    option = 0;
                }

            }
            return res;
        }
        //To Record the details of employee for update.
        public static async Task<string> GetManagerName(Employee e, List<Employee> employeeList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].Id == e.ManagerId)
                {
                    return employeeList[i].FirstName + " " + employeeList[i].LastName;
                }
            }
            return "NO MANAGER";
        }
        public static async Task DisplayEmployeeID(List<Employee> employeeList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {employeeList[i].Id}   {employeeList[i].FirstName}");
            }
        }
        public async Task UpdateEmployee()
        {
            List<Employee> employeeList = await _employeeManager.GetAll();
            Console.WriteLine("Employee Id's:");
            Console.WriteLine("0. Exit");
            await DisplayEmployeeID(employeeList);
            Console.Write("Choose from the above list:");
            int.TryParse(Console.ReadLine(), out int index);
            if (index == 0)
            {
                return;
            }
            if (index > 0 && index <= employeeList.Count)
            {
                bool res = await GetUpdateDetails(employeeList[index - 1], employeeList);
                if (res && await _employeeManager.Update(employeeList[index - 1]))
                {
                    string managerName = await GetManagerName(employeeList[index - 1], employeeList);
                    Console.WriteLine(_OutputFormat, "Id", "Full Name", "DateOfBirth", "Email", "MobileNumber", "JoinDate", "JobTitle", "Location", "Department", "ManagerName", "Project");
                    Console.WriteLine(new string('-', 158));
                    Console.WriteLine(_OutputFormat, employeeList[index - 1].Id, employeeList[index - 1].FirstName + " " + employeeList[index - 1].LastName, employeeList[index - 1].DateOfBirth, employeeList[index - 1].Email, employeeList[index - 1].MobileNumber, employeeList[index - 1].JoinDate,await _roleManager.GetRoleName(employeeList[index - 1].JobTitleId),await _locationManager.GetLocationName(employeeList[index - 1].LocationId),await _departmentManager.GetDepartmentName(employeeList[index - 1].DepartmentId), managerName,await _projectManager.GetProjectName(employeeList[index - 1].ProjectId));
                    Console.WriteLine("Employee Updated successfully");
                }
                else
                {
                    Console.WriteLine("Updation Unsuccessful");
                }
            }
            else
            {
                Console.WriteLine("Choose from above options only.");
                await UpdateEmployee();
            }
        }
        //To make the managerId's of employee that are containing the deleted id should be made default None
        //To Record the details of employee to perform deletion Operation
        public async Task DeleteEmployee()
        {
            Console.WriteLine("Employee Id's:");
            List<Employee> employeeList = await _employeeManager.GetAll();
            Console.WriteLine($"Exit");
            await DisplayEmployeeID(employeeList);
            Console.Write("Enter Employee Id/Enter exit:");
            string id = Console.ReadLine()!.ToUpper();
            if (id == "EXIT")
            {
                return;
            }
            if (await _employeeManager.Delete(id))
            {

                Console.WriteLine("Employee deleted successfully");
            }
            else
            {
                Console.WriteLine("Employee Id doesn't exists");
            }
        }
        //To Display Employee through their id
        public async Task DisplayOne()
        {
            List<Employee> employeeList = await _employeeManager.GetAll();
            Console.WriteLine("Employee Id's:");
            Console.WriteLine("0. Exit");
            await DisplayEmployeeID(employeeList);
            Console.Write("Choose from the above list:");
            int.TryParse(Console.ReadLine(), out int index);
            if (index == 0)
            {
                return;
            }
            if (index > 0 && index <= employeeList.Count)
            {
                if (employeeList[index - 1].Id != "None")
                {
                    string managerName = await GetManagerName(employeeList[index - 1], employeeList);
                    Console.WriteLine(_OutputFormat, "Id", "Full Name", "JoinDate", "JobTitle", "Location", "Department", "ManagerName", "Project");
                    await ConsoleEmployee(employeeList[index - 1], managerName);
                }
                else
                {
                    Console.WriteLine("Employee Id doesn't exists");
                }
            }
            else
            {
                Console.WriteLine("Choose from above options only.");
            }
        }
        private async Task ConsoleEmployee(Employee employee, string managerName)
        {
            Console.WriteLine(new string('-', 155));
            Console.WriteLine(_OutputFormat, employee.Id, employee.FirstName + " " + employee.LastName, employee.JoinDate, await _roleManager.GetRoleName(employee.JobTitleId),await _locationManager.GetLocationName(employee.LocationId),await _departmentManager.GetDepartmentName(employee.DepartmentId), managerName, await _projectManager.GetProjectName(employee.ProjectId));
        }
        public async Task DisplayAll()
        {
            List<Employee> employeeList =await _employeeManager.GetAll();
            Console.WriteLine($"\nNo of Employee records in the Database are: {employeeList.Count}");
            Console.WriteLine(_OutputFormat, "Id", "Full Name", "JoinDate", "JobTitle", "Location", "Department", "ManagerName", "Project");
            foreach (Employee employee in employeeList)
            {
                await ConsoleEmployee(employee, await GetManagerName(employee, employeeList));
            }
        }
        private async Task<bool> checkEmployeeExisits(Employee employee, List<Employee> employeeList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employee.FirstName == employeeList[i].FirstName && employee.LastName == employeeList[i].LastName && employee.DateOfBirth == employeeList[i].DateOfBirth)
                {
                    return true;
                }
            }
            return false;
        }
        private async Task<bool> GetEmployeeInputValues(Employee employee, List<Employee> employeeList)
        {
            employee.FirstName = _employeePropertyEntryManager.GetFirstName();
            if (employee.FirstName == "Abort")
            {
                return false;
            }
            employee.LastName = _employeePropertyEntryManager.GetLastName();
            if (employee.LastName == "Abort")
            {
                return false;
            }
            employee.DateOfBirth = _employeePropertyEntryManager.GetDateOfBirth();
            if (employee.DateOfBirth == "Abort")
            {
                return false;
            }
            if (await checkEmployeeExisits(employee, employeeList))
            {
                return false;
            }
            employee.Email = _employeePropertyEntryManager.GetEmail();
            if (employee.Email == "Abort")
            {
                return false;
            }
            employee.MobileNumber = _employeePropertyEntryManager.GetMobileNo();
            if (employee.MobileNumber == "Abort")
            {
                return false;
            }
            employee.JoinDate = _employeePropertyEntryManager.GetJoiningDate();
            if (employee.JoinDate == "Abort")
            {
                return false;
            }
            employee.JobTitleId = await _employeePropertyEntryManager.ChooseRole();
            if (employee.JobTitleId == 0)
            {
                return false;
            }
            employee.LocationId = await _employeePropertyEntryManager.ChooseLocation(employee);
            if (employee.LocationId == 0)
            {
                return false;
            }
            employee.DepartmentId =await _employeePropertyEntryManager.ChooseDepartment(employee);
            if (employee.DepartmentId == 0)
            {
                return false;
            }
            employee.ManagerId = _employeePropertyEntryManager.ChooseManager(employee, employeeList);
            if (employee.ManagerId == "Abort")
            {
                return false;
            }
            employee.ProjectId =await _employeePropertyEntryManager.ChooseProject();
            if (employee.ProjectId == 0)
            {
                return false;
            }
            return true;
        }
        public  static async Task<string> GetNewEmployeeId(List<Employee> employeeList)
        {
            int prevId;
            if (employeeList.Count == 0)
            {
                prevId = 0;
            }
            else
            {
                prevId = Int32.Parse((employeeList[employeeList.Count - 1].Id).Substring(2));
            }

            string newId = (prevId + 1).ToString();
            newId = newId.PadLeft(4, '0');
             return "TZ" + newId;
        }
        public async Task AddEmployee()
        {
            Employee emp = new Employee();
            List<Employee> employeeList = await _employeeManager.GetAll();
            bool addResult = await GetEmployeeInputValues(emp, employeeList);
            if (addResult)
            {
                emp.Id = await GetNewEmployeeId(employeeList);
                if (await _employeeManager.Create(emp))
                {
                    Console.WriteLine($"Employee Added Successfully with id {emp.Id}");
                }
                else
                {
                    Console.WriteLine("Employee Addition Unsuccessful");
                }
                return;
            }
            else
            {
                Console.WriteLine("Employee Exists previously in the Database");
                return;
            }
        }
    }
}
