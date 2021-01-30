using System;
using static TheSalem.Alignment;
using static TheSalem.Faction;

namespace TheSalem
{
    public record RoleAlignment(Faction Faction, Alignment Alignment) : IRoleSlot
    {
        #region Available role alignments
        public static readonly RoleAlignment Any = new();

        public static readonly RoleAlignment TownAny = new(Town);
        public static readonly RoleAlignment MafiaAny = new(Mafia);
        public static readonly RoleAlignment CovenAny = new(Coven);
        public static readonly RoleAlignment NeutralAny = new(Neutral);

        public static readonly RoleAlignment TownInvestigative = new(Town, Investigative);
        public static readonly RoleAlignment TownKilling = new(Town, Killing);
        public static readonly RoleAlignment TownProtective = new(Town, Protective);
        public static readonly RoleAlignment TownSupport = new(Town, Support);

        public static readonly RoleAlignment MafiaDeception = new(Mafia, Deception);
        public static readonly RoleAlignment MafiaKilling = new(Mafia, Killing);
        public static readonly RoleAlignment MafiaSupport = new(Mafia, Support);

        public static readonly RoleAlignment CovenEvil = new(Coven, Evil);

        public static readonly RoleAlignment NeutralBenign = new(Neutral, Benign);
        public static readonly RoleAlignment NeutralKilling = new(Neutral, Killing);
        public static readonly RoleAlignment NeutralEvil = new(Neutral, Evil);
        public static readonly RoleAlignment NeutralChaos = new(Neutral, Chaos);
        #endregion

        public RoleAlignment()
            : this(Faction.Any) { }
        public RoleAlignment(Alignment alignment)
            : this(Faction.Any, alignment) { }
        public RoleAlignment(Faction faction)
            : this(faction, Alignment.Any) { }

        public Role GenerateRandomRole(RoleCollection availableRoles, Random random)
        {
            var roles = availableRoles[this];
            return RoleInstancePool.Instance[roles[random.Next(roles.Length)]];
        }

        /// <summary>Determines whether this role alignment matches the given filter.</summary>
        /// <param name="alignment">The alignent filter to check whether it matches this alignment.</param>
        /// <returns><see langword="true"/> if the non-any fields of the alignment filter are equal to this alignment, otherwise <see langword="false"/>.</returns>
        public bool MatchesFilter(RoleAlignment alignment)
        {
            if (alignment.Faction != Faction.Any)
                if (alignment.Faction != Faction)
                    return false;

            if (alignment.Alignment != Alignment.Any)
                if (alignment.Alignment != Alignment)
                    return false;

            return true;
        }

        public override string ToString()
        {
            if (this == Any)
                return "Any";

            return $"{Faction} {Alignment}";
        }
        public override int GetHashCode() => ((int)Faction) | ((int)Alignment << 3);
    }
}
