
using System.Collections.Generic;
using BAL.Repositories;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]

    public class bodyTypeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        BodyTypeService _service;
        public bodyTypeController(IWebHostEnvironment env)
        {
            _service = new BodyTypeService();
            _env = env;
        }

        [HttpGet("all")]
        public List<BodyTypeBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}/brand")]
        public BodyTypeBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]BodyTypeBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]BodyTypeBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]BodyTypeBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
