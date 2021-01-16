package org.salem;

public final class Plaguebearer
    implements NeutralChaos, UniqueRole, CovenDLCExclusiveRole
{
    @Override
    public Class<?> promotesInto() { return Pestilence.class; }
}
