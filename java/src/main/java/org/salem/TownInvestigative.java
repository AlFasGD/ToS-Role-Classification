package org.salem;

public interface TownInvestigative
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.TownInvestigative; }
}

