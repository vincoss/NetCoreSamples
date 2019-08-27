using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_jQuery_Samples.Dto;


namespace WebApps_jQuery_Samples.Interfaces
{
    public interface IDataService
    {
        IQueryable<AdwProductDto> GeAdwProducts();
        Task<IQueryable<string>> GetCSharpKeywords();
    }
}
