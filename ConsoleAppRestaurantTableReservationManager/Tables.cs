//Maybe used
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject;
using ListRestaurant;
using AddRestaurants;

//Use now
using Serilog;


namespace Tables
{
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
                Log.Error(ex, "Error Book");
                return false;
            }
        }

        public bool IsBooked(DateTime d)
        {
            return BookedDates.Contains(d);
        }
    }
}
