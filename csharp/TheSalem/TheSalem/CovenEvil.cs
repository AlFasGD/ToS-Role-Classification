namespace TheSalem
{
    public abstract class CovenEvil : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.CovenEvil;

        public sealed override bool IsUnique => true;

        public sealed override bool CovenDLCExclusive => true;
    }

    public sealed class CovenLeader : CovenEvil { }
    public sealed class HexMaster : CovenEvil { }
    public sealed class Medusa : CovenEvil { }
    public sealed class Necromancer : CovenEvil { }
    public sealed class Poisoner : CovenEvil { }
    public sealed class PotionMaster : CovenEvil { }
}
