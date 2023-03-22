
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class reviewsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        reviewsService _service;
        public reviewsController(IWebHostEnvironment env)
        {
            _service = new reviewsService();
            _env = env;
        }

        [HttpGet("all")]
        public List<ReviewsBLL> GetAll()
        {
            return _service.GetAll();
        }

    }
}
