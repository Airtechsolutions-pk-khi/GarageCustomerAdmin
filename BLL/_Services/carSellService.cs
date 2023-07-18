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

        public List<CarSellBLL2> GetAll(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetAll(FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<CarSellBLL2>();
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
        public List<ModelBLL> GetAllModel(int MakeID)
        {
            try
            {
                return _service.GetAllModel(MakeID);
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
                var make = new List<MakeBLL>();
                var model = new List<ModelBLL>();
                var country = new List<CountryBLL>();
                var city = new List<CityBLL>();
                var feature = new List<FeatureBLL>();
                var image = new List<CarSellImageBLL>();
                var ds = _service.GetStatus(id);
                var _dsCarSell = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<CarSellBLL>>();
                var _dsCustomerData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<CarSellCustomerBLL>>();
                var _dsMakeData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[2])).ToObject<List<MakeBLL>>();
                var _dsModelData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[3])).ToObject<List<ModelBLL>>();
                var _dsCountryData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[4])).ToObject<List<CountryBLL>>();
                var _dsCityData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[5])).ToObject<List<CityBLL>>();
                var _dsFeatureData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[6])).ToObject<List<FeatureBLL>>();
                var _dsImageData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[7])).ToObject<List<CarSellImageBLL>>();

                foreach (var i in _dsCarSell)
                {
                    bll.Add(new CarSellBLL
                    {
                        CarSellID = i.CarSellID,
                        CustomerID = i.CustomerID,
                        Name = i.Name,
                        Description = i.Description,
                        RegistrationNo = i.RegistrationNo,
                        //b = i.BodyType,
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
                        //Reason = i.Reason,
                        BodyColor = i.BodyColor,
                        CarFeature = i.CarFeature
                    });

                    rsp.Carsell = bll.FirstOrDefault();
                    rsp.Customer = _dsCustomerData.Where(x => x.CustomerID == i.CustomerID).FirstOrDefault();
                    rsp.Make = _dsMakeData.Where(x => x.MakeID == i.MakeID).FirstOrDefault();
                    rsp.Model = _dsModelData.Where(x => x.ModelID == i.ModelID).FirstOrDefault();
                    rsp.Country = _dsCountryData.Where(x => x.Code == i.CountryCode).FirstOrDefault();
                    rsp.City = _dsCityData.Where(x => x.ID == i.CityID).FirstOrDefault();
                    rsp.Feature = _dsFeatureData.ToList();
                    rsp.Image = _dsImageData.ToList();
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
                data.StatusID = 1;
                data.CarSellImages = imBLL;
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CarSellBLL3 data, IWebHostEnvironment _env)
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
        public int UpdateStatus(CarSellBLL2 data, IWebHostEnvironment _env)
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
