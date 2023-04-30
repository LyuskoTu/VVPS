using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVC_TU.Core;
using MVC_TU.Model;

namespace MVCTest.Model
{
	public class Train : BaseModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		// TODO change to Date maybe
		public string Departure { get; set; }
		public decimal BasePrice { get; set; }	
    }
}

