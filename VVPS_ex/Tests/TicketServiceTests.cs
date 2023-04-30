using System;
using Xunit;
using MVC_TU.Core.Interface;
using VVPS_ex.Services;

namespace MVCTest.Tests
{
	public class TicketServiceTests
	{
        private readonly TicketService _ticketService;

        public TicketServiceTests()
        {
            _ticketService = new TicketService();
        }

        [Theory]
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", false, false, false, false, 95)] // Off-peak hour, no discounts
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", true, false, false, false, 190)] // Off-peak hour, no discounts, return trip
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", false, true, false, false, 62.7)] // Off-peak hour, elderly pass
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", false, false, true, true, 47.5)] // Off-peak hour, family card and child
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", false, false, false, true, 85.5)] // Off-peak hour, family card
        [InlineData("Sofia", "Plovdiv", "2023-05-01T07:00:00", false, false, true, false, 95)] // Off-peak hour, child
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:45:00", false, false, false, false, 100)] // Morning peak hour
        [InlineData("Sofia", "Plovdiv", "2023-05-01T18:30:00", false, false, false, false, 100)] // Afternoon peak hour
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", false, false, false, false, 100)] // peak hour, no discounts
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", true, false, false, false, 200)] // peak hour, no discounts, return trip
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", false, true, false, false, 66)] // peak hour, elderly pass
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", false, false, true, true, 50)] // peak hour, family card and child
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", false, false, false, true, 90)] // peak hour, family card
        [InlineData("Sofia", "Plovdiv", "2023-05-01T08:00:00", false, false, true, false, 100)] // peak hour, child
        public void CalculatePrice_ReturnsCorrectPrice(string from, string to, string departureTimeStr, bool isReturnTrip, bool hasElderlyPass, bool hasChild, bool hasFamilyCard, decimal expectedPrice)
        {
            DateTime departureTime = DateTime.Parse(departureTimeStr);

            decimal actualPrice = _ticketService.CalculatePrice(from, to, departureTime, isReturnTrip, hasElderlyPass, hasChild, hasFamilyCard);

            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void CalculatePrice_ThrowsException_WhenPassengerHasBothElderlyPassAndFamilyCard()
        {
            string from = "Sofia";
            string to = "Plovdiv";
            DateTime departureTime = DateTime.Parse("2023-05-01T08:00:00");
            bool isReturnTrip = false;
            bool hasElderlyPass = true;
            bool hasChild = false;
            bool hasFamilyCard = true;

            Assert.Throws<Exception>(() => _ticketService.CalculatePrice(from, to, departureTime, isReturnTrip, hasElderlyPass, hasChild, hasFamilyCard));
        }
    }
}

