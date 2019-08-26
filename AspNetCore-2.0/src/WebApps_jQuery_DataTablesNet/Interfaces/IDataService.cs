using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_jQuery_DataTablesNet.Dto;


namespace WebApps_jQuery_DataTablesNet.Interfaces
{
    public interface IDataService
    {
        IQueryable<AdwProductDto> GeAdwProducts();
    }
}
