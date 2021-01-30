package org.salem;

import java.util.Random;

public interface RoleSlot
{
    /**
     * Generates a random role from the provided dictionary of available roles.
     * @param availableRoles The dictionary of available roles the generated role will be from.
     * @param random The instance of {@link Random} to use to randomly choose the role.
     * @return The randomly generated role.
     */
    public Role generateRandomRole(RoleCollection availableRoles, Random random);
}
