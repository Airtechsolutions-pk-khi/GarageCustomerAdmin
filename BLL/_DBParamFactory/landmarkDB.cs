

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

    public class landmarkDB : baseDB
    {
        public static LandmarkBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public landmarkDB()
           : base()
        {
            repo = new LandmarkBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<LandmarkBLL> GetAll()
        {
            try
            {
                var lst = new List<LandmarkBLL>();
                //SqlParameter[] p = new SqlParameter[1];
               

                _dt = (new DBHelper().GetTableFromSP)("sp_GetLandmark");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<LandmarkBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LandmarkBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LandmarkBLL Get(int id)
        {
            try
            {
                var _obj = new LandmarkBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetLandmarkByID", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<LandmarkBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LandmarkBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(LandmarkBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@Name", data.Name);                
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
             
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertLandmark", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LandmarkBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@LandmarkID", data.LandmarkID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateLandmark_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LandmarkBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.LandmarkID);
                //p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteLandmark", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
