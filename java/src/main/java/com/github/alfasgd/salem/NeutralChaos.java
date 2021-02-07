package com.github.alfasgd.salem;

public interface NeutralChaos
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.NeutralChaos; }
}

