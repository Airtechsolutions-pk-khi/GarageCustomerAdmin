﻿
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]

    public class dashboardController : ControllerBase
    {
        dashboardService _service;
        public dashboardController()
        {
            _service = new dashboardService();
        }
		[HttpGet("all")]
		public List<DashboardSummary> GetAll()
		{
			return _service.GetAll();
		}
		//[HttpGet("get/{LocationID}/{Date}")]
  //      public RspDashboard GetDashboardSummary(int LocationID, string Date)
  //      {
  //          return _service.GetDashboard(LocationID, System.DateTime.Now.Date.ToString());
  //      }

        //[HttpGet("range/get/{locationID}/{FDate}/{TDate}")]
        //public RspDashboard GetDashboardSummary(int LocationID, string FDate, string TDate)
        //{
        //    return _service.GetDashboardRange(LocationID,FDate, TDate);
        //}
    }
}
