namespace TheSalem
{
    public abstract class TownSupport : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.TownSupport;
    }

    public sealed class Escort : TownSupport { }
    public sealed class Mayor : TownSupport
    {
        public override bool IsUnique => true;
    }
    public sealed class Medium : TownSupport { }
    public sealed class Retributionist : TownSupport
    {
        public override bool IsUnique => true;
    }
    public sealed class Transporter : TownSupport { }
}
