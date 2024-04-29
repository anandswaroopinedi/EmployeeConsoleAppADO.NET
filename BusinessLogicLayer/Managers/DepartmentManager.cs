using DataAccessLayer.Interface;
using BusinessLogicLayer.Interfaces;
using Models;
namespace BusinessLogicLayer.Managers
{
    public class DepartmentManager : IDepartmentManager
    {

        private readonly IDataOperations _dataOperations;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Department.json";
        public DepartmentManager(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }

        public static bool CheckDepartmentExists(string name, List<Department> departmentList)
        {
            for (int i = 0; i < departmentList.Count; i++)
            {
                if (departmentList[i].Name == name)
                    return true;
            }
            return false;
        }
        public async Task<string> GetDepartmentName(int id)
        {
            List<Department> departmentList = await GetAll();
            for (int i = 0; i < departmentList.Count; i++)
            {
                if (departmentList[i].Id == id)
                {
                    return departmentList[i].Name;
                }
            }
            return "None";
        }
        public async Task<bool> AddDepartment(Department dept)
        {

            List<Department> departmentList = GetAll().Result;
            if (!CheckDepartmentExists(dept.Name, departmentList))
            {
                dept.Id = departmentList.Count + 1;
                departmentList.Add(dept);
                Console.WriteLine(dept.Name);
                if(await _dataOperations.AddDepartmentToDb(dept))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _dataOperations.GetDepartments();
        }
    }
}
