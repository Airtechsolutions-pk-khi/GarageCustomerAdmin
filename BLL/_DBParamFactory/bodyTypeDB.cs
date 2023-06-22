using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;
using GarageCustomerAdmin._Models;

namespace BAL.Repositories
{
    public class bodyTypeDB : baseDB
    {
        public static BodyTypeBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public bodyTypeDB()
           : base()
        {
            repo = new BodyTypeBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<BodyTypeBLL> GetAll()
        {
            try
            {
                var lst = new List<BodyTypeBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBodyType_CAdmin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<BodyTypeBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BodyTypeBLL Get(int id)
        {
            try
            {
                var _obj = new BodyTypeBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBodyTypeByID_CAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<BodyTypeBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(BodyTypeBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[4] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@DisplayOrder", data.DisplayOrder);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertBodyType_CAdmin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(BodyTypeBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[4] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[7] = new SqlParameter("@BodyTypeID", data.BodyTypeID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateBodyType_CAdmin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(BodyTypeBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@BodyTypeID", data.BodyTypeID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteBodyType_CAdmin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
