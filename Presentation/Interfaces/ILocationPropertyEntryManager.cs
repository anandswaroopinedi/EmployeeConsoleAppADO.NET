using Models;

namespace Presentation.Interfaces
{
    public interface ILocationPropertyEntryManager
    {
        public Task<int> ChooseLocation();
    }
}
