using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class blogViewModel
    {
    }
    public class RspBlogDetail
    {
        public BlogBLL Blog { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public List<BlogImageBLL> Image { get; set; }
    }

    public class BlogImageBLL
    {
        public int BlogImageID { get; set; }
        public int? BlogID { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
    public class BlogBLL
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string ArabicTitle { get; set; }
        public string Description { get; set; }
        public string ArabicDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
		public string Country { get; set; }
		public int? City { get; set; }
		public string Type { get; set; }
		public int? StatusID { get; set; }
		public bool IsFeatured { get; set; }
		public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public List<string> ImagesSource { get; set; }
		public List<BlogImageBLL> BlogImages = new List<BlogImageBLL>();
    }
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}
