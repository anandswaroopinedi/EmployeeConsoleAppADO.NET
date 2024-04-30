using DataAccessLayer.Interface;
using Models;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Managers
{
    public class ProjectManager : IProjectManager
    {
        private readonly IDataOperations _dataOperations;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Project.json";
        public ProjectManager(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }

        public async Task<bool> AddProject(Project project)
        {
            List<Project> projectList = GetAll().Result;

            if (!CheckProjectExists(project.Name, projectList))
            {
                projectList.Add(project);
                if (await _dataOperations.AddProjectToDb(project))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Project>> GetAll()
        {
            return await _dataOperations.GetProjects();
        }
        public static bool CheckProjectExists(string project, List<Project> projectList)
        {
            for (int i = 0; i < projectList.Count; i++)
            {
                if (projectList[i].Name == project)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
