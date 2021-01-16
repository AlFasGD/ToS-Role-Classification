package org.salem;

public interface NonStartingRole
    extends Role
{
    @Override
    public default boolean canStartAs() { return false; }
}
