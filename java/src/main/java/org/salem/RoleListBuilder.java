package org.salem;

import org.salem.common.*;

/**
 * Represents a role list. It may contain either fixed role slots, or wildcard role slots.
 */
public class RoleListBuilder
{
    /**
     * The role list slots. This field is exposed to allow directly setting the role slots or use the {@link CappedList}
     * class' methods.
     */
    public CappedList<RoleSlot> roleSlots;

    /**
     * Initializes a new empty instance of the {@link RoleListBuilder} class.
     */
    public RoleListBuilder()
    {
        roleSlots = new CappedList<>(15);
    }

    /**
     * Creates a new {@link RoleList} out of the current role list.
     * @return The generated {@link RoleList}.
     */
    public RoleList toRoleList() { return new RoleList(roleSlots); }

    /**
     * Adds a role of the provided type to the list, if it is not full, otherwise nothing happens.
     * @param roleType The type of the role to add.
     */
    public void add(Class<? extends Role> roleType)
    {
        roleSlots.add(RoleInstancePool.getInstance().getInstance(roleType));
    }
    /**
     * Adds a role alignment to the list, if it is not full, otherwise nothing happens.
     * @param alignment The role alignment to add to the role list.
     */
    public void add(RoleAlignment alignment) { roleSlots.add(alignment); }
    /**
     * Adds a role of the provided type to the list a number of times, until the plist is full.
     * @param count The number of times to add the role to the list.
     * @param roleType The type of the role to add.
     */
    public void add(int count, Class<? extends Role> roleType)
    {
        for (int i = 0; i < count; i++)
            add(roleType);
    }
    /**
     * Adds a role slot to the list a number of times, until the list is full.
     * @param count The number of times to add the slot to the list.
     * @param slot The slot to add to the list.
     */
    public void add(int count, RoleSlot slot)
    {
        for (int i = 0; i < count; i++)
            roleSlots.add(slot);
    }
}
