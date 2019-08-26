using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_jQuery_DataTablesNet.Dto;
using WebApps_jQuery_DataTablesNet.Interfaces;

using CsvHelper;
using System.IO;

namespace WebApps_jQuery_DataTablesNet.Services
{
    public class DataService : IDataService
    {
        private static readonly IList<AdwProductDto> _adwProducts = new List<AdwProductDto>();

        public IQueryable<AdwProductDto> GeAdwProducts()
        {
            if (_adwProducts.Any() == false)
            {
                var rn = "WebApps_jQuery_DataTablesNet.Data.AdwentureWorksProducts.csv";
                var assembly = typeof(Extensions).Assembly;

                using (var stream = assembly.GetManifestResourceStream(rn))
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.HasHeaderRecord = true;

                    while (csv.Read())
                    {
                        var record = csv.GetRecord<AdwProductDto>();
                        _adwProducts.Add(record);
                    }
                }
            }

            return _adwProducts.AsQueryable();
        }
    }
}
