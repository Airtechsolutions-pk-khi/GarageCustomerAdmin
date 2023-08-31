using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
	public class carsViewModel
	{
	}
	public class CarsBLL
	{
		public int CarID { get; set; }
		public int? RowID { get; set; }
		public int? CustomerID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string VinNo { get; set; }
		public int? MakeID { get; set; }
		public int? ModelID { get; set; }
		public int? Year { get; set; }
		public string Color { get; set; }
		public string RegistrationNo { get; set; }
		public float CheckLitre { get; set; }
		public string EngineType { get; set; }
		public string RecommendedAmount { get; set; }
		public int? StatusID { get; set; }
		public string BinaryImage { get; set; }
		public string LastUpdatedBy { get; set; }
		public string LastUpdatedDate { get; set; }
		public string CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<int> LocationID { get; set; }
		public Nullable<int> UserID { get; set; }
		public string Gender { get; set; }
		public string CarType { get; set; }
		public string ImagePath { get; set; }
	}
	public class MakesBLL
	{
		public int MakeID { get; set; }

		public string Name { get; set; }
	}
	public class ModelsBLL
	{
		public int ModelID { get; set; }
		public int MakeID { get; set; }
		public string Name { get; set; }
	}
	public class CarsCustomerBLL
	{
		public int CustomerID { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Image { get; set; }
		public string City { get; set; }
		public int StatusID { get; set; }
		public string LastUpdatedBy { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<int> LocationID { get; set; }
		public Nullable<int> BrandID { get; set; }
		public string Password { get; set; }
	}

}
