

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

    public class featureDB : baseDB
    {
        public static FeatureBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public featureDB()
           : base()
        {
            repo = new FeatureBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<FeatureBLL> GetAll()
        {
            try
            {
                var lst = new List<FeatureBLL>();
                //SqlParameter[] p = new SqlParameter[1];
               

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetFeatures");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<FeatureBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FeatureBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AmenitiesBLL Get(int id)
        {
            try
            {
                var _obj = new AmenitiesBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetAmenitiesByID", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<AmenitiesBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AmenitiesBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(AmenitiesBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@Name", data.Name);                
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
             
                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("dbo.sp_InsertAmenities", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(AmenitiesBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@AmenitiesID", data.AmenitiesID);

                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("dbo.sp_updateAmenities_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(AmenitiesBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.AmenitiesID);
                //p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeleteAmenities", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
