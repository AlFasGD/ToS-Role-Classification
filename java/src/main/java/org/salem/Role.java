package org.salem;

import org.salem.common.Casing;

public interface Role
{
    public default String getRoleName()
    {
        return Casing.getCamelCaseWords(getClass().getSimpleName());
    }

    public abstract RoleAlignment getFullAlignment();
    public default Faction getFaction() { return getFullAlignment().RoleFaction; }
    public default Alignment getAlignment() { return getFullAlignment().RoleAlignment; }

    public default boolean canStartAs() { return true; }
    public default boolean isUnique() { return false; }
    public default int getMaximumOccurrences() { return isUnique() ? 1 : 15; }

    public default boolean canPromote() { return promotesInto() != null; }
    public default Class<?> promotesInto() { return null; }

    public default boolean covenDLCExclusive() { return false; }
    public default boolean unavailableInCovenDLC() { return false; }
}