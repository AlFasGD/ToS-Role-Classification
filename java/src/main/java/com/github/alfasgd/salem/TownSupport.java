package com.github.alfasgd.salem;

public interface TownSupport
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.TownSupport; }
}

