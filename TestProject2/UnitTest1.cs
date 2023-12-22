// Maybe used
using Xunit;
using System;
using UnitTestProject;
using NUnit;
using NUnit.Framework;
using System.IO;

// Use now
using ListRestaurant;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public class ReservationManagerTests
    {
        private ReservationManager _reservationManager;

        [SetUp]
        public void Setup()
        {
            _reservationManager = new ReservationManager();
        }

        [Test]
        public void AddRestaurant_ValidInput_Success()
        {
            string restaurantName = "TestRestaurant";
            int tableCount = 5;

            _reservationManager.AddRestaurant(restaurantName, tableCount);

            NUnit.Framework.Assert.AreEqual(1, _reservationManager.res.Count);
            NUnit.Framework.Assert.AreEqual(restaurantName, _reservationManager.
                res[0].Name);
            NUnit.Framework.Assert.AreEqual(tableCount, _reservationManager.res[0].
                Tables.Length);
        }   

        [Test]
        public void ReserveTable_ValidInput_Success()
        {
            _reservationManager.AddRestaurant("TestRestaurant", 5);

            bool reservationResult = _reservationManager.ReserveTable("TestRestaurant", 
                DateTime.Now, 3);

            NUnit.Framework.Assert.IsTrue(reservationResult);
        }

        [Test]
        public void ReserveTable_InvalidRestaurantName_ThrowsArgumentException()
        {
            NUnit.Framework.Assert.Throws<ArgumentException>(() => _reservationManager.
            ReserveTable("NonExistentRestaurant", DateTime.Now, 3));
        }

        [Test]
        public void GetAllFreeTables_ValidDate_ReturnsCorrectList()
        {
            _reservationManager.AddRestaurant("TestRestaurant", 4);
            _reservationManager.ReserveTable("TestRestaurant", DateTime.Now, 3);

            var freeTables = _reservationManager.GetAllFreeTables(DateTime.Now);

            NUnit.Framework.Assert.AreEqual(4, freeTables.Count);
        }

        [Test]
        public void SortRestAvail_ValidDate_Success()
        {
            _reservationManager.AddRestaurant("A", 5);
            _reservationManager.AddRestaurant("B", 10);
            _reservationManager.ReserveTable("A", DateTime.Now, 3);
            _reservationManager.ReserveTable("B", DateTime.Now, 5);

            _reservationManager.SortRestAvail(DateTime.Now);

            NUnit.Framework.Assert.AreEqual("B", _reservationManager.res[0].Name);
        }

    }
}

