using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class reviewsViewModel
    {
    }

    public class ReviewsBLL
    {
        public int ReviewID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Rate { get; set; }
        public string Location { get; set; }
        public int? StatusID { get; set; }
        public int? LocationID { get; set; }
        public DateTime? Date { get; set; }
    }
}