using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_Mvc_DependencyInjection.Services
{
    public class StatisticsService
    {
        public StatisticsService()
        {
        }

        public int GetCount()
        {
            return 10;
        }

        public int GetCompletedCount()
        {
            return 2;
        }

        public double GetAveragePriority()
        {
            return 1.3D;
        }
    }
}
