package org.salem;

public interface NeutralEvil
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.NeutralEvil; }
}

