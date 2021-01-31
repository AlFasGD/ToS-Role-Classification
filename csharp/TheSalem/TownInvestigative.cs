namespace TheSalem
{
    public abstract class TownInvestigative : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.TownInvestigative;
    }

    public sealed class Investigator : TownInvestigative { }
    public sealed class Lookout : TownInvestigative { }
    public sealed class Psychic : TownInvestigative
    {
        public override bool CovenDLCExclusive => true;
    }
    public sealed class Sheriff : TownInvestigative { }
    public sealed class Spy : TownInvestigative { }
    public sealed class Tracker : TownInvestigative
    {
        public override bool CovenDLCExclusive => true;
    }
}
