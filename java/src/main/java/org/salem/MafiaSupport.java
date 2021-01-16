package org.salem;

public interface MafiaSupport
    extends MafiaNonKilling
{
    @Override
    public default RoleAlignment getFullAlignment() { return RoleAlignment.MafiaSupport; }
}

