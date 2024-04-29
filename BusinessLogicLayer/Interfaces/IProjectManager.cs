using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectManager
    {
        public Task<bool> AddProject(Project project);
        public Task<List<Project>> GetAll();
        public Task<string> GetProjectName(int id);
    }
}
