using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageCustomerAdmin._Models
{
    public class dashboardViewModel
    {
    }
    public class RspDashboard
    {
        public DashboardSummary summarysales { get; set; }
        public DashboardToday todaysales{ get; set; }
        public DashboardMAEN maensales { get; set; }
    }
    public class DashboardSummary
    {
        public double TotalService { get; set; }
        public double TotalLocation { get; set; }
        public double TotalFeature { get; set; }
        public double TotalCarsell { get; set; }
    }
    public class DashboardToday
    {
        public List<string> Sales { get; set; }
        public List<string> TimeSlot { get; set; }
    }
    public class DashboardMAEN
    {
        public double Morning { get; set; }
        public double Evening { get; set; }
        public double AfterNoon { get; set; }
        public double Night { get; set; }
    }
}
