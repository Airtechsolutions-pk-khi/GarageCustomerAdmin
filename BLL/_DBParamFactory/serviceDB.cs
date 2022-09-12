

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

    public class serviceDB : baseDB
    {
        public static ServiceBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public serviceDB()
           : base()
        {
            repo = new ServiceBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<ServiceBLL> GetAll()
        {
            try
            {
                var lst = new List<ServiceBLL>();
                //SqlParameter[] p = new SqlParameter[1];
               

                _dt = (new DBHelper().GetTableFromSP)("sp_GetService");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<ServiceBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ServiceBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ServiceBLL Get(int id)
        {
            try
            {
                var _obj = new ServiceBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetServiceByID", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<ServiceBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ServiceBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(ServiceBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@ServiceTitle", data.ServiceTitle);
                p[1] = new SqlParameter("@ServiceDescription", data.ServiceDescription);
                p[2] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@Type", data.Type);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertService", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(ServiceBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@ServiceTitle", data.ServiceTitle);
                p[1] = new SqlParameter("@ServiceDescription", data.ServiceDescription);
                p[2] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@ServiceID", data.ServiceID);
                p[6] = new SqlParameter("@Type", data.Type);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateService_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(ServiceBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.ServiceID);
                //p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteService", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
