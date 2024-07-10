using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class discountViewModel
    {
    }

    public class DiscountBLL
    {
        public int DiscountID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ArabicDescription { get; set; }
        public string Image { get; set; }
        public string BackgroundColor { get; set; }
        public string FontColor { get; set; }
        //public List<string> ImagesSource { get; set; }
        public string ThumbnailImage { get; set; }
        public string ArabicImage { get; set; }
        public int StatusID { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Locations { get; set; }
    }

}
