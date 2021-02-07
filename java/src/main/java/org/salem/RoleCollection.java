package org.salem;

import java.util.*;
import java.util.stream.Collectors;

/**
 * Represents a role type collection. Provides the ability to index the available roles by their faction or alignment.
 */
public class RoleCollection
    implements Collection<Class<? extends Role>>
{
    private static final RoleCollection defaultCollection;

    /**
     * Initializes a new role type collection with all the available roles in the game.
     */
    public static RoleCollection getAllAvailableRolesCollection() { return new RoleCollection(defaultCollection); }

    static
    {
        defaultCollection = new RoleCollection(RoleInstancePool.getInstance().getAllRoleTypes().keySet());
    }

    private int count;

    private final HashMap<RoleAlignment, HashSet<Class<? extends Role>>> roleTypesByAlignment;
    private final HashMap<Faction, HashSet<Class<? extends Role>>> roleTypesByFaction;

    /**
     * Initializes a new instance of the {@link RoleCollection} class with no roles.
     */
    public RoleCollection()
    {
        roleTypesByAlignment = new HashMap<>();
        roleTypesByFaction = new HashMap<>();
    }
    /**
     * Initializes a new instance of the {@link RoleCollection} class with the roles from the specified types.
     * @param roleTypes The types of the roles to add to the collection.
     */
    public RoleCollection(Collection<Class<? extends Role>> roleTypes)
    {
        this();
        roleTypes = roleTypes.stream().filter(Role::isValidRoleType).collect(Collectors.toSet());
        for (var t : roleTypes)
        addConditionallyValidated(t, false);
    }
    /**
     * Initializes a new instance of the {@link RoleCollection} class from another {@link RoleCollection} instance.
     * @param other The other {@link RoleCollection} instance to copy.
     */
    public RoleCollection(RoleCollection other)
    {
        count = other.count;
        roleTypesByAlignment = new HashMap<>(other.roleTypesByAlignment);
        roleTypesByFaction = new HashMap<>(other.roleTypesByFaction);
    }

    /**
     * Gets all the roles that belong to a faction.
     * @param faction The faction.
     * @return A collection of elements that belong to the provided faction.
     */
    public Class<? extends Role>[] get(Faction faction)
    {
        return roleTypesByFaction.get(faction).stream().toArray(Class[]::new);
    }
    /**
     * Gets all the roles that belong to an alignment.
     * @param alignment The alignment.
     * @return A collection of elements that belong to the provided alignment.
     *         If the alignment is {@link RoleAlignment#Any}, this returns {@link #getAllRoleTypes()}.
     *         If {@link RoleAlignment#roleAlignment} is {@link Alignment#Any},
     *         the {@link #get(Faction)} function's result is returned instead.
     */
    public Class<? extends Role>[] get(RoleAlignment alignment)
    {
        if (alignment == RoleAlignment.Any)
            return getAllRoleTypes().stream().toArray(Class[]::new);

        if (alignment.roleAlignment == Alignment.Any)
            return get(alignment.roleFaction);

        return roleTypesByAlignment.get(alignment).stream().toArray(Class[]::new);
    }

    /**
     * Gets a dictionary containing all the role types stored in this collection, grouped by alignment.
     * @return The dictionary containing this collection's role types grouped by their alignment.
     */
    public HashMap<RoleAlignment, HashSet<Class<? extends Role>>> getRoleTypesByAlignment()
    {
        return new HashMap<>(roleTypesByAlignment);
    }
    /**
     * Gets a dictionary containing all the role types stored in this collection, grouped by faction.
     * @return The dictionary containing this collection's role types grouped by their faction.
     */
    public HashMap<Faction, HashSet<Class<? extends Role>>> getRoleTypesByFaction()
    {
        return new HashMap<>(roleTypesByFaction);
    }

    /**
     * Gets all the available role types in the game.
     */
    public Collection<Class<? extends Role>> getAllRoleTypes()
    {
        var result = new HashSet<Class<? extends Role>>();
        for (var set : roleTypesByAlignment.values())
            result.addAll(set);
        return result;
    }
    /**
     * Gets all the available role types in the game that the player can start as.
     */
    public Collection<Class<? extends Role>> getAllStartableRoleTypes()
    {
        return getAllRoleTypes().stream().filter(t -> getInstance(t).canStartAs()).collect(Collectors.toSet());
    }
    /**
     * Gets all the available non-Coven-DLC-exclusive role types in the game that the player can start as.
     */
    public Collection<Class<? extends Role>> getAllStartableClassicRoleTypes()
    {
        return getAllStartableRoleTypes().stream().filter(t -> !getInstance(t).covenDLCExclusive()).collect(Collectors.toSet());
    }
    /**
     * Gets all the role types in the game that are available in the Coven DLC that the player can start as.
     */
    public Collection<Class<? extends Role>> getAllStartableCovenRoleTypes()
    {
        return getAllStartableRoleTypes().stream().filter(t -> !getInstance(t).unavailableInCovenDLC()).collect(Collectors.toSet());
    }

    /**
     * Gets all the available role types in the provided game packs that the player can start as.
     * @param packTypes The game pack types of the game whose available startable roles to get.
     * @return The collection of all the available role types in the provided game packs.
     */
    public Collection<Class<? extends Role>> getAllStartableRoleTypes(GamePackTypes packTypes)
    {
        if (packTypes == GamePackTypes.Classic)
            return getAllStartableClassicRoleTypes();
        if (packTypes == GamePackTypes.Coven)
            return getAllStartableCovenRoleTypes();
        if (packTypes == GamePackTypes.All)
            return getAllStartableRoleTypes();
        return null;
    }
    /**
     * Gets the intersection of all the available role types in the provided game packs that the player can start as.
     * @param packTypes The game pack types of the game whose available startable roles to get.
     * @return The intersection of all the available role types in the provided game packs.
     */
    public Collection<Class<? extends Role>> getAllStartableRoleTypesIntersection(GamePackTypes packTypes)
    {
        if (packTypes == null || packTypes.code == 0)
            return Collections.emptyList();

        var result = new HashSet<>(getAllStartableRoleTypes());

        for (int flag = 1; flag < GamePackTypes.All.code; flag <<= 1)
        {
            if ((packTypes.code & flag) == 0)
                continue;

            result.retainAll(getAllStartableRoleTypes(GamePackTypes.getTypeFromCode(flag)));
        }

        return result;
    }

    @Override
    public int size() { return count; }
    @Override
    public boolean isEmpty() { return count == 0; }

    @Override
    public boolean contains(Object o)
    {
        if (!(o instanceof Class))
            return false;
        var type = (Class<? extends Role>)o;
        var instance = getInstance(type);
        if (instance == null)
            return false;

        return roleTypesByAlignment.get(instance.getFullAlignment()).contains(type);
    }

    @Override
    public boolean containsAll(Collection<?> collection)
    {
        for (var type : collection)
            if (!contains(type))
                return false;
        return true;
    }

    /**
     * Adds a role type to this collection, if it does not exist.
     * @param role The role type to add.
     * @return {@code true} if the role type was successfully added, otherwise {@code false}.
     */
    public boolean add(Role role)
    {
        return addConditionallyValidated(role.getClass(), false);
    }
    /**
     * Adds a role type to this collection, if it does not exist.
     * @param role The role type to add.
     * @return {@code true} if the role type was successfully added, otherwise {@code false}.
     */
    @Override
    public boolean add(Class<? extends Role> role)
    {
        return addConditionallyValidated(role, true);
    }

    /**
     * Removes a role from this collection, if it exists.
     * @param role The role type to remove.
     * @return {@code true} if the role type was successfully removed, otherwise {@code false}.
     */
    public boolean remove(Role role)
    {
        return remove(role.getClass());
    }
    /**
     * Removes a role from this collection, if it exists.
     * @param role The role type to remove.
     * @return {@code true} if the role type was successfully removed, otherwise {@code false}.
     */
    public boolean remove(Class<? extends Role> role)
    {
        var instance = getInstance(role);
        if (instance == null)
            return false;

        if (!roleTypesByAlignment.get(instance.getFullAlignment()).remove(role))
            return false;

        roleTypesByFaction.get(instance.getFaction()).remove(role);

        count--;
        return true;
    }
    @Override
    public boolean remove(Object o)
    {
        return remove((Class<? extends Role>)o);
    }

    @Override
    public boolean addAll(Collection<? extends Class<? extends Role>> collection)
    {
        boolean result = false;
        for (var type : collection)
            result |= add(type);
        return result;
    }

    @Override
    public boolean removeAll(Collection<?> collection)
    {
        boolean result = false;
        for (var type : collection)
            result |= remove(type);
        return result;
    }

    @Override
    public boolean retainAll(Collection<?> collection)
    {
        var types = toArray();
        boolean result = false;
        for (var t : types)
            if (!collection.contains(t))
                result |= remove(t);
        return result;
    }

    /**
     * Clears this collection.
     */
    public void clear()
    {
        roleTypesByAlignment.clear();
        roleTypesByFaction.clear();
        count = 0;
    }

    /**
     * Removes all role types that belong to the specified faction from this collection.
     * @param faction The faction that the roles to remove belong to.
     */
    public void clearFaction(Faction faction)
    {
        var factionRoles = roleTypesByFaction.get(faction);
        var rolesToRemove = new ArrayList<>(factionRoles);
        factionRoles.clear();
        removeAll(rolesToRemove);
        count -= rolesToRemove.size();
    }
    /**
     * Removes all role types that belong to the specified alignment from this collection.
     * @param alignment The alignment that the roles to remove belong to.
     */
    public void clearAlignment(RoleAlignment alignment)
    {
        var alignmentRoles = roleTypesByAlignment.get(alignment);
        var rolesToRemove = new ArrayList<>(alignmentRoles);
        alignmentRoles.clear();
        removeAll(rolesToRemove);
        count -= rolesToRemove.size();
    }

    @Override
    public Iterator<Class<? extends Role>> iterator()
    {
        return new RoleCollectionIterator();
    }

    @Override
    public Object[] toArray()
    {
        var result = new Object[count];

        int current = 0;
        for (var values : roleTypesByFaction.values())
            for (var type : values)
            {
                result[current] = type;
                current++;
            }

        return result;
    }
    @Override
    public <T> T[] toArray(T[] ts)
    {
        if (!ts.getClass().getComponentType().isAssignableFrom(Class.class))
            throw new ArrayStoreException("The provided type to store the role types is invalid.");

        int current = 0;
        for (var values : roleTypesByFaction.values())
            for (var type : values)
            {
                // Trust me, it *is* checked
                ts[current] = (T)type;
                current++;
            }

        return ts;
    }

    private boolean addConditionallyValidated(Class<? extends Role> role, boolean validate)
    {
        if (validate)
            if (!Role.isValidRoleType(role))
                return false;

        var instance = getInstance(role);

        roleTypesByAlignment.get(instance.getFullAlignment()).add(role);
        roleTypesByFaction.get(instance.getFaction()).add(role);

        count++;
        return true;
    }

    private Role getInstance(Class<? extends Role> roleType)
    {
        return RoleInstancePool.getInstance().getInstance(roleType);
    }

    /**
     * Initializes a new instance of the {@link RoleCollection} class with the roles from the specified {@link Role}
     * instances.
     * @param roles The instances of the roles to add to the collection.
     * @return The initialized instance.
     */
    public static RoleCollection fromRoles(Collection<Role> roles)
    {
        var collection = new RoleCollection();
        for (var r : roles)
            collection.addConditionallyValidated(r.getClass(), false);
        return collection;
    }

    private class RoleCollectionIterator
        implements Iterator<Class<? extends Role>>
    {
        private final Iterator<HashSet<Class<? extends Role>>> factionIterator;
        private Iterator<Class<? extends Role>> setIterator;

        public RoleCollectionIterator()
        {
            factionIterator = roleTypesByFaction.values().iterator();
        }

        @Override
        public boolean hasNext()
        {
            if (factionIterator.hasNext())
                return true;

            if (setIterator == null)
                return false;
            return setIterator.hasNext();
        }

        @Override
        public Class<? extends Role> next()
        {
            if (!setIterator.hasNext())
                setIterator = factionIterator.next().iterator();
            return setIterator.next();
        }
    }
}
