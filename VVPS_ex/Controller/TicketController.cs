
namespace MVCTest.Controller
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using MVC_TU;
    using MVC_TU.Core;
    using MVC_TU.Core.Interface;
    using MVC_TU.Core.Translations;
    using MVCTest.Model;
    using Spectre.Console;
    using VVPS_ex.Services;
    using static MVCTest.TextsExtended;
    [Authenticated]
    [MenuName(TICKETS_MENU_ITEM)]
    public class TicketController : BaseController
    {
        public readonly AppDbContext _db;
        public readonly TicketService ticketService;
        public TicketController()
        {
            ticketService = new TicketService();
            _db = DbContextFactory<AppDbContext>.Instance;
        }

        [Authenticated]
        [MenuName(TICKETS)]
        public IView View()
        {
            return View(_db);
        }

        [Authenticated(true)]
        [MenuName(ADD_TICKET)]
        public IView Add()
        {
            Ticket val = new();
            List<string> list = _db.Properties<Ticket>();
            foreach (string item in list)
            {
                string val2 = AnsiConsole.Prompt(new TextPrompt<string>(Texts.Format(Texts.t("Add a value for [green3_1]{0}[/]?"), item)));
                AddValue(val, item, val2);
            }
            val.Price = ticketService.CalculatePrice(val.From, val.To, DateTime.Parse(val.DepartureTime), val.IsReturnTrip, val.HasElderlyPass, val.HasChild, val.HasFamilyCard);
            _db.Add(val);
            _db.SaveChanges();
            return new BaseView(@bool: true);
        }

        [Authenticated(true)]
        [MenuName(UPDATE_TICKET)]
        public IView Update()
        {
            return Update<Ticket>(_db);
        }

        [Authenticated(true)]
        [MenuName(DELETE_TICKET)]
        public IView Dalete()
        {
            return Delete<Ticket>(_db);
        }

        [Authenticated(true)]
        [MenuName(GET_ALL_TICKETS)]
        public IView GetAll()
        {
            return GetAll<Ticket>(_db);
        }

        private void AddValue(Ticket entry, string prop, object val)
        {
            PropertyInfo property = typeof(Ticket)!.GetProperty(prop);
            Type propertyType = property.PropertyType;
            object result;
            if (propertyType.IsEnum)
            {
                Enum.TryParse(propertyType, val.ToString(), out result);
            }
            else
            {
                result = Convert.ChangeType(val, propertyType);
            }

            property.SetValue(entry, result);
        }
    }
}

