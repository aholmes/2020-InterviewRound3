using System;
using System.Linq;
using UCLARound3.Domain;
using UCLARound3.UnitTests.Helpers;
using Xunit;

namespace UCLARound3.UnitTests.Domain
{

    public class BkTreeTests
    {
        [Theory]
        [InlineData("ZEVG","BEVG",1)]
        [InlineData("AAKE","BAKE",1)]
        [InlineData("CKNF","CANF",1)]
        [InlineData("CXSB","CNSB",1)]
        [InlineData("SMCN","SNCN",1)]
        [InlineData("DJEG","DREG",1)]
        [InlineData("FOZN","FRZN",1)]
        [InlineData("FKVG","FRVG",1)]
        [InlineData("GPPA","GRPA",1)]
        [InlineData("MISF","MTSF",1)]
        [InlineData("MTSC","MISC",1)]
        [InlineData("XXVG","BEVG",2)]
        [InlineData("XXXG","BEVG",3)]
        [InlineData("XXXX","BEVG",4)]
        [InlineData("BEVG","",4)]
        [InlineData("","BEVG",4)]
        [InlineData("","",0)]
        [InlineData("BEV","BEVG",1)]
        [InlineData("XEV","BEVG",2)]
        public void LevenshteinDistance_Returns_Correct_Distances(string a, string b, int expectedDistance)
        {
            #region Act
            var distance = BkTree.LevenshteinDistance(a, b);
            #endregion

            #region Assert
            Assert.Equal(expectedDistance, distance);
            #endregion
        }

        [Theory]
        [InlineData("XEVG","BEVG")]
        [InlineData("XANF","CANF")]
        [InlineData("XRZN","FRZN")]
        public void Search_Returns_Single_Match_When_Only_One_Match_Is_Possible(string input, string expectedMatch)
        {
            #region Arrange
            var tree = new BkTree();
            foreach(var key in SampleDataGenerator.SamplePurchaseEntity.Barcodes.Select(barcode => barcode.ProductType).Distinct())
            {
                tree.Add(key);
            }
            #endregion

            #region Act
            var result = tree.Search(input, 1);
            #endregion

            #region Assert
            Assert.Single(result);
            Assert.Equal(expectedMatch, result.Single());
            #endregion
        }

        [Theory]
        [InlineData("BEVG",1,"BEVG",2)]
        [InlineData("XEVG",2,"BEVG",2)]
        [InlineData("XEVG",3,"BEVG",4)]
        [InlineData("X",3,"BEVX",4)]
        [InlineData("XXXX",int.MaxValue,"XXXX",7)]
        public void Search_Returns_Multiple_Matches_When_More_Than_One_Match_Is_Possible(
            string input,
            int distanceTolerance,
            string expectedMatch,
            int expectedMatchCount)
        {
            #region Arrange
            var tree = new BkTree();
            foreach(var key in SampleDataGenerator.SamplePurchaseEntity.Barcodes.Select(barcode => barcode.ProductType).Distinct())
            {
                tree.Add(key);
            }
            tree.Add("BEVX");
            tree.Add("BEXX");
            tree.Add("BXXX");
            tree.Add("XXXX");
            #endregion

            #region Act
            var result = tree.Search(input, distanceTolerance);
            #endregion

            #region Assert
            Assert.Contains(expectedMatch, result);
            Assert.Equal(expectedMatchCount, result.Count);
            #endregion
        }

        [Fact]
        public void Search_Throws_On_Null_Input()
        {
            #region Arrange
            var tree = new BkTree();
            foreach(var key in SampleDataGenerator.SamplePurchaseEntity.Barcodes.Select(barcode => barcode.ProductType).Distinct())
            {
                tree.Add(key);
            }
            #endregion

            #region Act
            void act() => tree.Search("", 1);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(act);
            #endregion
        }

        [Fact]
        public void Search_Throws_When_No_Words_Added_To_Tree()
        {
            #region Arrange
            var tree = new BkTree();
            #endregion

            #region Act
            void act() => tree.Search("BEVG", 1);
            #endregion

            #region Assert
            Assert.Throws<InvalidOperationException>(act);
            #endregion
        }
    }
}
