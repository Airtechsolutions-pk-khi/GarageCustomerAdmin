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
	public class blogDB : baseDB
	{
		public static BlogBLL repo;
		public static DataTable _dt;
		public static DataSet _ds;
		public blogDB()
		   : base()
		{
			repo = new BlogBLL();
			_dt = new DataTable();
			_ds = new DataSet();
		}
		public List<BlogBLL> GetAll(DateTime FromDate, DateTime ToDate)
		{
			try
			{
				var lst = new List<BlogBLL>();
				SqlParameter[] p = new SqlParameter[2];
				p[0] = new SqlParameter("@fromdate", FromDate.Date);
				p[1] = new SqlParameter("@todate", ToDate.Date);

				_dt = (new DBHelper().GetTableFromSP)("sp_GetBlog_CADMIN", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BlogBLL>>();
					}
				}
				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public List<CountryBLL> GetAllCountries()
		{
			try
			{
				var lst = new List<CountryBLL>();
				_dt = (new DBHelper().GetTableFromSP)("sp_GetCountries");
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
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
		public BlogBLL Get(int id)
		{
			try
			{
				var _obj = new BlogBLL();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@id", id);
				_dt = (new DBHelper().GetTableFromSP)("sp_GetBlogById_CADMIN", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						_obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BlogBLL>>().FirstOrDefault();
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
				var _obj = new BlogBLL();
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
				var _obj = new BlogBLL();
				List<string> ImagesSource = new List<string>();
				_dt = new DataTable();
				SqlParameter[] p1 = new SqlParameter[1];
				p1[0] = new SqlParameter("@id", id);
				_dt = (new DBHelper().GetTableFromSP)("sp_GetBlogImages_CAdmin", p1);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						_obj.BlogImages = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BlogImageBLL>>();

						for (int i = 0; i < _obj.BlogImages.Count; i++)
						{
							ImagesSource.Add(_obj.BlogImages[i].Image);
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
		public int Insert(BlogBLL data)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[13];

				p[0] = new SqlParameter("@Title", data.Title);
				p[1] = new SqlParameter("@ArabicTitle", data.ArabicTitle);
				p[2] = new SqlParameter("@Description", data.Description);
				p[3] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
				p[4] = new SqlParameter("@Country", data.Country);
				p[5] = new SqlParameter("@City", data.City);
				p[6] = new SqlParameter("@StartDate", data.StartDate);
				p[7] = new SqlParameter("@EndDate", data.EndDate);
				p[8] = new SqlParameter("@Type", data.Type);
				p[9] = new SqlParameter("@IsFeatured", data.IsFeatured);
				p[10] = new SqlParameter("@CreatedDate", DateTime.Now);
				p[11] = new SqlParameter("@CreatedBy", 1);
				p[12] = new SqlParameter("@StatusID", data.StatusID);
				rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_InsertBlog_CAdmin", p).Rows[0]["BlogID"].ToString());
				try
				{
					var imgStr = String.Join(",", data.BlogImages.Select(p => p.Image));
					SqlParameter[] p2 = new SqlParameter[4];
					p2[0] = new SqlParameter("@Images", imgStr);
					p2[1] = new SqlParameter("@BlogID", rtn);
					p2[2] = new SqlParameter("@CreatedDate", DateTime.UtcNow);
					p2[3] = new SqlParameter("@StatusID", data.StatusID);
					(new DBHelper().ExecuteNonQueryReturn)("sp_insertBlogImages_CAdmin", p2);
				}
				catch (Exception ex)
				{
					return 0;
				}
				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
		public int Update(BlogBLL data)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[13];

				p[0] = new SqlParameter("@Title", data.Title);
				p[1] = new SqlParameter("@ArabicTitle", data.ArabicTitle);
				p[2] = new SqlParameter("@Description", data.Description);
				p[3] = new SqlParameter("@ArabicDescription", data.ArabicDescription);
				p[4] = new SqlParameter("@Country", data.Country);
				p[5] = new SqlParameter("@City", data.City);
				p[6] = new SqlParameter("@StartDate", data.StartDate);
				p[7] = new SqlParameter("@EndDate", data.EndDate);
				p[8] = new SqlParameter("@Type", data.Type);
				p[9] = new SqlParameter("@IsFeatured", data.IsFeatured);
				p[10] = new SqlParameter("@CreatedDate", DateTime.Now);
				p[11] = new SqlParameter("@CreatedBy", 1);
				p[12] = new SqlParameter("@StatusID", data.StatusID);
				rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_UpdateBlog_CAdmin", p).Rows[0]["BlogID"].ToString());
				try
				{
					var imgStr = String.Join(",", data.BlogImages.Select(p => p.Image));
					SqlParameter[] p2 = new SqlParameter[4];
					p2[0] = new SqlParameter("@Images", imgStr);
					p2[1] = new SqlParameter("@BlogID", rtn);
					p2[2] = new SqlParameter("@CreatedDate", DateTime.UtcNow);
					p2[3] = new SqlParameter("@StatusID", data.StatusID);
					(new DBHelper().ExecuteNonQueryReturn)("sp_insertBlogImages_CAdmin", p2);
				}
				catch (Exception ex)
				{
					return 0;
				}
				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
		public int Delete(BlogBLL data)
		{
			try
			{
				int _obj = 0;
				SqlParameter[] p = new SqlParameter[2];
				p[0] = new SqlParameter("@id", data.BlogID);
				p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

				_obj = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeleteBlog_CADMIN", p);

				return _obj;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}
