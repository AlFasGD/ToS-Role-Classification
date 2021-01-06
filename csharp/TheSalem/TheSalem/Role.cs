using Garyon.Extensions;
using System;

namespace TheSalem
{
    public abstract class Role
    {
        public string Name => GetType().Name.GetPascalCaseWords().CombineWords();

        public abstract RoleAlignment FullAlignment { get; }
        public Faction Faction => FullAlignment.Faction;
        public Alignment Alignment => FullAlignment.Alignment;

        public virtual bool CanStartAs => true;
        public virtual bool IsUnique => false;
        public virtual int MaximumOccurrences => IsUnique ? 1 : 15;

        public bool CanPromote => PromotesInto is not null;
        public virtual Type PromotesInto => null;

        public virtual bool CovenDLCExclusive => false;
        public virtual bool NotInCovenDLC => false;
    }
}
