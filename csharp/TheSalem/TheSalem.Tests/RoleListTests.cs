using NUnit.Framework;

namespace TheSalem.Tests
{
    public class RoleListTests
    {
        [Test]
        public void InitializeRoleListTest()
        {
            var list = RoleListFactory.AllAny;

            // Due to randomization, ensure that the generated role lists cover a lot of cases
            for (int i = 0; i < 50; i++)
            {
                var generatedList = list.GenerateRandomRoleList(GamePackTypes.Classic);
                var counters = generatedList.GetRoleOccurrences();
                foreach (var kvp in counters)
                {
                    var type = kvp.Key;
                    Assert.IsTrue(kvp.Value <= RoleInstancePool.Instance[type].MaximumOccurrences);
                }
            }
        }

        [Test]
        public void IsValidTest()
        {
            var slots = RoleListFactory.AllAny.RoleSlots;
            for (int i = 0; i < 2; i++)
                slots[i] = RoleInstancePool.Instance[typeof(Veteran)];

            var list = new RoleList(slots);

            Assert.IsFalse(list.IsValidRoleList(GamePackTypes.Classic));
            Assert.IsFalse(list.IsValidRoleList(GamePackTypes.Coven));
            Assert.IsFalse(list.IsValidRoleList(GamePackTypes.All));

            slots[1] = RoleAlignment.Any;

            list = new RoleList(slots);

            Assert.IsTrue(list.IsValidRoleList(GamePackTypes.Classic));
            Assert.IsTrue(list.IsValidRoleList(GamePackTypes.Coven));
            Assert.IsTrue(list.IsValidRoleList(GamePackTypes.All));

            slots[0] = RoleInstancePool.Instance[typeof(CovenLeader)];

            list = new RoleList(slots);

            Assert.IsFalse(list.IsValidRoleList(GamePackTypes.Classic));
            Assert.IsTrue(list.IsValidRoleList(GamePackTypes.Coven));
            Assert.IsFalse(list.IsValidRoleList(GamePackTypes.All));
        }
    }
}