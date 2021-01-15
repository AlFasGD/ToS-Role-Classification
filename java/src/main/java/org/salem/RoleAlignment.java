package org.salem;

public class RoleAlignment
{
    // Available role alignments
    public static final RoleAlignment TownInvestigative = new RoleAlignment(Faction.Town, Alignment.Investigative);
    public static final RoleAlignment TownKilling = new RoleAlignment(Faction.Town, Alignment.Killing);
    public static final RoleAlignment TownProtective = new RoleAlignment(Faction.Town, Alignment.Protective);
    public static final RoleAlignment TownSupport = new RoleAlignment(Faction.Town, Alignment.Support);

    public static final RoleAlignment MafiaDeception = new RoleAlignment(Faction.Mafia, Alignment.Deception);
    public static final RoleAlignment MafiaKilling = new RoleAlignment(Faction.Mafia, Alignment.Killing);
    public static final RoleAlignment MafiaSupport = new RoleAlignment(Faction.Mafia, Alignment.Support);

    public static final RoleAlignment CovenEvil = new RoleAlignment(Faction.Coven, Alignment.Evil);

    public static final RoleAlignment NeutralBenign = new RoleAlignment(Faction.Neutral, Alignment.Benign);
    public static final RoleAlignment NeutralKilling = new RoleAlignment(Faction.Neutral, Alignment.Killing);
    public static final RoleAlignment NeutralEvil = new RoleAlignment(Faction.Neutral, Alignment.Evil);
    public static final RoleAlignment NeutralChaos = new RoleAlignment(Faction.Neutral, Alignment.Chaos);
    
    public final Faction RoleFaction;
    public final Alignment RoleAlignment;

    public RoleAlignment(Faction faction, Alignment alignment)
    {
        RoleFaction = faction;
        RoleAlignment = alignment;
    }

    @Override
    public String toString()
    {
        return RoleFaction + " " + RoleAlignment;
    }
    @Override
    public int hashCode()
    {
        return (RoleFaction.ordinal()) | (RoleAlignment.ordinal() << 2);
    }
}
