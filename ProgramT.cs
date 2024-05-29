using System;


public abstract class ToolVehicle
{
    // Properties common to all tool vehicles
    public int VehicleID { get; set; }
    public string RegNo { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal BasePrice { get; set; }
    public string VehicleType { get; set; }

    // Static properties to keep track of overall statistics
    public static int TotalVehicles { get; private set; }
    public static int TotalTaxPayingVehicles { get; private set; }
    public static int TotalNonTaxPayingVehicles { get; private set; }
    public static decimal TotalTaxCollected { get; private set; }

    // Constructor to increment the total number of vehicles
    public ToolVehicle()
    {
        TotalVehicles++;
    }

    // Abstract method to be implemented by derived classes to handle tax payment
    public abstract void PayTax();

    // Method to be called when a vehicle passes without paying tax
    public void PassWithoutPaying()
    {
        TotalNonTaxPayingVehicles++;
    }

    // Protected method to update tax statistics
    protected void UpdateTaxStats(decimal taxAmount)
    {
        TotalTaxCollected += taxAmount;
        TotalTaxPayingVehicles++;
    }
}

//Car Class

public class Car : ToolVehicle
{
    public Car()
    {
        VehicleType = "Car";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 2.00m;
        UpdateTaxStats(taxAmount);
    }
}
//Bike Class

public class Bike : ToolVehicle
{
    public Bike()
    {
        VehicleType = "Bike";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 1.00m;
        UpdateTaxStats(taxAmount);
    }
}
//HeavyVehicle Class

public class HeavyVehicle : ToolVehicle
{
    public HeavyVehicle()
    {
        VehicleType = "Heavy Vehicle";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 4.00m;
        UpdateTaxStats(taxAmount);
    }
}
//Main Program Class

public class Program
{
    // List to store all vehicles
    static List<ToolVehicle> vehicles = new List<ToolVehicle>();

    public static void Main()
    {
        bool running = true;

        // Main loop to display menu and handle user input
        while (running)
        {
            DisplayMenu();
            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddVehicle(new Car());
                    break;
                case 2:
                    AddVehicle(new Bike());
                    break;
                case 3:
                    AddVehicle(new HeavyVehicle());
                    break;
                case 4:
                    PassWithoutPaying();
                    break;
                case 5:
                    ShowTotalVehicles();
                    break;
                case 6:
                    ShowTotalTaxCollected();
                    break;
                case 7:
                    ShowTotalTaxPayingVehicles();
                    break;
                case 8:
                    ShowTotalNonTaxPayingVehicles();
                    break;
                case 9:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please enter a number between 1 and 9.");
                    break;
            }
        }
    }

    // Method to display the menu
    static void DisplayMenu()
    {
        Console.WriteLine("1. Add Car");
        Console.WriteLine("2. Add Bike");
        Console.WriteLine("3. Add Heavy Vehicle");
      //  Console.WriteLine("4. Pass without Paying");
        Console.WriteLine("5. Show Total Vehicles");
        Console.WriteLine("6. Show Total Tax Collected");
        Console.WriteLine("7. Show Total Tax Paying Vehicles");
        Console.WriteLine("8. Show Total Non-Tax Paying Vehicles");
        Console.WriteLine("9. Exit");
    }

    // Method to get user choice and handle invalid input
    static int GetUserChoice()
    {
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input, please enter a number between 1 and 9.");
            return -1;
        }
        return choice;
    }

    // Method to add a vehicle
    static void AddVehicle(ToolVehicle vehicle)
    {
        Console.WriteLine($"Enter Registration Number for {vehicle.VehicleType}:");
        vehicle.RegNo = Console.ReadLine();
        Console.WriteLine($"Enter Model for {vehicle.VehicleType}:");
        vehicle.Model = Console.ReadLine();
        Console.WriteLine($"Enter Brand for {vehicle.VehicleType}:");
        vehicle.Brand = Console.ReadLine();
        Console.WriteLine($"Enter Base Price for {vehicle.VehicleType}:");
        vehicle.BasePrice = Convert.ToDecimal(Console.ReadLine());

        vehicles.Add(vehicle);

        Console.WriteLine("Press 1 to pay tax, or 2 to pass without paying:");
        int choice = GetUserChoice();

        if (choice == 1)
        {
            vehicle.PayTax();
            Console.WriteLine($"{vehicle.VehicleType} has paid the tax.");
        }
        else if (choice == 2)
        {
            vehicle.PassWithoutPaying();
            Console.WriteLine($"{vehicle.VehicleType} passed without paying.");
        }
        else
        {
            Console.WriteLine("Invalid choice, please enter 1 or 2.");
        }
    }

    // Method to handle a vehicle passing without paying
    static void PassWithoutPaying()
    {
        Console.WriteLine("Enter Vehicle Type (Car, Bike, Heavy Vehicle):");
        string type = Console.ReadLine().ToLower();

        // Search for a vehicle of the specified type
        ToolVehicle vehicle = vehicles.Find(v => v.VehicleType.ToLower() == type);

        if (vehicle != null)
        {
            vehicle.PassWithoutPaying();
            Console.WriteLine($"{vehicle.VehicleType} passed without paying.");
        }
        else
        {
            Console.WriteLine("No such vehicle found.");
        }
    }

    // Method to show total vehicles
    static void ShowTotalVehicles()
    {
        Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
    }

    // Method to show total tax collected
    static void ShowTotalTaxCollected()
    {
        Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected:C}");
    }

    // Method to show total tax-paying vehicles
    static void ShowTotalTaxPayingVehicles()
    {
        Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
    }

    // Method to show total non-tax-paying vehicles
    static void ShowTotalNonTaxPayingVehicles()
    {
        Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
    }
}