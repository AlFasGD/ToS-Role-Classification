package com.github.alfasgd.salem;

import org.reflections.Reflections;

import java.util.HashMap;

public final class RoleInstancePool
{
    private static final RoleInstancePool instance;

    public static RoleInstancePool getInstance() { return instance; }

    static
    {
        instance = new RoleInstancePool(new Reflections("org.salem"));
    }

    private final HashMap<Class<? extends Role>, Role> roleTypeInstances = new HashMap<>();

    private RoleInstancePool(Reflections reflections)
    {
        var roleTypes = reflections.getSubTypesOf(Role.class);
        roleTypes.removeIf(t -> !Role.isValidRoleType(t));
        for (var t : roleTypes)
            registerConditionallyChecked(t, false);
    }

    public HashMap<Class<? extends Role>, Role> getAllRoleTypes() { return new HashMap<>(roleTypeInstances); }

    public int getRoleTypeCount() { return roleTypeInstances.size(); }

    public Role getInstance(Class<? extends Role> type) { return roleTypeInstances.get(type); }

    public boolean register(Class<? extends Role> roleType)
    {
        return registerConditionallyChecked(roleType, true);
    }
    private boolean registerConditionallyChecked(Class<? extends Role> roleType, boolean check)
    {
        if (check)
            if (!Role.isValidRoleType(roleType))
                return false;

        if (roleTypeInstances.get(roleType) != null)
            return false;

        try
        {
            var ctor = roleType.getConstructor();
            var instance = (Role)ctor.newInstance();

            roleTypeInstances.put(roleType, instance);
            return true;
        }
        catch (Exception ignored) { }
        return false;
    }
}
