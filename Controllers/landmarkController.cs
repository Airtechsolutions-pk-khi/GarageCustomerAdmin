
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class landmarkController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        landmarkService _service;
        public landmarkController(IWebHostEnvironment env)
        {
            _service = new landmarkService();
            _env = env;
        }

        [HttpGet("all")]
        public List<LandmarkBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public LandmarkBLL Get(int id )
        {
            return _service.Get(id );
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] LandmarkBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] LandmarkBLL obj)
        {
            return _service.Update(obj, _env);
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
        public int PostDelete([FromBody] LandmarkBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
