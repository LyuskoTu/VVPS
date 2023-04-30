
namespace MVCTest.Controller
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using MVC_TU;
    using MVC_TU.Core;
    using MVC_TU.Core.Interface;
    using MVCTest.Model;
    using static MVCTest.TextsExtended;
    [Authenticated]
    [MenuName(TRAINS_MENU_ITEM)]
    public class TrainController : BaseController
    {
        public readonly AppDbContext _db;
        public TrainController()
        {
            _db = DbContextFactory<AppDbContext>.Instance;
        }

        [Authenticated]
        [MenuName(TRAINS)]
        public IView View()
        {
            return View(_db);
        }

        [Authenticated(true)]
        [MenuName(ADD_TRAIN)]
        public IView Add()
        {
            return Add<Train>(_db);
        }

        [Authenticated(true)]
        [MenuName(UPDATE_TRAIN)]
        public IView Update()
        {
            return Update<Train>(_db);
        }

        [Authenticated(true)]
        [MenuName(DELETE_TRAIN)]
        public IView Dalete()
        {
            return Delete<Train>(_db);
        }

        [Authenticated(true)]
        [MenuName(GET_ALL_TRAINS)]
        public IView GetAll()
        {
            return GetAll<Train>(_db);
        }
    }
}

