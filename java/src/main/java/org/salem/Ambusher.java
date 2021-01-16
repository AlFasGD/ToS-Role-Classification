package org.salem;

public final class Ambusher
    implements MafiaKilling
{
    @Override
    public Class<?> promotesInto() { return Mafioso.class; }
}
