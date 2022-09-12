
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class serviceController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        serviceService _service;
        public serviceController(IWebHostEnvironment env)
        {
            _service = new serviceService();
            _env = env;
        }

        [HttpGet("all")]
        public List<ServiceBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public ServiceBLL Get(int id )
        {
            return _service.Get(id );
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] ServiceBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] ServiceBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] ServiceBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
