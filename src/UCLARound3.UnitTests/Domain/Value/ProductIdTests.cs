using InterviewRound3.Domain.Value;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InterviewRound3.Domain.Entity;
using InterviewRound3.UnitTests.Helpers;
using Xunit;

namespace InterviewRound3.UnitTests.Domain.Value
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