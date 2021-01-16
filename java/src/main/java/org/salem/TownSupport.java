package org.salem;

public interface TownSupport
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.TownSupport; }
}

