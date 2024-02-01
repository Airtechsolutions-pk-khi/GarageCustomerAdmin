using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
	public class locationViewModel
	{
	}
	public class LocationBLL
	{
		public int LocationID { get; set; }
		public int UserID { get; set; }
		public string Name { get; set; }
		public string ArabicName { get; set; }
		public string ArabicDescription { get; set; }
		public string Descripiton { get; set; }
		public string Address { get; set; }
		public string ArabicAddress { get; set; }
		public string ContactNo { get; set; }
		public string Email { get; set; }
		//public double? MinOrderAmount { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public int? LandmarkID { get; set; }
		public string LastUpdatedBy { get; set; }
		public string LastUpdatedDate { get; set; }
		public int StatusID { get; set; }
		public int? CustomerStatusID { get; set; }
		public string ImageURL { get; set; }
		public string Gmaplink { get; set; }
		public string Amenities { get; set; }
		public string Service { get; set; }
		public string BrandThumbnailImage { get; set; }
		public string BusinessType { get; set; }
		public string CountryID { get; set; }
		public int? CityID { get; set; }
		public bool? IsFeatured { get; set; }
		public List<LocationimagesBLL> LocationImages = new List<LocationimagesBLL>();
		public List<string> ImagesSource { get; set; }
		public List<LocationTimings> LocationTimings { get; set; }
		public List<LocationTimings> ArabicTimings { get; set; }
	}
	//public class LocationBLL1
	//{
	//	public int LocationID { get; set; }
	//	public string Name { get; set; }
	//	public string ArabicName { get; set; }
	//	public string ArabicDescription { get; set; }
	//	public string Descripiton { get; set; }
	//	public string Address { get; set; }
	//	public string ArabicAddress { get; set; }
	//	public string ContactNo { get; set; }
	//	public string Email { get; set; }
	//	//public double? MinOrderAmount { get; set; }
	//	public string Longitude { get; set; }
	//	public string Latitude { get; set; }
	//	public int? LandmarkID { get; set; }
	//	public string LastUpdatedBy { get; set; }
	//	public string LastUpdatedDate { get; set; }
	//	public int StatusID { get; set; }
	//	public int? CustomerStatusID { get; set; }
	//	public string ImageURL { get; set; }
	//	public string Gmaplink { get; set; }
	//	public string Amenities { get; set; }
	//	public string Service { get; set; }
	//	public string BrandThumbnailImage { get; set; }
	//	public string CountryCode { get; set; }
	//	public int CityID { get; set; }
	//	public bool? IsFeatured { get; set; }
	//	public List<LocationimagesBLL> LocationImages = new List<LocationimagesBLL>();
	//	public List<string> ImagesSource { get; set; }
	//	public List<LocationTimings> LocationTimings { get; set; }
	//}
	public class LocationimagesBLL
	{
		public int LocationImageID { get; set; }
		public string ImageURL { get; set; }
		public int? LocationID { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? Updatedby { get; set; }
	}
	public class LocationTimings
	{
		public int? LocationID { get; set; }
		public string Name { get; set; }
		public string Time { get; set; }
        public string ArabicName { get; set; }
        public string ArabicTime { get; set; }
    }
}
