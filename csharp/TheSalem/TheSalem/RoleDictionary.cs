using Garyon.DataStructures;
using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TheSalem
{
    /// <summary>Represents a role type dictionary. Provides the ability to index the avaiable roles by their faction or alignment.</summary>
    public class RoleDictionary : IEnumerable<Type>
    {
        private static readonly RoleDictionary defaultDictionary;

        /// <summary>Initializes a new role type dictionary with all the avaiable roles in the game.</summary>
        public static RoleDictionary AllAvailableRolesDictionary => new(defaultDictionary);

        static RoleDictionary()
        {
            defaultDictionary = new(RoleInstancePool.Instance.AllRoleTypes.Keys);
        }

        // Consider using FlexibleHashSetDictionary, once they are added to Garyon
        private readonly FlexibleListDictionary<RoleAlignment, Type> roleTypesByAlignment;
        private readonly FlexibleListDictionary<Faction, Type> roleTypesByFaction;

        /// <summary>Gets all the available role types in the game.</summary>
        public IEnumerable<Type> AllRoleTypes => roleTypesByAlignment.Values.Flatten();
        /// <summary>Gets all the available role types in the game that the player can start as.</summary>
        public IEnumerable<Type> AllStartableRoleTypes => AllRoleTypes.Where(t => RoleInstancePool.Instance[t].CanStartAs);

        /// <summary>Initializes a new instance of the <seealso cref="RoleDictionary"/> class with no roles.</summary>
        public RoleDictionary()
        {
            roleTypesByAlignment = new();
            roleTypesByFaction = new();
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleDictionary"/> class with the roles from the specified types.</summary>
        /// <param name="roleTypes">The types of the roles to add to the dictionary.</param>
        public RoleDictionary(IEnumerable<Type> roleTypes)
        {
            roleTypes = roleTypes.Where(Role.IsValidRoleType);
            foreach (var t in roleTypes)
                AddConditionallyChecked(t, false);
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleDictionary"/> class with the roles from the specified <seealso cref="Role"/> instances.</summary>
        /// <param name="roles">The instances of the roles to add to the dictionary.</param>
        public RoleDictionary(IEnumerable<Role> roles)
        {
            foreach (var r in roles)
                AddConditionallyChecked(r.GetType(), false);
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleDictionary"/> class from another <seealso cref="RoleDictionary"/> insatnce.</summary>
        /// <param name="other">The other <seealso cref="RoleDictionary"/> instance to copy.</param>
        public RoleDictionary(RoleDictionary other)
        {
            roleTypesByAlignment = new(other.roleTypesByAlignment);
            roleTypesByFaction = new(other.roleTypesByFaction);
        }

        /// <summary>Adds a role type to this dictionary, if it does not exist.</summary>
        /// <param name="role">The role type to add.</param>
        /// <returns><see langword="true"/> if the role type was successfully added, otherwise <see langword="false"/>.</returns>
        public bool Add(Type role)
        {
            return AddConditionallyChecked(role, true);
        }
        /// <summary>Removes a role from this dictionary, if it exists.</summary>
        /// <param name="role">The role type to remove.</param>
        /// <returns><see langword="true"/> if the role type was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool Remove(Type role)
        {
            var instance = RoleInstancePool.Instance[role];
            if (instance == null)
                return false;

            roleTypesByAlignment[instance.FullAlignment].Remove(role);
            roleTypesByFaction[instance.Faction].Remove(role);

            return true;
        }

        /// <summary>Adds a collection of role types to this dictionary.</summary>
        /// <param name="roles">The role types to add.</param>
        public void AddRange(IEnumerable<Type> roles)
        {
            foreach (var r in roles)
                Add(r);
        }
        /// <summary>Removes a collection of role types from this dictionary, if they exist.</summary>
        /// <param name="roles">The role types to remove.</param>
        public void RemoveRange(IEnumerable<Type> roles)
        {
            foreach (var r in roles)
                Remove(r);
        }

        /// <summary>Clears this dictionary.</summary>
        public void Clear()
        {
            roleTypesByAlignment.Clear();
            roleTypesByFaction.Clear();
        }
        /// <summary>Removes all role types that belong to the specified faction from this dictionary.</summary>
        /// <param name="faction">The faction that the roles to remove belong to.</param>
        public void ClearFaction(Faction faction)
        {
            var rolesToRemove = roleTypesByFaction[faction].ToArray();
            RemoveRange(rolesToRemove);
        }
        /// <summary>Removes all role types that belong to the specified alignment from this dictionary.</summary>
        /// <param name="alignment">The alignment that the roles to remove belong to.</param>
        public void ClearAlignment(RoleAlignment alignment)
        {
            var rolesToRemove = roleTypesByAlignment[alignment].ToArray();
            RemoveRange(rolesToRemove);
        }

        private bool AddConditionallyChecked(Type role, bool check)
        {
            if (check)
                if (!Role.IsValidRoleType(role))
                    return false;

            var instance = RoleInstancePool.Instance[role];

            roleTypesByAlignment.Add(instance.FullAlignment, role);
            roleTypesByFaction.Add(instance.Faction, role);

            return true;
        }

        #region IEnumerable Implementation
        public IEnumerator<Type> GetEnumerator() => AllRoleTypes.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        /// <summary>Gets all the roles that belong to a faction.</summary>
        /// <param name="faction">The faction.</param>
        /// <returns>A collection of elements that belong to the provided faction.</returns>
        public IEnumerable<Type> this[Faction faction] => roleTypesByFaction[faction].ToArray();
        /// <summary>Gets all the roles that belong to an alignment.</summary>
        /// <param name="alignment">The alignment.</param>
        /// <returns>A collection of elements that belong to the provided alignment.</returns>
        public IEnumerable<Type> this[RoleAlignment alignment] => roleTypesByAlignment[alignment].ToArray();

    }
}
