namespace TheSalem
{
    public abstract class NeutralBenign : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.NeutralBenign;
    }

    public sealed class Amnesiac : NeutralBenign { }
    public sealed class GuardianAngel : NeutralBenign
    {
        public override bool CovenDLCExclusive => true;
    }
    public sealed class Survivor : NeutralBenign { }
}
