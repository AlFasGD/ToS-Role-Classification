using static TheSalem.Alignment;
using static TheSalem.Faction;

namespace TheSalem
{
    public record RoleAlignment(Faction Faction, Alignment Alignment)
    {
        #region Available role alignments
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

        public override string ToString() => $"{Faction} {Alignment}";
    }
}
