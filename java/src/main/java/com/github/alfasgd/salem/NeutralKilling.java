package com.github.alfasgd.salem;

public interface NeutralKilling
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.NeutralKilling; }
}

