package com.github.alfasgd.salem;

public interface MafiaKilling
    extends UniqueRole
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.MafiaKilling; }
}
