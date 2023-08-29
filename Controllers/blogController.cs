using System;
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class BlogController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        blogService _service;
      
        public BlogController(IWebHostEnvironment env)
        {
            _service = new blogService();
            _env = env;
        }
        [HttpGet("all/{fromDate}/{toDate}")]
        public List<BlogBLL> GetAll(string fromDate, string toDate)
        {
            return _service.GetAll(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
        }
        [HttpGet("allCountry")]
        public List<CountryBLL> GetAllCountries()
        {
            return _service.GetAllCountry();
        }
        [HttpGet("allCity/{code}")]
        public List<CityBLL> GetAllCitiess(string code)
        {
            return _service.GetAllCity(code);
        }
        [HttpGet("{id}")]
        public BlogBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet("images/{id}")]
        public List<string> GetImages(int id)
        {
            return _service.GetItemImages(id);
        }

        [HttpPost]
        [Route("insert")]   
        public int Post([FromBody]BlogBLL obj)
        {
           return _service.Insert(obj, _env);        
        }
        [HttpPost]
        [Route("update")]
        public int Update([FromBody]BlogBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]OrdersBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
