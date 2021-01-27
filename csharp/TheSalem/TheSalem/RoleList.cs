using Garyon.DataStructures;
using Garyon.Exceptions;
using Garyon.Extensions;
using Garyon.Extensions.ArrayExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSalem
{
    /// <summary>Represents a role list. It may contain either fixed role slots, or wildcard role slots.</summary>
    public class RoleList
    {
        private readonly IRoleSlot[] roleSlots;

        /// <summary>Gets a copy of the internally stored role slots array.</summary>
        public IRoleSlot[] RoleSlots => roleSlots.CopyArray();

        /// <summary>Gets all the fixed role slots.</summary>
        public IEnumerable<Role> FixedRoleSlots => roleSlots.Where(slot => slot is Role).Cast<Role>();
        /// <summary>Gets all the wildcard role slots.</summary>
        public IEnumerable<RoleAlignment> WildcardRoleSlots => roleSlots.Where(slot => slot is RoleAlignment).Cast<RoleAlignment>();

        public RoleList(IRoleSlot[] slots)
        {
            ValidateGivenSlotCount(slots.Length);
            roleSlots = slots.CopyArray();
        }
        public RoleList(ICollection<IRoleSlot> slots)
        {
            ValidateGivenSlotCount(slots.Count);
            roleSlots = slots.ToArray();
        }
        public RoleList(RoleList other)
        {
            roleSlots = other.roleSlots.CopyArray();
        }

        private void ValidateGivenSlotCount(int count)
        {
            if (count > 15)
                ThrowHelper.Throw<ArgumentException>("The slot collection may not contain more than 15 slots.");
        }

        /// <summary>Gets a <seealso cref="ValueCounterDictionary{TKey}"/> that contains the number of occurrences of each role in this list. This ignores the <seealso cref="RoleAlignment"/> slots.</summary>
        /// <returns>The <seealso cref="ValueCounterDictionary{TKey}"/> containing the number of occurrences of each role in this list.</returns>
        public ValueCounterDictionary<Type> GetRoleOccurrences()
        {
            var result = new ValueCounterDictionary<Type>();
            FixedRoleSlots.ForEach(role => result.Add(role.GetType()));
            return result;
        }
        /// <summary>Gets a <seealso cref="HashSet{T}"/> that contains the distinct role types that are present in this list. This ignores the <seealso cref="RoleAlignment"/> slots.</summary>
        /// <returns>The <seealso cref="HashSet{T}"/> containing the distinct role types that are present in this list.</returns>
        public HashSet<Type> GetDistinctRoles()
        {
            return new(GetRoleOccurrences().Keys);
        }

        /// <summary>Determines whether this current role list is a valid one.</summary>
        /// <param name="packTypes">The game pack types where this role list is applied on.</param>
        public bool IsValidRoleList(GamePackTypes packTypes) => ValidateRoleList(packTypes, out _);

        /// <summary>Generates a random role list from this given role list, replacing wildcard slots with randomly chosen ones fitting the given criteria.</summary>
        /// <param name="packTypes">The game pack types where this role list is applied on.</param>
        /// <returns>The generated role list if this role list is valid, otherwise <see langword="null"/>.</returns>
        public RoleList GenerateRandomRoleList(GamePackTypes packTypes)
        {
            return GenerateRandomRoleList(new RoleDictionary(RoleDictionary.AllAvailableRolesDictionary.GetAllStartableRoleTypesIntersection(packTypes)));
        }
        /// <summary>Generates a random role list from this given role list, replacing wildcard slots with randomly chosen ones fitting the given criteria.</summary>
        /// <param name="availableRoles">The available roles dictionary.</param>
        /// <returns>The generated role list if this role list is valid, otherwise <see langword="null"/>.</returns>
        public RoleList GenerateRandomRoleList(RoleDictionary availableRoles)
        {
            if (!ValidateRoleList(availableRoles, out var remainingSlots))
                return null;

            var availableRoleTypes = new RoleDictionary(remainingSlots.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Key));

            var random = new Random();
            var resultingArray = roleSlots.ToArray();
            for (int i = 0; i < resultingArray.Length; i++)
            {
                var roleEntry = resultingArray[i];

                var role =  roleEntry.GenerateRandomRole(availableRoleTypes, random);
                resultingArray[i] = role;

                var type = role.GetType();
                remainingSlots.Subtract(type);

                if (remainingSlots[type] <= 0)
                    availableRoleTypes.Remove(role);
            }

            return new(resultingArray);
        }

        private bool ValidateRoleList(GamePackTypes packTypes, out ValueCounterDictionary<Type> remainingSlots)
        {
            return ValidateRoleList(new RoleDictionary(RoleDictionary.AllAvailableRolesDictionary.GetAllStartableRoleTypesIntersection(packTypes)), out remainingSlots);
        }
        private bool ValidateRoleList(RoleDictionary availableRoles, out ValueCounterDictionary<Type> remainingSlots)
        {
            remainingSlots = new();
            // This exists to prevent impossible role lists like more than 6 Coven roles in a game (since all the Coven roles are unique)
            var remainingAlignmentSlots = new ValueCounterDictionary<Faction>();

            foreach (var t in availableRoles)
            {
                var instance = RoleInstancePool.Instance[t];
                remainingSlots.Add(t, instance.MaximumOccurrences);
                remainingAlignmentSlots.Add(instance.Faction, instance.MaximumOccurrences);
            }

            foreach (var slot in roleSlots)
            {
                if (slot is RoleAlignment alignment) { }
                else
                {
                    var role = slot as Role;

                    // This is probably the greatest usage of inline variable declarations via is
                    alignment = role.FullAlignment;
                    var type = role.GetType();
                    remainingSlots.Subtract(type);
                    if (remainingSlots[type] < 0)
                        return false;
                }

                var faction = alignment.Faction;

                if (faction == Faction.Any)
                    continue;

                remainingAlignmentSlots.Subtract(faction);
                if (remainingAlignmentSlots[faction] < 0)
                    return false;
            }

            return true;
        }

        /// <summary>Gets the <seealso cref="IRoleSlot"/> at the specified index in this list.</summary>
        /// <param name="index">The index of the <seealso cref="IRoleSlot"/> to get.</param>
        /// <returns>The <seealso cref="IRoleSlot"/> at the specified index.</returns>
        public IRoleSlot this[int index] => roleSlots[index];
    }
}
