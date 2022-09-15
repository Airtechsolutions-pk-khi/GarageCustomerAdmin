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
    public class locationService : baseService
    {
        private readonly IWebHostEnvironment _env;
        locationDB _service;
        public locationService()
        {
            _service = new locationDB();
        }

        public List<LocationBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<LocationBLL>();
            }
        }
        public List<string> GetLocationImages(int id)
        {
            try
            {
                return _service.GetLocationImages(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //here
        public LocationBLL Get(int id)
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
        public int Insert(LocationBLL data)
        {
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LocationBLL data)
        {
            List<LocationimagesBLL> imBLL = new List<LocationimagesBLL>();
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.ImageURL = UploadImage(img, "Location", _env);
                    }


                    imBLL.Add(new LocationimagesBLL
                    {
                        ImageURL = UploadImage(img, "Location", _env),
                        //UpdatedDate = DateTime.Now.ToString()
                    });

                }
                data.LocationImages = imBLL;
                var result = _service.Update(data );

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LocationBLL data)
        {
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
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
