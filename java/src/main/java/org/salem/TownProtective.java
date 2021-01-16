package org.salem;

public interface TownProtective
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.TownProtective; }
}

