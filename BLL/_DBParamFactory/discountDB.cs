﻿

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

    public class discountDB : baseDB
    {
        public static DiscountBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public discountDB()
           : base()
        {
            repo = new DiscountBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<DiscountBLL> GetAll()
        {
            try
            {
                var lst = new List<DiscountBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetDiscount");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DiscountBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DiscountBLL Get(int id)
        {
            try
            {
                var _obj = new DiscountBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDiscountByID", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DiscountBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(DiscountBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[14];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@ArabicName", data.ArabicName);
                p[3] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@ArabicImage", data.ArabicImage);
                p[6] = new SqlParameter("@FromDate", data.FromDate);
                p[7] = new SqlParameter("@ToDate", data.ToDate);
                p[8] = new SqlParameter("@FromTime", data.FromTime);
                p[9] = new SqlParameter("@ToTime", data.ToTime);
                p[10] = new SqlParameter("@StatusID", data.StatusID);
                p[11] = new SqlParameter("@ThumbnailImage", data.ThumbnailImage);
                p[12] = new SqlParameter("@BackgroundColor", data.BackgroundColor);
                p[13] = new SqlParameter("@FontColor", data.FontColor);

                //rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertDiscount", p);
                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_InsertDiscount", p).Rows[0]["ID"].ToString());
                //rtn = DiscountID;

                if (data.Locations != "")
                {
                    SqlParameter[] p1 = new SqlParameter[3];

                    p1[0] = new SqlParameter("@Locations", data.Locations == "" ? null : data.Locations);
                    p1[1] = new SqlParameter("@DiscountID", rtn);
                    p1[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertDiscLocationJunc_CAdmin", p1);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DiscountBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[15];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@ArabicName", data.ArabicName);
                p[3] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@ArabicImage", data.ArabicImage);
                p[6] = new SqlParameter("@FromDate", data.FromDate);
                p[7] = new SqlParameter("@ToDate", data.ToDate);
                p[8] = new SqlParameter("@FromTime", data.FromTime);
                p[9] = new SqlParameter("@ToTime", data.ToTime);
                p[10] = new SqlParameter("@StatusID", data.StatusID);
                p[11] = new SqlParameter("@DiscountID", data.DiscountID);
                p[12] = new SqlParameter("@ThumbnailImage", data.ThumbnailImage);
                p[13] = new SqlParameter("@BackgroundColor", data.BackgroundColor);
                p[14] = new SqlParameter("@FontColor", data.FontColor);


                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateDiscount_Admin", p);
                if (data.Locations != "")
                {
                    SqlParameter[] p1 = new SqlParameter[3];
                    p1[0] = new SqlParameter("@Locations", data.Locations == "" ? null : data.Locations);
                    p1[1] = new SqlParameter("@DiscountID", data.DiscountID);
                    p1[2] = new SqlParameter("@LastUpdatedDate", DateTime.Now);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertDiscLocationJunc_CAdmin", p1);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DiscountBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@DiscountID", data.DiscountID);
                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteDiscount_CAdmin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
