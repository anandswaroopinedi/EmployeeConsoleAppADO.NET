using Presentation.Interfaces;

namespace Presentation.Services;

public class RolePropertyEntryManager : IRolePropertyEntryManager
{
    public string GetDescription()
    {
        string description = "";
        Console.WriteLine("Description:");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Upload Later");
        Console.WriteLine("2. Enter Description");
        Console.Write("Choose from above options:");
        int.TryParse(Console.ReadLine(), out int option);
        switch (option)
        {
            case 0: return "Abort";
            case 1:
                description = "NONE";
                return description;
            case 2:
                Console.Write("Enter the Description:");
                description = Console.ReadLine();
                if (description == "")
                {
                    return GetDescription();
                }
                else
                {
                    return description;
                }

            default:
                Console.WriteLine("Select option from the above list only");
                return GetDescription();
        }
    }
}
