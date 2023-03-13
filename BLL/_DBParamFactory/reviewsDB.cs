

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

    public class reviewsDB : baseDB
    {
        public static ReviewsBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public reviewsDB()
           : base()
        {
            repo = new ReviewsBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<ReviewsBLL> GetAll()
        {
            try
            {
                var lst = new List<ReviewsBLL>();

                _dt = (new DBHelper().GetTableFromSP)("sp_GetReviews_CAdmin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ReviewsBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
