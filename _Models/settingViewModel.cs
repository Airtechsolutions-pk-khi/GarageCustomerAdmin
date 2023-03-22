using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class settingViewModel
    {
    }

    public class SettingBLL
    {
        public int? ID { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public string ArabicTitle { get; set; }
        public string ArabicDescription { get; set; }
        public string PageName { get; set; }
        public string Image { get; set; }
        public string AlternateImage { get; set; }
        public string Type { get; set; }
        public string Locations { get; set; }
        public int? DisplayOrder { get; set; }
        public int StatusID { get; set; }

    }

}
