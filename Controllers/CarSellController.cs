
using System;
using System.Collections.Generic;
using GarageCustomerAdmin._Models;
using GarageCustomerAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class CarSellController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        carSellService _service;
      
        public CarSellController(IWebHostEnvironment env)
        {
            _service = new carSellService();
            _env = env;
        }


        [HttpGet("all/{fromDate}/{toDate}")]
        public List<CarSellBLL> GetAll(string fromDate, string toDate)
        {
            return _service.GetAll(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
        }
        [HttpGet("allMake")]
        public List<MakeBLL> GetAllMakes()
        {
            return _service.GetAllMake();
        }
        [HttpGet("allModel")]
        public List<ModelBLL> GetAllModels()
        {
            return _service.GetAllModel();
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
        public CarSellBLL Get(int id)
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
        public int Post([FromBody]CarSellBLL obj)
        {

            return _service.Insert(obj, _env);
            
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] CarSellBLL obj)
        {
            return _service.Update(obj, _env);
        }
        [HttpPost]
        [Route("updateorderstatus")]
        public int PostUpdateStatus([FromBody] CarSellBLL obj)
        {
            return _service.UpdateStatus(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]OrdersBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
