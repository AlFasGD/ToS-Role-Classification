using Garyon.Extensions;
using System;

namespace TheSalem
{
    public abstract class Role : IRoleSlot
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

        /// <summary>Returns this instance. This implementation does not perform any unnecessary computations.</summary>
        /// <param name="availableRoles">This argument is ignored in this implementation.</param>
        /// <param name="random">This argument is ignored in this implementation.</param>
        /// <returns>This <seealso cref="Role"/> instance.</returns>
        public Role GenerateRandomRole(RoleDictionary availableRoles, Random random) => this;

        public static bool IsValidRoleType(Type type) => type.IsSealed && type.IsAssignableTo(typeof(Role));
    }
}
