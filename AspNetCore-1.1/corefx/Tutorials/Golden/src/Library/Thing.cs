using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
   /// <summary>
   /// NOTE: Based on
   /// https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-on-windows
   /// </summary>
    public class Thing
    {
        public int Get(int number) => Newtonsoft.Json.JsonConvert.DeserializeObject<int>($"{number}");
    }
}
