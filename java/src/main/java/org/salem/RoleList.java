package org.salem;

import org.salem.common.*;

import java.util.*;
import java.util.stream.Collectors;

/**
 * Represents a role list. It may contain either fixed role slots, or wildcard role slots.
 */
public class RoleList
{
    private final RoleSlot[] roleSlots;

    /**
     * Initializes a new instance of the {@link RoleList} class from the given role slots array.
     * @param slots The array of the role slots to initialize this instance from. It must contain up to 15 elements.
     */
    public RoleList(RoleSlot[] slots)
    {
        validateGivenSlotCount(slots.length);
        roleSlots = ArrayUtils.copyOf(slots);
    }
    /**
     * Initializes a new instance of the {@link RoleList} class from the given role slots collection.
     * @param slots The collection of the role slots to initialize this instance from. It must contain up to 15
     *              elements.
     */
    public RoleList(Collection<RoleSlot> slots)
    {
        validateGivenSlotCount(slots.size());
        roleSlots = slots.toArray(RoleSlot[]::new);
    }
    /**
     * Initializes a new instance of the {@link RoleList} class from another {@link RoleList} instance.
     * @param other The other instance to copy the role slots from.
     */
    public RoleList(RoleList other)
    {
        roleSlots = ArrayUtils.copyOf(other.roleSlots);
    }

    /**
     * Gets a copy of the internally stored role slots array.
     * @return The copy of the role slots array.
     */
    public RoleSlot[] getRoleSlots() { return Arrays.stream(roleSlots).toArray(RoleSlot[]::new); }

    /**
     * Gets all the fixed role slots.
     * @return The fixed role slots.
     */
    public Collection<Role> getFixedRoleSlots() { return getRoleSlotsOfType(Role.class); }
    /**
     * Gets all the wildcard role slots.
     * @return The wildcard role slots.
     */
    public Collection<RoleAlignment> getWildcardRoleSlots() { return getRoleSlotsOfType(RoleAlignment.class); }

    private <T extends RoleSlot> Collection<T> getRoleSlotsOfType(Class<T> subclass)
    {
        return CollectionHandling.collectOfType(CollectionHandling.asCollection(roleSlots), subclass);
    }

    private void validateGivenSlotCount(int count)
    {
        if (count > 15)
            throw new IllegalArgumentException("The slot collection may not contain more than 15 slots.");
    }

    /**
     * Gets a {@link ValueCounterHashMap} that contains the number of occurrences of each role in this list. This
     * ignores the {@link RoleAlignment} slots.
     * @return The {@link HashMap{TKey}} containing the number of occurrences of each role in this list.
     */
    public ValueCounterHashMap<Class<? extends Role>> getRoleOccurrences()
    {
        var result = new ValueCounterHashMap<Class<? extends Role>>();
        getFixedRoleSlots().forEach(role -> result.add(role.getClass(), 1));
        return result;
    }

    /**
     * Gets a {@link ValueCounterHashMap} that contains the distinct role types that are present in this list. This
     * ignores the {@link RoleAlignment} slots.
     * @return The {@link ValueCounterHashMap} containing the distinct role types that are present in this list.
     */
    public HashSet<Class<? extends Role>> getDistinctRoles()
    {
        return new HashSet<>(getRoleOccurrences().keySet());
    }

    /**
     * Determines whether this current role list is a valid one.
     * @param packTypes The game pack types where this role list is applied on.
     * @return {@code true} if the current role list is valid for the given game pack types, otherwise {@code false}.
     */
    public boolean isValidRoleList(GamePackTypes packTypes) { return validateRoleList(packTypes, new InstanceReference<>()); }

    /**
     * Generates a random role list from this given role list, replacing wildcard slots with randomly chosen ones fitting the given criteria.
     * @param packTypes The game pack types where this role list is applied on.
     * @return The generated role list if this role list is valid, otherwise {@code null}.
     */
    public RoleList generateRandomRoleList(GamePackTypes packTypes)
    {
        return generateRandomRoleList(new RoleCollection(RoleCollection.getAllAvailableRolesCollection().getAllStartableRoleTypesIntersection(packTypes)));
    }
    /**
     * Generates a random role list from this given role list, replacing wildcard slots with randomly chosen ones fitting the given criteria.
     * @param availableRoles The available roles collection.
     * @return The generated role list if this role list is valid, otherwise {@code null}.
     */
    public RoleList generateRandomRoleList(RoleCollection availableRoles)
    {
        var remainingSlots = new InstanceReference<ValueCounterHashMap<Class<? extends Role>>>();
        if (!validateRoleList(availableRoles, remainingSlots))
            return null;

        var availableRoleTypesList = remainingSlots.get().keySet().stream().filter(key -> remainingSlots.get().get(key) > 0).collect(Collectors.toList());
        var availableRoleTypes = new RoleCollection(availableRoleTypesList);

        var random = new Random();
        var resultingArray = getRoleSlots();
        for (int i = 0; i < resultingArray.length; i++)
        {
            var roleEntry = resultingArray[i];

            var role = roleEntry.generateRandomRole(availableRoleTypes, random);
            resultingArray[i] = role;

            var type = role.getClass();
            remainingSlots.get().subtract(type);

            if (remainingSlots.get().get(type) <= 0)
                availableRoleTypes.remove(role);
        }

        return new RoleList(resultingArray);
    }

    private boolean validateRoleList(GamePackTypes packTypes, InstanceReference<ValueCounterHashMap<Class<? extends Role>>> remainingSlots)
    {
        return validateRoleList(new RoleCollection(RoleCollection.getAllAvailableRolesCollection().getAllStartableRoleTypesIntersection(packTypes)), remainingSlots);
    }
    private boolean validateRoleList(RoleCollection availableRoles, InstanceReference<ValueCounterHashMap<Class<? extends Role>>> remainingSlots)
    {
        remainingSlots.set(new ValueCounterHashMap<>());
        // This exists to prevent impossible role lists like more than 6 Coven roles in a game (since all the Coven roles are unique)
        var remainingAlignmentSlots = new ValueCounterHashMap<Faction>();

        for (var t : availableRoles)
        {
            var instance = RoleInstancePool.getInstance().getInstance(t);
            remainingSlots.get().add(t, instance.getMaximumOccurrences());
            remainingAlignmentSlots.add(instance.getFaction(), instance.getMaximumOccurrences());
        }

        for (var slot : roleSlots)
        {
            RoleAlignment alignment;
            if (slot instanceof RoleAlignment)
            {
                alignment = (RoleAlignment)slot;
            }
            else
            {
                var role = (Role)slot;

                alignment = role.getFullAlignment();
                var type = role.getClass();
                remainingSlots.get().subtract(type);
                if (remainingSlots.get().get(type) < 0)
                    return false;
            }

            var faction = alignment.roleFaction;

            if (faction == Faction.Any)
                continue;

            remainingAlignmentSlots.subtract(faction);
            if (remainingAlignmentSlots.get(faction) < 0)
                return false;
        }

        return true;
    }

    /**
     * Gets the {@link RoleSlot} at the specified index in this list.
     * @param index The index of the {@link RoleSlot} to get.
     * @return The {@link RoleSlot} at the specified index.
     */
    public RoleSlot get(int index) { return roleSlots[index]; }
}
