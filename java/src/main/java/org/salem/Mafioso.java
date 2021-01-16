package org.salem;

public final class Mafioso
    implements MafiaKilling
{
    @Override
    public Class<?> promotesInto() { return Godfather.class; }
}
