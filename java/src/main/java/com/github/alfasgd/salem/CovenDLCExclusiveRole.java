package com.github.alfasgd.salem;

public interface CovenDLCExclusiveRole
    extends Role
{
    @Override
    public default boolean covenDLCExclusive() { return true; }
}

