package org.salem;

public interface CovenEvil
    extends CovenDLCExclusiveRole, UniqueRole
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.CovenEvil; }
}
