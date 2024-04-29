using DataAccessLayer.Interface;
using BusinessLogicLayer.Interfaces;
using Models;

namespace BusinessLogicLayer.Managers
{
    public class LocationManager : ILocationManager
    {
        private readonly IDataOperations _dataOperations;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Location.json";
        public LocationManager(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;   
        }

        public async Task<bool> AddLocation(Location location)
        {
            List<Location> locationList =GetAll().Result;

            if (!CheckLocationExists(location.Name, locationList))
            {
                location.Id = locationList.Count + 1;
                locationList.Add(location);
                if(await _dataOperations.AddLocationToDb(location))
                {
                    return true;
                }
            }
                return false;
        }

        public async Task<List<Location>> GetAll()
        {
            return await _dataOperations.GetLocations();
        }
        public async  Task<string> GetLocationName(int id)
        {
            List<Location> locationList = await GetAll();
            for (int i = 0; i < locationList.Count; i++)
            {
                if (locationList[i].Id == id)
                {
                    return locationList[i].Name;
                }
            }
            return "None";
        }
        public static bool CheckLocationExists(string loc, List<Location> locationList)
        {
            for (int i = 0; i < locationList.Count; i++)
            {
                if (locationList[i].Name == loc)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
