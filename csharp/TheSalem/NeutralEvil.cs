using System;

namespace TheSalem
{
    public abstract class NeutralEvil : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.NeutralEvil;
    }

    public sealed class Executioner : NeutralEvil
    {
        public override Type PromotesInto => typeof(Jester);
    }
    public sealed class Jester : NeutralEvil { }
    public sealed class Witch : NeutralEvil
    {
        public override bool NotInCovenDLC => true;
    }
}
