namespace TheSalem
{
    public abstract class TownProtective : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.TownProtective;
    }

    public sealed class Bodyguard : TownProtective { }
    public sealed class Crusader : TownProtective
    {
        public override bool CovenDLCExclusive => true;
    }
    public sealed class Doctor : TownProtective { }
    public sealed class Trapper : TownProtective
    {
        public override bool CovenDLCExclusive => true;
    }
}
