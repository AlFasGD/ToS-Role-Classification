namespace TheSalem
{
    public abstract class MafiaSupport : MafiaNonKilling
    {
        public sealed override RoleAlignment FullAlignment => RoleAlignment.MafiaSupport;
    }

    public sealed class Blackmailer : MafiaSupport { }
    public sealed class Consigliere : MafiaSupport { }
    public sealed class Consort : MafiaSupport { }
}
