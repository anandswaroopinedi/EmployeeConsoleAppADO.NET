using BusinessLogicLayer.Interfaces;
using Models;
using Presentation.Interfaces;
using Presentation.Validations;

namespace Presentation.Services
{
    public class EmployeePropertyEntryManager : IEmployeePropertyEntryManager
    {
        private readonly IRoleManagement _roleManagement;
        private readonly IRoleManager _roleManager;
        private readonly IProjectManager _projectManager;
        private readonly IProjectManagement _projectManagement;
        private readonly IDepartmentManager _departmentManager;
        private readonly ILocationManager _locationManager;
        public EmployeePropertyEntryManager(IRoleManagement roleManagement, IRoleManager roleManager, IProjectManager projectManager, IProjectManagement projectManagement, ILocationManager locationManager, IDepartmentManager departmentManager)
        {
            _roleManagement = roleManagement;
            _roleManager = roleManager;
            _projectManager = projectManager;
            _projectManagement = projectManagement;
            _locationManager = locationManager;
            _departmentManager = departmentManager;
        }
        public string GetFirstName()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Choose to Enter First Name");
            Console.Write("Choose options from above List:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return "Abort";
            }
            else if (option == 1)
            {
                Console.Write("Enter Employee FirstName*:");
                string empFirstName = Console.ReadLine().ToUpper();
                if (Validation.ValidateName(empFirstName))
                {
                    return empFirstName;
                }
                else
                {
                    Console.WriteLine("The First Name should only contains alphabets");
                    return GetFirstName();
                }
            }
            else
            {
                Console.WriteLine("You should choose options from above List only");
                return GetFirstName();
            }
        }
        public string GetLastName()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Choose to Enter LastName");
            Console.Write("Choose options from above List:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return "Abort";
            }
            else if (option == 1)
            {
                Console.Write("Enter Employee Last Name*:");
                string empLastName = Console.ReadLine().ToUpper();
                if (Validation.ValidateName(empLastName))
                {
                    return empLastName;
                }
                else
                {
                    Console.WriteLine("The Last Name should only contains alphabets");
                    return GetLastName();
                }
            }
            else
            {
                Console.WriteLine("You should choose options from above List only");
                return GetLastName();
            }
        }
        public string GetDateOfBirth()
        {
            Console.WriteLine("Date Of Birth:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Upload Later");
            Console.WriteLine("2. Enter Manually Now");

            Console.Write("Choose from above options:");
            int.TryParse(Console.ReadLine(), out int res);
            switch (res)
            {
                case 0:
                    return "Abort";
                case 1:
                    return null;
                case 2:
                    Console.Write("Enter Date Of Birth(DD/MM/YYYY):");
                    string dob = Console.ReadLine();
                    DateTime dateTime = DateTime.Parse("01/01/2003");
                    DateTime date = new DateTime();
                    if (DateTime.TryParse(dob, out date) && date < dateTime)
                    {
                        return dob;
                    }
                    Console.WriteLine("Enter Date Of Birth Correctly(It should be dd/mm/yyyy format and should be less than 01/01/2003)");
                    return GetDateOfBirth();

                default:
                    Console.WriteLine("Select option from the above list only");
                    return GetDateOfBirth();
            }
        }
        public string GetEmail()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Choose to Enter Email");
            Console.Write("Choose options from above List:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return "Abort";
            }
            else if (option == 1)
            {
                Console.Write("Enter Employee Email*:");
                string empEmail = Console.ReadLine().ToUpper();
                if (Validation.ValidateEmail(empEmail))
                {
                    return empEmail;
                }
                else
                {
                    Console.WriteLine("The entered email is not valid");
                    return GetEmail();
                }
            }
            else
            {
                Console.WriteLine("You should choose options from above List only");
                return GetEmail();
            }
        }
        public string GetMobileNo()
        {
            Console.WriteLine("Mobile Number:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Upload Later");
            Console.WriteLine("2. Enter Manually Now");

            Console.Write("Choose from above options:");
            int.TryParse(Console.ReadLine(), out int res);
            switch (res)
            {
                case 0: return "Abort";

                case 1:
                    return null;
                case 2:
                    Console.Write("Enter Mobile No:");
                    string mobNo = Console.ReadLine();
                    if (Validation.ValidateMobileNumber(mobNo))
                    {
                        return mobNo;
                    }
                    else
                    {
                        Console.WriteLine("Entered mobile number is invalid(It should contain only numeric characters and of length 10)");
                        return GetMobileNo();
                    }
                default:
                    Console.WriteLine("Select option from the above list only");
                    return GetMobileNo();
            }
        }
        public string GetJoiningDate()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Choose to Enter Joining Date");

            Console.Write("Choose options from above List:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return "Abort";
            }
            else if (option == 1)
            {

                Console.Write("Enter Joining Date(DD/MM/YYYY)*:");
                string JoiningDate = Console.ReadLine();
                if (Validation.ValidateJoiningDate(JoiningDate))
                {
                    return JoiningDate;
                }
                else
                {
                    Console.WriteLine("Entered Joining Date is Invalid(It should follow dd/mm/yyyy format and less than today date)");
                    return GetJoiningDate();
                }
            }
            else
            {
                Console.WriteLine("You should choose options from above List only");
                return GetJoiningDate();
            }
        }
        public async Task<int> ChooseProject()
        {
            List<Project> projectList =await _projectManager.GetAll();
            Console.WriteLine("Select Project:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Add New Project");
            for (int j = 0; j < projectList.Count; j++)
            {
                Console.WriteLine($"{j + 2}. {projectList[j].Name}");
            }

            Console.Write("Choose from the above options:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return 0;
            }
            else if (option == 1)
            {
                int result = await _projectManagement.AddProject();
                if (result == 0)
                {
                    return 0;
                }
                else if (result == -1)
                {
                    return await ChooseProject();
                }
                else
                {
                    projectList = await _projectManager.GetAll();
                    return projectList[projectList.Count - 1].Id;
                }
            }
            if (option > 1 && option <= projectList.Count + 1)
            {
                return projectList[option - 2].Id;
            }
            else
            {
                Console.WriteLine("You can only choose from above list");
            }
            return await ChooseProject();

        }
        public static string DisplayEmployeeId(Employee employee, List<Employee> employeeList)
        {
            Console.WriteLine("0. Exit");
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].Id != employee.Id)
                {
                    Console.WriteLine($"{i + 1}  {employeeList[i].Id}  {employeeList[i].FirstName + "  " + employeeList[i].LastName}");
                }
            }
            Console.Write("Choose from above options:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return "Abort";
            }
            if (option > 0 && option <= employeeList.Count)
            {
                return employeeList[option - 1].Id;
            }
            else
            {
                return DisplayEmployeeId(employee, employeeList);
            }
        }
        public static int CheckIdExists(string id, List<Employee> employeeList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].Id == id)
                    return i;
            }

            return -1;
        }
        public string ChooseManager(Employee emp, List<Employee> employeeList)
        {
            string managerId = "";
            Console.WriteLine("Manager Details:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Upload Later");
            Console.WriteLine("2. Enter Manually");

            Console.Write("Choose from above options:");
            int.TryParse(Console.ReadLine(), out int option);
            switch (option)
            {
                case 0: return "Abort";
                case 1:
                    managerId = null;
                    break;
                case 2:
                    managerId = DisplayEmployeeId(emp, employeeList);
                    if (managerId == "Abort")
                    {
                        return "Abort";
                    }
                    if (Validation.ValidateManagerId(managerId) || CheckIdExists(managerId, employeeList) != -1)
                    {
                        return managerId;
                    }
                    else
                    {
                        Console.WriteLine("The ManagerId entered is invalid or not in records or matching the Employee id itself");
                        return ChooseManager(emp, employeeList);
                    }
                default:
                    Console.WriteLine("Select option from the above list only");
                    return ChooseManager(emp, employeeList);
            }
            return managerId;
        }
        public static void DisplayRolesNames(List<Roles> rolesList)
        {
            for (int i = 0; i < rolesList.Count; i++)
            {
                Console.WriteLine($"{i + 2}. {rolesList[i].Name}");
            }
        }
        public async Task<int> ChooseRole()
        {
            List<Roles> rolesList = await _roleManager.GetAll();
            int option;
            Console.WriteLine("Roles:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Enter New Role:");
            DisplayRolesNames(rolesList);

            Console.Write("Choose Roles from above options*:");
            int.TryParse(Console.ReadLine(), out option);
            if (option == 0)
            {
                return 0;
            }
            if (option == 1)
            {
                /*RoleManagement roleManagement = new RoleManagement();*/
                int result = await _roleManagement.AddRole();
                if (result != 0)
                {
                    return result;
                }
                else if (result == -1)
                {
                    return await ChooseRole();
                }
                else
                {
                    return 0;
                }
            }
            if (option > 1 && option <= rolesList.Count + 1)
            {
                return rolesList[option - 2].Id;
            }
            else
            {
                Console.WriteLine("Select option from the above list only");
                return await ChooseRole();
            }
        }

        public void DisplayHeaders()
        {

            for (int j = 0; j < Employee.Headers.Length; j++)
            {
                Console.WriteLine("{0,-4}{1,-15}", $"{j}.", $"{Employee.Headers[j]}");
            }
        }
        public async Task<int> ChooseDepartment(Employee employee)
        {
            List<Roles> rolesList = await _roleManager.GetAll();
            List<int> departmentIds = new List<int>();
            Console.WriteLine("Select Department:");
            Console.WriteLine("0. Exit");
            for (int j = 0; j < rolesList.Count; j++)
            {
                if (rolesList[j].Id == employee.JobTitleId)
                {
                    departmentIds.Add(rolesList[j].DepartmentId);
                    List<Department> deptList = await _departmentManager.GetAll();
                    for (int k = 0; k < deptList.Count; k++)
                    {
                        if (rolesList[j].DepartmentId == deptList[k].Id)
                        {
                            Console.WriteLine($"{departmentIds.Count}. {deptList[k].Name}");
                        }
                    }

                }
            }

            Console.Write("Choose from the above options:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return 0;
            }
            if (option > 0 && option <= departmentIds.Count)
            {
                return departmentIds[option - 1];
            }
            else
            {
                Console.WriteLine("You can only choose from above list");
            }
            return await ChooseDepartment(employee);
        }
        public async Task<int> ChooseLocation(Employee employee)
        {
            List<Roles> rolesList = await _roleManager.GetAll();
            List<int> locationIds = new List<int>();
            Console.WriteLine("Select Location:");
            Console.WriteLine("0. Exit");
            for (int j = 0; j < rolesList.Count; j++)
            {
                if (rolesList[j].Id == employee.JobTitleId)
                {
                    locationIds.Add(rolesList[j].LocationId);
                    List<Location> locationModels = await _locationManager.GetAll();
                    for (int k = 0; k < locationModels.Count; k++)
                    {
                        if (locationModels[k].Id == rolesList[j].LocationId)
                        {

                            Console.WriteLine($"{locationIds.Count}. {locationModels[k].Name}");
                        }

                    }
                }
            }

            Console.Write("Choose from the above options:");
            int.TryParse(Console.ReadLine(), out int option);
            if (option == 0)
            {
                return 0;
            }
            if (option > 0 && option <= locationIds.Count)
            {
                return locationIds[option - 1];
            }
            else
            {
                Console.WriteLine("You can only choose from above list");
            }
            return await ChooseLocation(employee);
        }
    }
}
