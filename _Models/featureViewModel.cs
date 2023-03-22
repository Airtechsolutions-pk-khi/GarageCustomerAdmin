using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class featureViewModel
    {
    }

    public class FeatureBLL
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }        
        public string ArabicName { get; set; }
        public string Image { get; set; }
        public int StatusID { get; set; }
        
    }

}
