using System;

namespace TheSalem
{
    public abstract class MafiaKilling : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.MafiaKilling;

        public sealed override bool IsUnique => true;
    }

    public sealed class Ambusher : MafiaKilling
    {
        public override Type PromotesInto => typeof(Mafioso);
    }
    public sealed class Godfather : MafiaKilling { }
    public sealed class Mafioso : MafiaKilling
    {
        public override Type PromotesInto => typeof(Godfather);
    }
}
