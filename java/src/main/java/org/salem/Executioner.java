package org.salem;

public final class Executioner
    implements NeutralEvil
{
    @Override
    public Class<?> promotesInto() { return Jester.class; }
}
