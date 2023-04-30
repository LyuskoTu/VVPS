using MVC_TU;
using MVC_TU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVPS_ex.Services
{
    public class TicketService
    {
        private const decimal BasePrice = 100;
        private const decimal DefaultDiscount = 0.95m; // 5% discount
        private const decimal ElderlyDiscount = 0.66m; // 34% discount
        private const decimal ChildDiscount = 0.5m; // 50% discount
        private const decimal FamilyCardDiscount = 0.9m; // 10% discount

        private static readonly TimeSpan MorningPeakStart = new(7, 30, 0);
        private static readonly TimeSpan MorningPeakEnd = new(9, 30, 0);
        private static readonly TimeSpan AfternoonPeakStart = new(16, 0, 0);
        private static readonly TimeSpan AfternoonPeakEnd = new(19, 30, 0);

        public decimal CalculatePrice(string from, string to, DateTime departureTime, bool isReturnTrip, bool hasElderlyPass, bool hasChild, bool hasFamilyCard)
        {
            if(hasElderlyPass && hasFamilyCard)
            {
                throw new Exception("The passenger can only have one type of card");
            }
            decimal price = BasePrice;

            if (!IsPeakHour(departureTime))
            {
                price *= DefaultDiscount;
            }

            if (hasElderlyPass)
            {
                price *= ElderlyDiscount;
            }
            else if (hasChild && hasFamilyCard)
            {
                price *= ChildDiscount;
            }
            else if (!hasChild && hasFamilyCard)
            {
                price *= FamilyCardDiscount;
            }

            if (isReturnTrip)
            {
                price *= 2;
            }

            return price;
        }
        
        private bool IsPeakHour(DateTime travelTime)
        {
            TimeSpan timeOfDay = travelTime.TimeOfDay;
            return (timeOfDay >= MorningPeakStart && timeOfDay <= MorningPeakEnd) || (timeOfDay >= AfternoonPeakStart && timeOfDay <= AfternoonPeakEnd);
        }
    }
}
