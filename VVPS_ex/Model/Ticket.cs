using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVC_TU.Core;
using MVC_TU.Core.Entity;
using MVC_TU.Model;

namespace MVCTest.Model
{
    public class Ticket : BaseModel
    {
        [ReadOnly]
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureTime { get; set; }
        public bool IsReturnTrip { get; set; }  
        public bool HasElderlyPass { get; set; }
        public bool HasChild { get; set; }
        public bool HasFamilyCard { get; set; }
    }
}

