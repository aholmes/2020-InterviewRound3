using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace UCLARound3.UnitTests.Helpers
{
    /// <summary>
    /// https://blog.ploeh.dk/2010/10/08/AutoDataTheorieswithAutoFixture/
    /// </summary>
    public class AutoMoqDataAttribute: AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
