namespace TheSalem
{
    public abstract class MafiaDeception : MafiaNonKilling
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.MafiaDeception;
    }

    public sealed class Disguiser : MafiaDeception { }
    public sealed class Forger : MafiaDeception { }
    public sealed class Framer : MafiaDeception { }
    public sealed class Hypnotist : MafiaDeception { }
    public sealed class Janitor : MafiaDeception { }
}
