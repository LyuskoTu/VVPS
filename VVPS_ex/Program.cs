// See https://aka.ms/new-console-template for more information
using MVC_TU;
using MVC_TU.Core;

var db = DbContextFactory<AppDbContext>.SetDbContext(new AppDbContext());
Bootstrap.Init();
