

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

    public class settingDB : baseDB
    {
        public static SettingBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public settingDB()
           : base()
        {
            repo = new SettingBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<SettingBLL> GetAll()
        {
            try
            {
                var lst = new List<SettingBLL>();
                //SqlParameter[] p = new SqlParameter[1];
               

                _dt = (new DBHelper().GetTableFromSP)("sp_GetSetting");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<SettingBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SettingBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SettingBLL Get(int id)
        {
            try
            {
                var _obj = new SettingBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetSettingByID", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<SettingBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SettingBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(SettingBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@PageName", data.PageName);
                p[3] = new SqlParameter("@Type", data.Type);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@AlternateImage", data.AlternateImage);
                p[6] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@ArabicTitle", data.ArabicTitle);
                p[9] = new SqlParameter("@ArabicDescription", data.ArabicDescription);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertSetting", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(SettingBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@PageName", data.PageName);
                p[3] = new SqlParameter("@Type", data.Type);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@AlternateImage", data.AlternateImage);
                p[6] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@ID", data.ID);
                p[9] = new SqlParameter("@ArabicTitle", data.ArabicTitle);
                p[10] = new SqlParameter("@ArabicDescription", data.ArabicDescription);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateSetting_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(SettingBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.ID);
                //p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteSetting", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
