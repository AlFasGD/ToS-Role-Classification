using NUnit.Framework;

namespace TheSalem.Tests
{
    public class RoleAlignmentTests
    {
        [Test]
        public void MatchesFilterTest()
        {
            var alignment = RoleAlignment.MafiaSupport;
            AssertFilterMatching(RoleAlignment.MafiaSupport, true);
            AssertFilterMatching(RoleAlignment.MafiaAny, true);
            AssertFilterMatching(RoleAlignment.Any, true);
            AssertFilterMatching(new(Alignment.Support), true);

            AssertFilterMatching(RoleAlignment.CovenAny, false);
            AssertFilterMatching(RoleAlignment.TownSupport, false);

            void AssertFilterMatching(RoleAlignment other, bool expected)
            {
                Assert.AreEqual(expected, alignment.MatchesFilter(other));
            }
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual("Mafia Any", RoleAlignment.MafiaAny.ToString());
            Assert.AreEqual("Any", RoleAlignment.Any.ToString());
            Assert.AreEqual("Any Support", new RoleAlignment(Alignment.Support).ToString());
            Assert.AreEqual("Coven Evil", RoleAlignment.CovenEvil.ToString());
        }
    }
}