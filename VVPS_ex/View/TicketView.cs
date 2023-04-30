using System;
using ConsoleTableExt;
using MVC_TU;
using MVC_TU.Core.Interface;

namespace VVPS_ex.View
{
    public class TicketView : IView
    {
        public TicketView(AppDbContext db)
        {
            ConsoleTableBuilder
                .From(db.TicketData.ToList())
                .ExportAndWriteLine();
        }
    }
}

