using System;
using ConsoleTableExt;
using MVC_TU;
using MVC_TU.Core.Interface;

namespace VVPS_ex.View
{
    public class TrainView : IView
    {
        public TrainView(AppDbContext db)
        {
            ConsoleTableBuilder
                .From(db.TrainData.ToList())
                .ExportAndWriteLine();
        }
    }
}

