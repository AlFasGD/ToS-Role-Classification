package org.salem;

public interface TownKilling
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.TownKilling; }
}

