using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace InterviewRound3.UnitTests.Helpers
{
    /// <summary>
    /// https://blog.ploeh.dk/2010/10/08/AutoDataTheorieswithAutoFixture/
    /// Automatically create new Mock{T} objects in test method parameters.
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
