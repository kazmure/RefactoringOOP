using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject;


namespace UnitTestProject
{
    [TestClass]
    public class ReservationManagerTests
    {
        [TestMethod]
        public void ReserveTable_SuccessfulReservation_ReturnsTrue()
        {
            // Arrange
            ReservationManager reservationManager = new ReservationManager();
            reservationManager.AddRestaurant("A", 10);

            // Act
            bool result = reservationManager.ReserveTable("A", new DateTime(2023, 12, 25), 3);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReserveTable_DuplicateReservation_ReturnsFalse()
        {
            // Arrange
            ReservationManager reservationManager = new ReservationManager();
            reservationManager.AddRestaurant("A", 10);

            // Act
            bool firstReservationResult = reservationManager.ReserveTable("A", new DateTime(2023, 12, 25), 3);
            bool duplicateReservationResult = reservationManager.ReserveTable("A", new DateTime(2023, 12, 25), 3);

            // Assert
            Assert.IsTrue(firstReservationResult);
            Assert.IsFalse(duplicateReservationResult);
        }

        [TestMethod]
        public void ReserveTable_InvalidRestaurant_ReturnsFalse()
        {
            // Arrange
            ReservationManager reservationManager = new ReservationManager();

            // Act
            bool result = reservationManager.ReserveTable("InvalidRestaurant", new DateTime(2023, 12, 25), 3);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
