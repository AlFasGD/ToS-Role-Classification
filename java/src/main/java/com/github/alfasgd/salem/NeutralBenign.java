package com.github.alfasgd.salem;

public interface NeutralBenign
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.NeutralBenign; }
}

