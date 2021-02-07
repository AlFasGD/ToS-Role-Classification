package com.github.alfasgd.salem;

import java.util.Random;

public class RoleAlignment
    implements RoleSlot
{
    // Available role alignments
    public static final RoleAlignment Any = new RoleAlignment();

    public static final RoleAlignment TownAny = new RoleAlignment(Faction.Town);
    public static final RoleAlignment MafiaAny = new RoleAlignment(Faction.Mafia);
    public static final RoleAlignment CovenAny = new RoleAlignment(Faction.Coven);
    public static final RoleAlignment NeutralAny = new RoleAlignment(Faction.Neutral);
    
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
    
    public final Faction roleFaction;
    public final Alignment roleAlignment;

    public RoleAlignment()
    {
        this(Faction.Any, Alignment.Any);
    }
    public RoleAlignment(Faction faction)
    {
        this(faction, Alignment.Any);
    }
    public RoleAlignment(Alignment alignment)
    {
        this(Faction.Any, alignment);
    }
    public RoleAlignment(Faction faction, Alignment alignment)
    {
        roleFaction = faction;
        roleAlignment = alignment;
    }

    /**
     * Determines whether this role alignment matches the given filter.
     * @param alignment The alignment filter to check whether it matches this alignment.
     * @return {@code true} if the non-any fields of the alignment filter are equal to this alignment, otherwise {@code
     *         false}.
     */
    public boolean matchesFilter(RoleAlignment alignment)
    {
        if (alignment.roleFaction != Faction.Any)
            if (alignment.roleFaction != roleFaction)
                return false;

        if (alignment.roleAlignment != Alignment.Any)
            if (alignment.roleAlignment != roleAlignment)
                return false;

        return true;
    }

    @Override
    public Role generateRandomRole(RoleCollection availableRoles, Random random)
    {
        Class<? extends Role>[] roles = availableRoles.get(this);
        return RoleInstancePool.getInstance().getInstance(roles[random.nextInt(roles.length)]);
    }

    @Override
    public String toString()
    {
        if (equals(Any))
            return "Any";

        return roleFaction + " " + roleAlignment;
    }
    @Override
    public boolean equals(Object obj)
    {
        if (!(obj instanceof RoleAlignment))
            return false;

        var other = (RoleAlignment)obj;
        return roleFaction == other.roleFaction && roleAlignment == other.roleAlignment;
    }
    @Override
    public int hashCode()
    {
        return (roleFaction.ordinal()) | (roleAlignment.ordinal() << 3);
    }
}
