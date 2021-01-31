using System;

namespace TheSalem
{
    public abstract class NeutralChaos : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.NeutralChaos;
    }

    public sealed class Pirate : NeutralChaos
    {
        public override bool IsUnique => true;

        public override bool CovenDLCExclusive => true;
    }
    public sealed class Plaguebearer : NeutralChaos
    {
        public override bool IsUnique => true;

        public override bool CovenDLCExclusive => true;

        public override Type PromotesInto => typeof(Pestilence);
    }
    public sealed class Pestilence : NeutralChaos
    {
        public override bool IsUnique => true;

        public override bool CovenDLCExclusive => true;

        public override bool CanStartAs => false;
    }
    public sealed class Vampire : NeutralChaos { }
}
