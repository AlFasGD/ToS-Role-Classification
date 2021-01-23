using System;

namespace TheSalem
{
    /// <summary>Denotes that the type is a valid <seealso cref="Role"/> slot within a <seealso cref="RoleList"/>.</summary>
    public interface IRoleSlot
    {
        /// <summary>Generates a random role from the provided dictionary of available roles.</summary>
        /// <param name="availableRoles">The dictionary of available roles the generated role will be from.</param>
        /// <param name="random">The instance of <seealso cref="Random"/> to use to randomly choose the role.</param>
        /// <returns>The randomly generated role.</returns>
        public Role GenerateRandomRole(RoleDictionary availableRoles, Random random);
    }
}
