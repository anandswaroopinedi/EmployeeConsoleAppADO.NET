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
                project.Id = projectList.Count + 1;
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
        public async Task<string> GetProjectName(int id)
        {
            List<Project> projectList = await GetAll();
            for (int i = 0; i < projectList.Count; i++)
            {
                if (projectList[i].Id == id)
                {
                    return projectList[i].Name;
                }
            }
            return "";
        }
    }
}
