using BusinessLogicLayer.Interfaces;
using Models;
using Presentation.Interfaces;

namespace LocationManagementLibrary
{
    public class LocationPropertyEntryManager : ILocationPropertyEntryManager
    {
        private readonly ILocationManager _locationManager;
        public LocationPropertyEntryManager(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        public static void DisplayLocationList(List<Location> locationList)
        {

            for (int i = 0; i < locationList.Count; i++)
            {
                Console.WriteLine($"{i + 2}.  {locationList[i].Name}");
            }
        }
        public async Task<int> ChooseLocation()
        {
            List<Location> locationList =await _locationManager.GetAll();
            Console.WriteLine("Locations:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Add New Location");
            DisplayLocationList(locationList);
            Console.Write("Choose Location from above options:");
            int option;
            int.TryParse(Console.ReadLine(), out option);
            if (option == 0)
            {
                return 0;
            }
            if (option == 1)
            {
                Console.Write("Enter new Location:");
                string location = Console.ReadLine().ToUpper();
                Location locationModel = new Location();
                locationModel.Name = location;
                if (await _locationManager.AddLocation(locationModel))
                {
                    Console.WriteLine("Location Added Successfully");
                    return locationModel.Id;
                }
                else
                {
                    Console.WriteLine("Entered Location is previously exists in the database.");
                    return await ChooseLocation();
                }

            }
            if (option > 1 && option <= locationList.Count + 1)
            {
                return locationList[option - 2].Id;
            }
            else
            {
                Console.WriteLine("Select option from the above list only");
                return await ChooseLocation();
            }
        }
    }
}
