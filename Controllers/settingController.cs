
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class settingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        settingService _service;
        public settingController(IWebHostEnvironment env)
        {
            _service = new settingService();
            _env = env;
        }

        [HttpGet("all")]
        public List<SettingBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public SettingBLL Get(int id )
        {
            return _service.Get(id );
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] SettingBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] SettingBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] SettingBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
