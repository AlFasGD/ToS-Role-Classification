package com.github.alfasgd.salem;

public interface MafiaDeception
    extends MafiaNonKilling
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.MafiaDeception; }
}

