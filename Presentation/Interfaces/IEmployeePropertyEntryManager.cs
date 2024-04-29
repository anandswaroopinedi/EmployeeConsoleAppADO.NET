using Models;
namespace Presentation.Interfaces
{
    public interface IEmployeePropertyEntryManager
    {
        public void DisplayHeaders();
        public string GetFirstName();
        public string GetLastName();
        public string GetEmail();
        public string GetDateOfBirth();
        public string GetMobileNo();
        public string GetJoiningDate();
        public Task<int> ChooseProject();
        public Task<int> ChooseRole();
        public Task<int> ChooseDepartment(Employee employee);
        public Task<int> ChooseLocation(Employee employee);
        public string ChooseManager(Employee employeeModel, List<Employee> employeeList);
    }
}
