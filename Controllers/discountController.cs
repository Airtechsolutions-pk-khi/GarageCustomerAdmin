
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class discountController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        discountService _service;
        public discountController(IWebHostEnvironment env)
        {
            _service = new discountService();
            _env = env;
        }

        [HttpGet("all")]
        public List<DiscountBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public DiscountBLL Get(int id )
        {
            return _service.Get(id );
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] DiscountBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] DiscountBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] DiscountBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
