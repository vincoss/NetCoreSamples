using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestDebugTroubleshoot_DotNetUnitTest
{
    public    class PrimeServicesTest
    {
        [Fact]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var service = new PrimeService();
            var result = service.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var service = new PrimeService();
            var result = service.IsPrime(value);

            Assert.False(result, $"{value} should not be prime");
        }
    }
}
