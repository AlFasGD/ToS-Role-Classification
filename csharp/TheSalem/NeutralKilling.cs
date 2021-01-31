namespace TheSalem
{
    public abstract class NeutralKilling : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.NeutralKilling;
    }

    public sealed class Arsonist : NeutralKilling { }
    public sealed class Juggernaut : NeutralKilling
    {
        public override bool CovenDLCExclusive => true;

        public override bool IsUnique => true;
    }
    public sealed class SerialKiller : NeutralKilling { }
    public sealed class Werewolf : NeutralKilling
    {
        public override bool IsUnique => true;
    }
}
