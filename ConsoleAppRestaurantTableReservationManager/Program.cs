
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


public class TableReservationApp
{
    static void Main(string[] args)
    {
        ReservationManager m = new ReservationManager();
        try
        {
            m.AddRestaurant("A", 10);
            m.AddRestaurant("B", 5);

            Console.WriteLine(m.ReserveTable("A", new DateTime(2023, 12, 25), 3)); // True
            Console.WriteLine(m.ReserveTable("A", new DateTime(2023, 12, 25), 3)); // False
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erorr");
        }
    }
}

public class ReservationManager
{
    public List<Restaurant> res;

    public ReservationManager()
    {
        res = new List<Restaurant>();
    }

    public void AddRestaurant(string name, int tableCount)
    {
        try
        {
            Restaurant restaurant = new Restaurant { Name = name, Tables = Enumerable.Range(0, tableCount).Select(_ => new Table()).ToArray() };
            res.Add(restaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error Main");
        }
    }


    private void LoadRestaurantsFromFile(string filePath)
{
    try
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
            {
                AddRestaurant(parts[0], tableCount);
            }
            else
            {
                Console.WriteLine(line);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error AddRestaurant");
    }
}


    public List<string> GetAllFreeTables(DateTime date)
    {
        var freeTables = new List<string>();

        foreach (var restaurant in res)
        {
            for (int tableNumber = 0; tableNumber < restaurant.Tables.Length; tableNumber++)
            {
                if (!restaurant.Tables[tableNumber].IsBooked(date))
                {
                    freeTables.Add($"{restaurant.Name} - Table {tableNumber + 1}");
                }
            }
        }

        return freeTables;
    }


    public bool ReserveTable(string restaurantName, DateTime date, int tableNumber)
    {
        var restaurant = res.FirstOrDefault(r => r.Name == restaurantName);

        if (restaurant != null && tableNumber >= 0 && tableNumber < restaurant.Tables.Length)
        {
            return restaurant.Tables[tableNumber].Book(date);
        }

        throw new ArgumentException("Invalid restaurant name, table number, or restaurant not found");
    }


    public void SortRestaurantsByAvailability(DateTime date)
    {
        try
        {
            res = res.OrderByDescending(r => CountAvailableTablesForRestaurantAndDateTime(r, date)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error SortRestaurantsByAvailability");
        }
    }


    public int CountAvailableTablesForRestaurantAndDateTime(Restaurant restaurant, DateTime date)
{
    try
    {
        return restaurant.Tables.Count(table => !table.IsBooked(date));
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error");
        return 0;
    }
}

}


private readonly ILogger<ReservationManager> _logger;

public ReservationManager(ILogger<ReservationManager> logger)
{
    res = new List<Restaurant>();
    _logger = logger;
}

// Використання _logger замість Console.WriteLine у методах класу.

public class Restaurant
{
    public string Name;
    public Table[] Tables;
}

public class Table
{
    private List<DateTime> BookedDates;


    public Table()
    {
        BookedDates = new List<DateTime>();
    }

    public bool Book(DateTime d)
    {
        try
        { 
            if (BookedDates.Contains(d))
            {
                return false;
            }
            BookedDates.Add(d);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return false;
        }
    }

    public bool IsBooked(DateTime d)
    {
        return BookedDates.Contains(d);
    }
}
