using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MVC_TU.Core;
using MVC_TU.Core.Interface;
using MVC_TU.Model;
using MVCTest.Model;

namespace MVC_TU
{
    public class AppDbContext : MyDbContext
    {
        public AppDbContext()
            : base()
        {
        }

        public DbSet<Train> TrainData { get; set; }
        public DbSet<Ticket> TicketData { get; set; }

    }
}