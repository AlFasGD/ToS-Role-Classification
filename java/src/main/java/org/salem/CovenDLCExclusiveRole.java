package org.salem;

public interface CovenDLCExclusiveRole
    extends Role
{
    @Override
    public default boolean covenDLCExclusive() { return true; }
}

