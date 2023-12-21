
using System;
using System.Collections.Generic;

public class TableReservationApp
{
    static void Main(string[] args)
    {
        ReservationManager m = new ReservationManager();
        m.AddRestaurant("A", 10);
        m.AddRestaurant("B", 5);

        Console.WriteLine(m.ReserveTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(m.ReserveTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}

public class ReservationManager
{
    public List<Restaurant> res;

    public ReservationManager()
    {
        res = new List<Restaurant>();
    }

    public void AddRestaurant(string n, int t)
    {
        try
        {
            Restaurant r = new Restaurant();
            r.Name = n;
            r.Tables = new Table[t];
            for (int i = 0; i < t; i++)
            {
                r.Tables[i] = new Table();
            }
            res.Add(r);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    private void LoadRestaurantsFromFile(string fileP)
    {
        try
        {
            string[] ls = File.ReadAllLines(fileP);
            foreach (string l in ls)
            {
                var parts = l.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurant(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(l);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    public List<string> GetAllFreeTables(DateTime dt)
    {
        try
        { 
            List<string> free = new List<string>();
            foreach (var r in res)
            {
                for (int i = 0; i < r.Tables.Length; i++)
                {
                    if (!r.Tables[i].IsBooked(dt))
                    {
                        free.Add($"{r.Name} - Table {i + 1}");
                    }
                }
            }
            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return new List<string>();
        }
    }

    public bool ReserveTable(string rName, DateTime d, int tNumber)
    {
        foreach (var r in res)
        {
            if (r.Name == rName)
            {
                if (tNumber < 0 || tNumber >= r.Tables.Length)
                {
                    throw new Exception(null); //Invalid table number
                }

                return r.Tables[tNumber].Book(d);
            }
        }

        throw new Exception(null); //Restaurant not found
    }

    public void SortRestaurantsByAvailability(DateTime dt)
    {
        try
        { 
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < res.Count - 1; i++)
                {
                    int avTc = CountAvailableTablesForRestaurantAndDateTime(res[i], dt); // available tables current
                    int avTn = CountAvailableTablesForRestaurantAndDateTime(res[i + 1], dt); // available tables next

                    if (avTc < avTn)
                    {
                        var temp = res[i];
                        res[i] = res[i + 1];
                        res[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    public int CountAvailableTablesForRestaurantAndDateTime(Restaurant r, DateTime dt)
    {
        try
        {
            int count = 0;
            foreach (var t in r.Tables)
            {
                if (!t.IsBooked(dt))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return 0;
        }
    }
}

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
