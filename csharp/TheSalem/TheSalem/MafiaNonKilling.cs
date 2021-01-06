using System;

namespace TheSalem
{
    public abstract class MafiaNonKilling : Role
    {
        public sealed override Type PromotesInto => typeof(Mafioso);
    }
}
