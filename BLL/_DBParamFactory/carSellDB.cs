

using GarageCustomerAdmin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class carSellDB : baseDB
    {
        public static CarSellBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public carSellDB()
           : base()
        {
            repo = new CarSellBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<CarSellBLL2> GetAll(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<CarSellBLL2>();
                SqlParameter[] p = new SqlParameter[2];

                p[0] = new SqlParameter("@fromdate", FromDate.Date);
                p[1] = new SqlParameter("@todate", ToDate.Date);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetCarSell_CADMIN", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarSellBLL2>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<MakeBLL> GetAllMake()
        {
            try
            {
                var lst = new List<MakeBLL>();
                //SqlParameter[] p = new SqlParameter[1];


                _dt = (new DBHelper().GetTableFromSP)("sp_GetMakes");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<MakeBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MakeBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ModelBLL> GetAllModel(int MakeID)
        {
            try
            {
                var lst = new List<ModelBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@MakeID", MakeID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetModels", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<ModelBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ModelBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<ModelBLL>();
            }
        }
        public List<CountryBLL> GetAllCountries()
        {
            try
            {
                var lst = new List<CountryBLL>();
                //SqlParameter[] p = new SqlParameter[1];


                _dt = (new DBHelper().GetTableFromSP)("sp_GetCountries");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<ModelBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CountryBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<CountryBLL>();
            }
        }
        public List<CityBLL> GetAllCities(string code)
        {
            try
            {
                var lst = new List<CityBLL>();
                SqlParameter[] p = new SqlParameter[1];

                p[0] = new SqlParameter("@Code", code);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetCities", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<ModelBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CityBLL>>();
                    }
                }
                return lst;
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
                var _obj = new CarSellBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);


                _dt = (new DBHelper().GetTableFromSP)("sp_GetCarSellById_CADMIN", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarSellBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetStatus(int id)
        {
            try
            {
                var _obj = new CarSellBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetCarsellbyID2_Admin", p);

                return _ds;
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
                var _obj = new CarSellBLL();
                List<string> ImagesSource = new List<string>();
                _dt = new DataTable();
                SqlParameter[] p1 = new SqlParameter[1];
                p1[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetCarSellImages_CAdmin", p1);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj.CarSellImages = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarSellImageBLL>>();

                        for (int i = 0; i < _obj.CarSellImages.Count; i++)
                        {
                            ImagesSource.Add(_obj.CarSellImages[i].Image);
                        }
                    }
                }

                return ImagesSource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Update(CarSellBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[23];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@RegistrationNo", data.RegistrationNo);
                p[3] = new SqlParameter("@BodyTypeID", data.BodyTypeID);
                p[4] = new SqlParameter("@FuelType", data.FuelType);
                p[5] = new SqlParameter("@EngineType", data.EngineType);
                p[6] = new SqlParameter("@Year", data.Year);
                p[7] = new SqlParameter("@CustomerID", data.CustomerID);
                p[8] = new SqlParameter("@MakeID", data.MakeID);
                p[9] = new SqlParameter("@ModelID", data.ModelID);
                p[10] = new SqlParameter("@Transmition", data.Transmition);
                p[11] = new SqlParameter("@Kilometer", data.Kilometer);
                p[12] = new SqlParameter("@Price", data.Price);
                p[13] = new SqlParameter("@IsInspected", data.IsInspected);
                p[14] = new SqlParameter("@CountryCode", data.CountryCode);
                p[15] = new SqlParameter("@CityID", data.CityID);
                p[16] = new SqlParameter("@Address", data.Address);
                p[17] = new SqlParameter("@CarSellAddID", data.CarSellAddID);
                p[18] = new SqlParameter("@BodyColor", data.BodyColor);
                p[19] = new SqlParameter("@Assembly", data.Assembly);
                p[20] = new SqlParameter("@CreatedDate", DateTime.Now);
                p[21] = new SqlParameter("@CreatedBy", 1);
                p[22] = new SqlParameter("@CarSellID", data.CarSellID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateCarSell", p);

                //rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_UpdateCarSell", p).Rows[0]["CarSellID"].ToString());

                if (data.Features != "" && data.Features != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@CarsellID", data.CarSellID);
                    p1[1] = new SqlParameter("@Features", data.Features);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertICarsellFeatures_Admin", p1);
                }
                try
                {
                    var imgStr = String.Join(",", data.CarSellImages.Select(p => p.Image));
                    SqlParameter[] p2 = new SqlParameter[2];
                    p2[0] = new SqlParameter("@Images", imgStr);
                    p2[1] = new SqlParameter("@CarsellID", data.CarSellID);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertCarsellImages_CAdmin", p2);
                }
                catch (Exception ex)
                { return 0; }

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateStatus(CarSellBLL2 data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@StatusID", data.StatusID);
                p[2] = new SqlParameter("@Reason", data.Reason);
                p[1] = new SqlParameter("@CarSellID", data.CarSellID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateCarSellStatus_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Insert(CarSellBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[23];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@RegistrationNo", data.RegistrationNo);
                p[3] = new SqlParameter("@BodyType", data.BodyType);
                p[4] = new SqlParameter("@BodyTypeID", data.BodyTypeID);
                p[5] = new SqlParameter("@FuelType", data.FuelType);
                p[6] = new SqlParameter("@EngineType", data.EngineType);
                p[7] = new SqlParameter("@Year", data.Year);
                p[8] = new SqlParameter("@CustomerID", data.CustomerID);
                p[9] = new SqlParameter("@MakeID", data.MakeID);
                p[10] = new SqlParameter("@ModelID", data.ModelID);
                p[11] = new SqlParameter("@Transmition", data.Transmition);
                p[12] = new SqlParameter("@Kilometer", data.Kilometer);
                p[13] = new SqlParameter("@Price", data.Price);
                p[14] = new SqlParameter("@IsInspected", data.IsInspected);
                p[15] = new SqlParameter("@CityID", data.CityID);
                p[16] = new SqlParameter("@CountryCode", data.CountryCode);
                p[17] = new SqlParameter("@Address", data.Address);
                p[18] = new SqlParameter("@CarSellAddID", data.CarSellAddID);
                p[19] = new SqlParameter("@BodyColor", data.BodyColor);
                p[20] = new SqlParameter("@Assembly", data.Assembly);
                p[21] = new SqlParameter("@CreatedDate", DateTime.Now);
                p[22] = new SqlParameter("@CreatedBy", 1);

                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_InsertCarSell", p).Rows[0]["CarSellID"].ToString());

                if (data.Features != "" && data.Features != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@CarsellID", rtn);
                    p1[1] = new SqlParameter("@Features", data.Features);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertICarsellFeatures_Admin", p1);
                }
                try
                {
                    var imgStr = String.Join(",", data.CarSellImages.Select(p => p.Image));
                    SqlParameter[] p2 = new SqlParameter[2];
                    p2[0] = new SqlParameter("@Images", imgStr);
                    p2[1] = new SqlParameter("@CarsellID", rtn);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertCarsellImages_CAdmin", p2);
                }
                catch (Exception ex)
                { return 0; }

                return rtn;
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
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.OrderID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeleteOrders", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
