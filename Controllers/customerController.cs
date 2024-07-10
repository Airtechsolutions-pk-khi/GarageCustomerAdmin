
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class customerController : ControllerBase
    {
        customerService _service;
        public customerController()
        {
            _service = new customerService();
        }

        [HttpGet("all/{brandid}")]
        public List<CustomerBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }

        [HttpGet("all")]
        public List<CustomerBLL> GetCustomer()
        {
            return _service.GetCustomer();
        }


        [HttpGet("{id}/brand/{brandid}")]
        public CustomerBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]CustomerBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]CustomerBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]CustomerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
