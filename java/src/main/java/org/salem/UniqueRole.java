package org.salem;

public interface UniqueRole
    extends Role
{
    @Override
    public default boolean isUnique() { return true; }
}
