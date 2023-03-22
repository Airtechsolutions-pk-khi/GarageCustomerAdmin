using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class serviceViewModel
    {
    }

    public class ServiceBLL
    {
        public int ServiceID { get; set; }
        public string ServiceTitle { get; set; }        
        public string ServiceDescription { get; set; }

        public string ArabicServiceTitle { get; set; }
        public string ArabicServiceDescription { get; set; }
        public string Image { get; set; }
        public int? DisplayOrder { get; set; }
        public int StatusID { get; set; }
        public string Type { get; set; }
    }

}
