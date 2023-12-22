//Maybe used
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using AddRestaurants;
using Tables;

//Use now
using Serilog;
using ListRestaurant;

namespace UnitTestProject
{
    public class TableReservationApp
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();

            ReservationManager m = new ReservationManager();
            try
            {
                m.AddRestaurant("A", 10);
                m.AddRestaurant("B", 5);
                Log.Information("{Result}",(m.ReserveTable("A", new DateTime(2023, 12, 25), 
                    3))); // True
                Log.Information("{Result}",(m.ReserveTable("A", new DateTime(2023, 12, 25), 
                    3))); // False
            }

            catch (Exception ex)
            {
                Log.Error(ex, "Erorr");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}