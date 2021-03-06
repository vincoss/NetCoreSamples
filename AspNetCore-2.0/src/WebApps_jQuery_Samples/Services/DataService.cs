﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_jQuery_Samples.Dto;
using WebApps_jQuery_Samples.Interfaces;

using CsvHelper;
using System.IO;
using System.Globalization;

namespace WebApps_jQuery_Samples.Services
{
    public class DataService : IDataService
    {
        private static readonly IList<AdwProductDto> _adwProducts = new List<AdwProductDto>();

        public IQueryable<string> GeAdwProductCategories()
        {
            var query = GeAdwProducts().Select(x => x.ProductCategory ?? string.Empty).Distinct();
            return query;
        }

        public IQueryable<AdwProductDto> GeAdwProducts()
        {
            if (_adwProducts.Any() == false)
            {
                var rn = "WebApps_jQuery_Samples.Data.AdwentureWorksProducts.csv";
                var assembly = typeof(UtilityExtensions).Assembly;

                using (var stream = assembly.GetManifestResourceStream(rn))
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
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

        public Task<IQueryable<string>> GetCSharpKeywords()
        {
            var keywordk = UtilityExtensions.GetEmbeddedContent("WebApps_jQuery_Samples.Data.CSharpKeywords.txt");
            var items = keywordk.Split(Environment.NewLine);
            return Task.FromResult(items.OrderBy(x => x).AsQueryable());
        }

    }
}
