
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class featureController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        featureService _service;
        public featureController(IWebHostEnvironment env)
        {
            _service = new featureService();
            _env = env;
        }

        [HttpGet("all")]
        public List<FeatureBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public AmenitiesBLL Get(int id )
        {
            return _service.Get(id );
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] AmenitiesBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] AmenitiesBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] AmenitiesBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
