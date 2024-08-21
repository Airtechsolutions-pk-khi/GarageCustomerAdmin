
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class locationController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        locationService _service;
        public locationController(IWebHostEnvironment env)
        {
            _service = new locationService();
            _env = env;
        }

        [HttpGet("all")]
        public List<LocationBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("images/{id}")]
        public List<string> GetImages(int id)
        {
            return _service.GetLocationImages(id);
        }
		[HttpGet("{id}")]
        public LocationBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insertlocation")]
        public int Post([FromBody]LocationBLL obj)
        {
            return _service.Insert(obj);
        }
        [HttpPost]
        [Route("add")]
        public int PostUpdate([FromBody] LocationBLL1 obj)
        {
            //return _service.Update(obj, _env);
            return 1;
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]LocationBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
