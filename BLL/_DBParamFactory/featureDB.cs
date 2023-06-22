

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
                _dt = (new DBHelper().GetTableFromSP)("sp_GetFeatures");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
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

        public FeatureBLL Get(int id)
        {
            try
            {
                var _obj = new FeatureBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetFeatureByID_CAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FeatureBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(FeatureBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);                
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);                
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
             
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertFeatures_CAdmin", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(FeatureBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[5] = new SqlParameter("@FeatureID", data.FeatureID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateFeature_CAdmin", p);
                return rtn;
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
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.FeatureID);
                p[1] = new SqlParameter("@StatusID", data.StatusID);
                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_deleteFeature_CAdmin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
