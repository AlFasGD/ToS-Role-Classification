namespace TheSalem
{
    public abstract class TownKilling : Role
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.TownKilling;
    }

    public sealed class Jailor : TownKilling
    {
        public override bool IsUnique => true;
    }
    public sealed class VampireHunter : TownKilling { }
    public sealed class Veteran : TownKilling
    {
        public override bool IsUnique => true;
    }
    public sealed class Vigilante : TownKilling { }
}
