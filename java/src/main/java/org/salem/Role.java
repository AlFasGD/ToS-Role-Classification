package org.salem;

import org.salem.common.Casing;

public abstract class Role
{
    public String getRoleName()
    {
        return Casing.getCamelCaseWords(getClass().getName());
    }

    public abstract RoleAlignment getFullAlignment();
    public Faction getFaction() { return getFullAlignment().RoleFaction; }
    public Alignment getAlignment() { return getFullAlignment().RoleAlignment; }
    
    public boolean canStartAs() { return true; }
    public boolean isUnique() { return false; }
    public int getMaximumOccurrences() { return isUnique() ? 1 : 15; }
    
    public boolean canPromote() { return promotesInto() != null; }
    public Class<?> promotesInto() { return null; }
    
    public boolean covenDLCExclusive() { return false; }
    public boolean unavailableInCovenDLC() { return false; }
}

