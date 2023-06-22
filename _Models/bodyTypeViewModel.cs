using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class bodyTypeViewModel
    {
    }

    public class BodyTypeBLL
    {
        public int BodyTypeID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Image { get; set; }
        public int StatusID { get; set; }
        public int DisplayOrder { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

    }

}
