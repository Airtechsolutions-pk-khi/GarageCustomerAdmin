﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class carsellViewModel
    {
    }
    public class RspCarSellDetail
    {
        public CarSellCustomerBLL Customer { get; set; }
        public CarSellBLL Carsell { get; set; }
        public MakeBLL Make { get; set; }
        public ModelBLL Model { get; set; }
        public CountryBLL Country { get; set; }
        public CityBLL City { get; set; }
        public List<FeatureBLL> Feature { get; set; }
        public List<CarSellImageBLL> Image { get; set; }
    }
    public class CarSellFeatures_Junc_BLL
    {
        public int CarsellID { get; set; }
        public int? FeatureID { get; set; }
    }
    public class Feature
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
    }
    public class CarSellImageBLL
    {
        public int ID { get; set; }
        public int? CarSellID { get; set; }
        public string Image { get; set; }

        public int? StatusID { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
    }
    public class CarSellBLL1
    {
        public int CarSellID { get; set; }
        public int? CustomerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public string BodyType { get; set; }
        public string FuelType { get; set; }
        public string EngineType { get; set; }
        public string Kilometer { get; set; }
        public string Year { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string Transmition { get; set; }
        public double Price { get; set; }
        public bool IsInspected { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public string Address { get; set; }
        public int? CarSellAddID { get; set; }
        public string BodyColor { get; set; }
        public string Assembly { get; set; }
        public string Reason { get; set; }
        public int? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string ImageSource { get; set; }

        public List<CarSellImageBLL> CarSellImages = new List<CarSellImageBLL>();

    }
    public class CarSellBLL3
    {
        public int CarSellID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerPhone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public int? BodyTypeID { get; set; }
        public string FuelType { get; set; }
        //public string BodyType { get; set; }
        public string EngineType { get; set; }
        public string Kilometer { get; set; }
        public string Year { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; }
        //public string MakeName { get; set; }
        //public string ModelName { get; set; }
        public string Transmition { get; set; }
        public double Price { get; set; }
        public bool IsInspected { get; set; }
        public int? CityID { get; set; }
        public string Features { get; set; }
        public string BodyColor { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public int? CarSellAddID { get; set; }
        public string Assembly { get; set; }
        public string Image { get; set; }
        //public int? StatusID { get; set; }
        //public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        ////public int? UpdatedBy { get; set; }
        public List<string> ImagesSource { get; set; }
        public List<CarSellImageBLL> CarSellImages = new List<CarSellImageBLL>();
    }

    public class CarSellBLL
    {
        public int CarSellID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerPhone { get; set; }
        public int? FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public int? BodyTypeID { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string EngineType { get; set; }
        public string Kilometer { get; set; }
        public string Year { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string Transmition { get; set; }
        public double Price { get; set; }
        public bool IsInspected { get; set; }
        public int? CityID { get; set; }
        public string Features { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public int? CarSellAddID { get; set; }
        public string BodyColor { get; set; }
        public string Assembly { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
        //public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public int? UpdatedBy { get; set; }
        public List<string> ImagesSource { get; set; }
        public List<CarSellImageBLL> CarSellImages = new List<CarSellImageBLL>();
        public List<CarSellFeatures_Junc_BLL> CarFeature = new List<CarSellFeatures_Junc_BLL>();
    }

    public class CarSellBLL2
    {
        public int CarSellID { get; set; }
        public int? CustomerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public int? BodyTypeID { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string EngineType { get; set; }
        public int? Kilometer { get; set; }
        public string Year { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string Transmition { get; set; }
        public double Price { get; set; }
        public bool IsInspected { get; set; }
        public int? CityID { get; set; }
        public string Features { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public int? CarSellAddID { get; set; }
        public string BodyColor { get; set; }
        public string Assembly { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
        public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public int? UpdatedBy { get; set; }
        public List<string> ImagesSource { get; set; }

        public List<CarSellImageBLL> CarSellImages = new List<CarSellImageBLL>();
    }
    public class MakeBLL
    {
        public int MakeID { get; set; }

        public string Name { get; set; }
    }
    public class ModelBLL
    {
        public int ModelID { get; set; }
        public int MakeID { get; set; }

        public string Name { get; set; }
    }
    public class CountryBLL
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class CityBLL
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
    public class CarSellCustomerBLL
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public int StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string Password { get; set; }
    }

    public class CarSell
    {
        public int CarSellID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerPhone { get; set; }
        public int? FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public int? BodyTypeID { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string EngineType { get; set; }
        public int Kilometer { get; set; }
        public string Year { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string Transmition { get; set; }
        public double Price { get; set; }
        public bool IsInspected { get; set; }
        public int? CityID { get; set; }
        public string Features { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public int? CarSellAddID { get; set; }
        public string BodyColor { get; set; }
        public string Assembly { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
        //public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public int? UpdatedBy { get; set; }
        public List<string> ImagesSource { get; set; }
        public List<CarSellImageBLL> CarSellImages = new List<CarSellImageBLL>();
        public List<CarSellFeatures_Junc_BLL> CarFeature = new List<CarSellFeatures_Junc_BLL>();
    }
}
