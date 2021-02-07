package com.github.alfasgd.salem;

public interface MafiaSupport
    extends MafiaNonKilling
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.MafiaSupport; }
}

