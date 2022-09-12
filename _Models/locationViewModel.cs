using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class locationViewModel
    {
    }

    //public class LocationBLL1
    //{
    //    public int LocationID { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public string Address { get; set; }
    //    public string ContactNo { get; set; }
    //    public string Email { get; set; }
    //    public double? MinOrderAmount { get; set; }

    //    public string Longitude { get; set; }
    //    public string Latitude { get; set; }
    //    public int LandmarkID { get; set; }

    //    public string LastUpdatedBy { get; set; }
    //    public string LastUpdatedDate { get; set; }

    //    public int StatusID { get; set; }
    //    public string ImageURL { get; set; }

    //    public string GMapLink { get; set; }
    //    public bool IsFeatured { get; set; }
    //}
    public class LocationBLL
    {  
        public int LocationID { get; set; }                                
        public string Name { get; set; }
        public string Descripiton { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }        
        public double? MinOrderAmount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }         
        public int LandmarkID { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public int StatusID { get; set; }
        public string ImageURL { get; set; }    
        public string Gmaplink { get; set; }

        public string Amenities { get; set; }
        public string Service { get; set; }
         
        public int? IsFeatured { get; set; }

        public List<LocationimagesBLL> LocationImages = new List<LocationimagesBLL>();
        public List<string> ImagesSource { get; set; }
        
    }
    public class LocationimagesBLL
    {
        public int LocationImageID { get; set; }

        public string Image { get; set; }

        public int? LocationID { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? Updatedby { get; set; }

    }
}
