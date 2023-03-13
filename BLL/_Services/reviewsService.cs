using BAL.Repositories;
using GarageCustomerAdmin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin.BLL._Services
{
    public class reviewsService : baseService
    {
        reviewsDB _service;
        public reviewsService()
        {
            _service = new reviewsDB();
        }

        public List<ReviewsBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<ReviewsBLL>();
            }
        }  
    }
}
