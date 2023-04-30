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
        [InlineData("Sofia", "Plovdiv", "2023-04-25T07:35:00", false, false, false, false, 100)] // Peak hour, no discounts
        [InlineData("Sofia", "Plovdiv", "2023-05-12T11:30:00", false, false, false, false, 95)] // Off-peak hour, no discounts, first interval
        [InlineData("Sofia", "Plovdiv", "2023-05-15T23:50:00", false, false, false, false, 95)] // Off-peak hour, no discounts, second interval
        [InlineData("Sofia", "Plovdiv", "2023-04-06T08:30:00", false, true, false, false, 66)] // Peak hour, elderly discount
        [InlineData("Sofia", "Plovdiv", "2023-04-06T08:30:00", false, false, true, false, 100)] // Peak hour, has child, no family card
        [InlineData("Sofia", "Plovdiv", "2023-04-06T08:00:00", false, false, true, true, 50)] // Peak hour, has child, no family card
        [InlineData("Sofia", "Plovdiv", "2023-04-06T08:00:00", true, true, false, false, 132)] // Peak hour, elderly pass, return trip
        [InlineData("Sofia", "Plovdiv", "2023-05-05T23:18:00", true, false, false, false, 190)] // Off-peak hour, no discounts, return trip, second interval
        [InlineData("Sofia", "Plovdiv", "2023-05-05T10:20:00", true, false, false, false, 190)] // Off-peak hour, no discounts, return trip, first interval
        [InlineData("Sofia", "Plovdiv", "2023-04-06T07:35:00", true, false, false, false, 200)] // Peak hour, no discounts, return trip
        [InlineData("Sofia", "Plovdiv", "2023-04-06T07:35:00", true, false, true, false, 200)] // Peak hour, no discounts, return trip, has child
        [InlineData("Sofia", "Plovdiv", "2023-04-06T07:50:00", true, false, true, true, 100)] // Peak hour, return trip, has child, has family card
        [InlineData("Sofia", "Plovdiv", "2023-04-06T11:35:00", false, false, true, true, 47.5)] // Off-peak hour, has child , has family card
        [InlineData("Sofia", "Plovdiv", "2023-04-06T10:35:00", false, false, false, true, 85.5)] // Off-peak hour, has family card
        [InlineData("Sofia", "Plovdiv", "2023-04-06T10:35:00", false, true, false, false, 62.7)] // Off-peak hour, has elderly pass

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

