using BAL.Repositories;
using GarageCustomerAdmin._Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GarageCustomerAdmin.BLL._Services
{
    public class carSellService : baseService
    {

        carSellDB _service;
        locationDB _serviceLocation;
        public carSellService()
        {
            _service = new carSellDB();
            _serviceLocation = new locationDB();
        }

        public List<CarSellBLL> GetAll(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetAll(FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<CarSellBLL>();
            }
        }
        public List<MakeBLL> GetAllMake()
        {
            try
            {
                return _service.GetAllMake();
            }
            catch (Exception ex)
            {
                return new List<MakeBLL>();
            }
        }
        public List<ModelBLL> GetAllModel()
        {
            try
            {
                return _service.GetAllModel();
            }
            catch (Exception ex)
            {
                return new List<ModelBLL>();
            }
        }
        public List<CountryBLL> GetAllCountry()
        {
            try
            {
                return _service.GetAllCountries();
            }
            catch (Exception ex)
            {
                return new List<CountryBLL>();
            }
        }
        public List<CityBLL> GetAllCity(string code)
        {
            try
            {
                return _service.GetAllCities(code);
            }
            catch (Exception ex)
            {
                return new List<CityBLL>();
            }
        }
        public CarSellBLL Get(int id)
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
        public RspCarSellDetail GetStatus(int id)
        {
            try
            {
                RspCarSellDetail rsp = new RspCarSellDetail();
                var customer = new CarSellCustomerBLL();
                var bll = new List<CarSellBLL>();
                var ds = _service.GetStatus(id);
                var _dsCarSell = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<CarSellBLL>>();
                var _dsCustomerData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<CarSellCustomerBLL>>();

                foreach (var i in _dsCarSell)
                {
                    bll.Add(new CarSellBLL
                    {
                        CarSellID = i.CarSellID,
                        CustomerID = i.CustomerID,
                        Name = i.Name,
                        Description = i.Description,
                        RegistrationNo = i.RegistrationNo,
                        BodyType = i.BodyType,
                        FuelType = i.FuelType,
                        EngineType = i.EngineType,
                        Kilometer = i.Kilometer,
                        Year = i.Year,
                        Transmition = i.Transmition,
                        Price = i.Price,
                        CountryCode = i.CountryCode,
                        Address = i.Address,
                        CarSellAddID = i.CarSellAddID,
                        Assembly = i.Assembly,
                        StatusID = i.StatusID,
                        Reason = i.Reason
                        ////    BodyColor = i.BodyColor
                    });

                    rsp.Carsell = bll.FirstOrDefault();
                    rsp.Customer = _dsCustomerData.Where(x => x.CustomerID == i.CustomerID).FirstOrDefault();
                }

                return rsp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<string> GetItemImages(int id)
        {
            try
            {
                return _service.GetItemImages(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(CarSellBLL data, IWebHostEnvironment _env)
        {
            try
            {
                List<CarSellImageBLL> imBLL = new List<CarSellImageBLL>();
                //data.Image = UploadImage(data.Image, "Orders", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.Image = UploadImage(img, "CarSell", _env);
                    }


                    imBLL.Add(new CarSellImageBLL
                    {
                        Image = UploadImage(img, "CarSell", _env),
                        //UpdatedDate = DateTime.Now.ToString()
                    });

                }
                data.CarSellImages = imBLL;
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CarSellBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
                List<CarSellImageBLL> imBLL = new List<CarSellImageBLL>();
                //data.Image = UploadImage(data.Image, "Orders", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.Image = UploadImage(img, "CarSell", _env);
                    }


                    imBLL.Add(new CarSellImageBLL
                    {
                        Image = UploadImage(img, "CarSell", _env),
                        //UpdatedDate = DateTime.Now.ToString()
                    });

                }
                data.CarSellImages = imBLL;
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateStatus(CarSellBLL data, IWebHostEnvironment _env)
        {
            try
            {

                var result = _service.UpdateStatus(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(OrdersBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
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
