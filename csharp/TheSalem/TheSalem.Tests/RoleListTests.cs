using NUnit.Framework;

namespace TheSalem.Tests
{
    public class RoleListTests
    {
        [Test]
        public void InitializeRoleListTest()
        {
            var list = new RoleList(new IRoleSlot[]
            {
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,

                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,

                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
                RoleAlignment.Any,
            });

            // Due to randomization, ensure that the generated role lists cover a lot of cases
            for (int i = 0; i < 50; i++)
            {
                var generatedList = list.GenerateRandomRoleList(GamePackType.Classic);
                var counters = generatedList.GetRoleOccurrences();
                foreach (var kvp in counters)
                {
                    var type = kvp.Key;
                    Assert.IsTrue(kvp.Value <= RoleInstancePool.Instance[type].MaximumOccurrences);
                }
            }
        }
    }
}