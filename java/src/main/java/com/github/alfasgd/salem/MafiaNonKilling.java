package com.github.alfasgd.salem;

public interface MafiaNonKilling
    extends Role
{
    @Override
    public default Class<?> promotesInto() { return Mafioso.class; }
}
