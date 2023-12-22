//Maybe Used
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject;

//Use now
using AddRestaurants;
using Tables;
using Serilog;

namespace ListRestaurant
{
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
                    Restaurant restaurant = new Restaurant
                    {
                        Name = name,
                        Tables =
                        Enumerable.Range(0, tableCount).Select(_ => new Table()).ToArray()
                    };
                    res.Add(restaurant);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error Main");
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
                    Log.Error(ex, "Error LoadRestaurantsFromFile");
                }
            }


            public List<string> GetAllFreeTables(DateTime date)
            {
                var freeTables = new List<string>();

                foreach (var restaurant in res)
                {
                    for (int tableNumber = 0; tableNumber < restaurant.Tables.Length;
                        tableNumber++)
                    {
                        if (!restaurant.Tables[tableNumber].IsBooked(date))
                        {
                            freeTables.Add($"{restaurant.Name} - Table {tableNumber + 1}");
                        }
                    }
                }

                return freeTables;
            }

            public bool ReserveTable(string restaurantName, DateTime date,
                int tableNumber)
            {
                var restaurant = res.FirstOrDefault(r => r.Name == restaurantName);

                if (restaurant != null && tableNumber >= 0 && tableNumber <
                    restaurant.Tables.Length)
                {
                    return restaurant.Tables[tableNumber].Book(date);
                }

                throw new ArgumentException("Invalid restaurant name, table number, or restaurant not found");
            }


            public void SortRestAvail(DateTime date)
            {
                try
                {
                    res = res.OrderByDescending(r => CountAvailTable(r, date)).ToList();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error SortRestAvail");
                }
            }


            public int CountAvailTable(Restaurant restaurant, DateTime date)
            {
                try
                {
                    return restaurant.Tables.Count(table => !table.IsBooked(date));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error CountAvailTable");
                    return 0;
                }
            }

        }
    }
