using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using UCLARound3.UnitTests.Helpers;
using Xunit;

namespace UCLARound3.UnitTests.Domain.Value
{
    public class ProductIdTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVXXYZ")]
        public void Instantiation_Throws_When_Input_Is_Not_20_Characters_Long(string value)
        {
            #region Arrange/Act
            void create() => new ProductIdValue(value);
            #endregion

            #region Assert
            Assert.Throws<ArgumentException>(create);
            #endregion
        }
    }
}