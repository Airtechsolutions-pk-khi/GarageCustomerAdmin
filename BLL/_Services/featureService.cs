﻿using BAL.Repositories;
using GarageCustomerAdmin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin.BLL._Services
{
    public class featureService : baseService
    {
        featureDB _service;
        public featureService()
        {
            _service = new featureDB();
        }

        public List<FeatureBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<FeatureBLL>();
            }
        }
        
        public FeatureBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(FeatureBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Features", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(FeatureBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Features", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(FeatureBLL data)
        {
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
                data.StatusID = 3;
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
