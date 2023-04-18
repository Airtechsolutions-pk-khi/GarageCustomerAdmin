

using GarageCustomerAdmin._Models;
using Microsoft.AspNetCore.Hosting;
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

    public class locationDB : baseDB
    {
        public static LocationBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public locationDB()
           : base()
        {
            repo = new LocationBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<LocationBLL> GetAll()
        {
            try
            {
                var lst = new List<LocationBLL>();
                SqlParameter[] p = new SqlParameter[0];
                

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_getLocation_CADMIN", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<LocationBLL>();

                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LocationBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<string> GetLocationImages(int id)
        {

            try
            {
                var _obj = new LocationBLL();
                List<string> ImagesSource = new List<string>();
                _dt = new DataTable();
                SqlParameter[] p1 = new SqlParameter[1];
                p1[0] = new SqlParameter("@id", id);
                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetLocationImages_CAdmin", p1);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj.LocationImages = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LocationimagesBLL>>();

                        for (int i = 0; i < _obj.LocationImages.Count; i++)
                        {
                            ImagesSource.Add(_obj.LocationImages[i].ImageURL);
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
        public LocationBLL Get(int id)
        {
            try
            {
                var _obj = new LocationBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetLocationsByID_CADMIN", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<LocationBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LocationBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
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
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[24];

                p[0] = new SqlParameter("@Name", data.Name);
                p[0] = new SqlParameter("@ArabicName", data.ArabicName);
                p[1] = new SqlParameter("@Description", data.Descripiton);
                p[1] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
                p[2] = new SqlParameter("@Address", data.Address);
                p[2] = new SqlParameter("@ArabicAddress", data.ArabicAddress);
                p[3] = new SqlParameter("@ContactNo", data.ContactNo);
                p[4] = new SqlParameter("@Email", data.Email);
                //p[5] = new SqlParameter("@TimeZoneID", data.TimeZoneID);
                //p[6] = new SqlParameter("@CountryID", data.CountryID);
                //p[7] = new SqlParameter("@LicenseID", data.LicenseID);
                //p[8] = new SqlParameter("@CityID", data.CityID);
                //p[9] = new SqlParameter("@UserID", data.UserID);                
                //p[10] = new SqlParameter("@Longitude", data.Longitude);
                //p[11] = new SqlParameter("@Latitude", data.Latitude);
                //p[12] = new SqlParameter("@DeliveryServices", data.DeliveryServices);
                //p[13] = new SqlParameter("@DeliveryCharges", data.DeliveryCharges);
                //p[14] = new SqlParameter("@DeliveryTime", data.DeliveryTime);
                p[15] = new SqlParameter("@MinOrderAmount", data.MinOrderAmount);               
                p[16] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[17] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[18] = new SqlParameter("@StatusID", data.StatusID);
                //p[19] = new SqlParameter("@CompanyCode", data.CompanyCode);
                p[20] = new SqlParameter("@ImageURL", data.ImageURL);               
                //p[21] = new SqlParameter("@Opentime", data.Open_Time);
                //p[22] = new SqlParameter("@Closetime", data.Close_Time);
                p[23] = new SqlParameter("@LocationID", data.LocationID);

                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("dbo.sp_InsertLocation_CADMIN", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LocationBLL data)
        {           
            try
            {                
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[18];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Descripiton", data.Descripiton);
                p[2] = new SqlParameter("@Address", data.Address);
                p[3] = new SqlParameter("@ContactNo", data.ContactNo);
                p[4] = new SqlParameter("@Email", data.Email);
                p[5] = new SqlParameter("@Longitude", data.Longitude);
                p[6] = new SqlParameter("@Latitude", data.Latitude);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[8] = new SqlParameter("@LandmarkID", data.LandmarkID);
                p[9] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[10] = new SqlParameter("@StatusID", data.StatusID);
                p[11] = new SqlParameter("@IsFeatured", data.IsFeatured);                               
                p[12] = new SqlParameter("@LocationID", data.LocationID);
                p[13] = new SqlParameter("@GMapLink", data.Gmaplink);
                p[14] = new SqlParameter("@ArabicName", data.ArabicName);
                p[15] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
                p[16] = new SqlParameter("@ArabicAddress", data.ArabicAddress);
                p[17] = new SqlParameter("@CustomerStatusID", data.CustomerStatusID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateLocation_CADMIN", p);

                if (data.Amenities != "")
                {
                    SqlParameter[] p1 = new SqlParameter[3];

                    p1[0] = new SqlParameter("@Amenities", data.Amenities == "" ? null : data.Amenities);
                    p1[1] = new SqlParameter("@LocationID", data.LocationID);
                    p1[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString());
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertLocationAmenities_CAdmin", p1);
                }
                if (data.Service != "")
                {
                    SqlParameter[] p1 = new SqlParameter[3];

                    p1[0] = new SqlParameter("@Service", data.Service == "" ? null : data.Service);
                    p1[1] = new SqlParameter("@LocationID", data.LocationID);
                    p1[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString());
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertLocationServices_CAdmin", p1);
                }
                //if (data.Landmark != null)
                //{
                //    SqlParameter[] p1 = new SqlParameter[3];

                //    p1[0] = new SqlParameter("@Landmark", data.Landmark == "" ? null : data.Landmark);

                //    p1[1] = new SqlParameter("@LocationID", data.LocationID);
                //    p1[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString());
                //    (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_insertLocationLandmark_CAdmin", p1);
                //}


                try
                {
                    var imgStr = String.Join(",", data.LocationImages.Select(p => p.ImageURL));
                    SqlParameter[] p3 = new SqlParameter[3];
                    p3[0] = new SqlParameter("@Images", imgStr);
                    p3[1] = new SqlParameter("@LocationID", data.LocationID);
                    p3[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString());
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertLocationImages_CAdmin", p3);
                }
                catch { }

                return rtn;
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
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.LocationID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeleteLocation_CADMIN", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
