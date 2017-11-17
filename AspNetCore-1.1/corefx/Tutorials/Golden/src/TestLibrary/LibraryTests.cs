using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace TestLibrary
{
    /// <summary>
    /// NOTE: Based on
    /// https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-on-windows
    /// </summary>
    public class LibraryTests
    {
        [Fact]
        public void ThingGetsObjectValFromNumber()
        {
            Assert.Equal(42, new Thing().Get(42));
        }
    }
}
