package com.github.alfasgd.salem;

public final class Ambusher
    implements MafiaKilling
{
    @Override
    public Class<?> promotesInto() { return Mafioso.class; }
}
