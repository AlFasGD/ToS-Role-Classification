package org.salem;

public interface NeutralBenign
    extends Role
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.NeutralBenign; }
}

