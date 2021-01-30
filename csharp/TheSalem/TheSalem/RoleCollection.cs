using Garyon.DataStructures;
using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TheSalem
{
    /// <summary>Represents a role type collection. Provides the ability to index the available roles by their faction or alignment.</summary>
    public class RoleCollection : ICollection<Type>
    {
        private static readonly RoleCollection defaultCollection;

        /// <summary>Initializes a new role type dictionary with all the available roles in the game.</summary>
        public static RoleCollection AllAvailableRolesCollection => new(defaultCollection);

        static RoleCollection()
        {
            defaultCollection = new(RoleInstancePool.Instance.AllRoleTypes.Keys);
        }

        // Consider using FlexibleInitializableValueDictionary, once they are added to Garyon
        private readonly FlexibleTypeHashSetDictionary<RoleAlignment> roleTypesByAlignment;
        private readonly FlexibleTypeHashSetDictionary<Faction> roleTypesByFaction;

        /// <summary>Gets a dictionary containing all the role types stored in this collection, grouped by alignment.</summary>
        /// <returns>The dictionary containing this collection's role types grouped by their alignment.</returns>
        public Dictionary<RoleAlignment, HashSet<Type>> GetRoleTypesByAlignment() => new(roleTypesByAlignment);
        /// <summary>Gets a dictionary containing all the role types stored in this collection, grouped by faction.</summary>
        /// <returns>The dictionary containing this collection's role types grouped by their faction.</returns>
        public Dictionary<Faction, HashSet<Type>> GetRoleTypesByFaction() => new(roleTypesByFaction);

        /// <summary>Gets all the available role types in the game that the player can start as.</summary>
        public IEnumerable<Type> AllStartableRoleTypes => this.Where(t => RoleInstancePool.Instance[t].CanStartAs);
        /// <summary>Gets all the available non-Coven-DLC-exclusive role types in the game that the player can start as.</summary>
        public IEnumerable<Type> AllStartableClassicRoleTypes => AllStartableRoleTypes.Where(t => !RoleInstancePool.Instance[t].CovenDLCExclusive);
        /// <summary>Gets all the role types in the game that are available in the Coven DLC that the player can start as.</summary>
        public IEnumerable<Type> AllStartableCovenRoleTypes => AllStartableRoleTypes.Where(t => !RoleInstancePool.Instance[t].NotInCovenDLC);

        /// <summary>Gets the number of role types contained in this role type collection.</summary>
        public int Count { get; private set; }

        bool ICollection<Type>.IsReadOnly => false;

        /// <summary>Initializes a new instance of the <seealso cref="RoleCollection"/> class with no roles.</summary>
        public RoleCollection()
        {
            roleTypesByAlignment = new();
            roleTypesByFaction = new();
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleCollection"/> class with the roles from the specified types.</summary>
        /// <param name="roleTypes">The types of the roles to add to the collection.</param>
        public RoleCollection(IEnumerable<Type> roleTypes)
            : this()
        {
            roleTypes = roleTypes.Where(Role.IsValidRoleType);
            foreach (var t in roleTypes)
                AddConditionallyValidated(t, false);
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleCollection"/> class with the roles from the specified <seealso cref="Role"/> instances.</summary>
        /// <param name="roles">The instances of the roles to add to the collection.</param>
        public RoleCollection(IEnumerable<Role> roles)
            : this()
        {
            foreach (var r in roles)
                AddConditionallyValidated(r.GetType(), false);
        }
        /// <summary>Initializes a new instance of the <seealso cref="RoleCollection"/> class from another <seealso cref="RoleCollection"/> instance.</summary>
        /// <param name="other">The other <seealso cref="RoleCollection"/> instance to copy.</param>
        public RoleCollection(RoleCollection other)
        {
            roleTypesByAlignment = new(other.roleTypesByAlignment);
            roleTypesByFaction = new(other.roleTypesByFaction);
        }

        /// <summary>Gets all the available role types in the provided game packs that the player can start as.</summary>
        /// <param name="packTypes">The game pack types of the game whose available startable roles to get.</param>
        /// <returns>The collection of all the available role types in the provided game packs.</returns>
        public IEnumerable<Type> GetAllStartableRoleTypes(GamePackTypes packTypes)
        {
            return packTypes switch
            {
                GamePackTypes.Classic => AllStartableClassicRoleTypes,
                GamePackTypes.Coven => AllStartableCovenRoleTypes,
                GamePackTypes.All => AllStartableRoleTypes,
            };
        }
        /// <summary>Gets the intersection of all the available role types in the provided game packs that the player can start as.</summary>
        /// <param name="packTypes">The game pack types of the game whose available startable roles to get.</param>
        /// <returns>The intersection of all the available role types in the provided game packs.</returns>
        public IEnumerable<Type> GetAllStartableRoleTypesIntersection(GamePackTypes packTypes)
        {
            if (packTypes == default) // 'is' is illegal here without specifying the type whose default literal to compare to
                return Enumerable.Empty<Type>();

            var result = AllStartableRoleTypes.ToHashSet();

            for (var flag = (GamePackTypes)1; flag < GamePackTypes.All; flag = (GamePackTypes)((int)flag << 1))
            {
                if ((packTypes & flag) == default)
                    continue;

                result.IntersectWith(GetAllStartableRoleTypes(flag));
            }

            return result;
        }

        /// <summary>Determines whether this role collection contains the specified role type.</summary>
        /// <typeparam name="T">The type of the role to check whether it is contained in the collection.</typeparam>
        /// <returns><see langword="true"/> if the role type was found in the collection, otherwise <see langword="false"/>.</returns>
        public bool Contains<T>()
            where T : Role, new()
        {
            return Contains(typeof(T));
        }
        /// <summary>Determines whether this role collection contains the specified role type.</summary>
        /// <param name="type">The type of the role to check whether it is contained in the collection.</param>
        /// <returns><see langword="true"/> if the role type was found in the collection, otherwise <see langword="false"/>.</returns>
        public bool Contains(Type type)
        {
            var instance = RoleInstancePool.Instance[type];
            if (instance == null)
                return false;
            return roleTypesByAlignment[instance.FullAlignment].Contains(type);
        }

        /// <summary>Adds a role type to this collection, if it does not exist.</summary>
        /// <typeparam name="T">The role type to add.</typeparam>
        /// <returns><see langword="true"/> if the role type was successfully added, otherwise <see langword="false"/>.</returns>
        public bool Add<T>()
            where T : Role, new()
        {
            return AddConditionallyValidated(typeof(T), false);
        }
        /// <summary>Adds a role type to this collection, if it does not exist.</summary>
        /// <param name="role">The role type to add.</param>
        /// <returns><see langword="true"/> if the role type was successfully added, otherwise <see langword="false"/>.</returns>
        public bool Add(Type role)
        {
            return AddConditionallyValidated(role, true);
        }

        /// <summary>Removes a role from this collection, if it exists.</summary>
        /// <typeparam name="T">The role type to remove.</typeparam>
        /// <returns><see langword="true"/> if the role type was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool Remove<T>()
            where T : Role, new()
        {
            return Remove(typeof(T));
        }
        /// <summary>Removes a role from this collection, if it exists.</summary>
        /// <param name="role">The role type to remove.</param>
        /// <returns><see langword="true"/> if the role type was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool Remove(Role role)
        {
            return Remove(role.GetType());
        }
        /// <summary>Removes a role from this collection, if it exists.</summary>
        /// <param name="role">The role type to remove.</param>
        /// <returns><see langword="true"/> if the role type was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool Remove(Type role)
        {
            var instance = RoleInstancePool.Instance[role];
            if (instance == null)
                return false;

            roleTypesByAlignment[instance.FullAlignment].Remove(role);
            roleTypesByFaction[instance.Faction].Remove(role);

            Count--;
            return true;
        }

        /// <summary>Adds a collection of role types to this collection.</summary>
        /// <param name="roles">The role types to add.</param>
        public void AddRange(params Type[] roles)
        {
            AddRange((IEnumerable<Type>)roles);
        }
        /// <summary>Adds a collection of role types to this collection.</summary>
        /// <param name="roles">The role types to add.</param>
        public void AddRange(IEnumerable<Type> roles)
        {
            foreach (var r in roles)
                Add(r);
        }
        /// <summary>Removes a collection of role types from this collection, if they exist.</summary>
        /// <param name="roles">The role types to remove.</param>
        public void RemoveRange(params Type[] roles)
        {
            RemoveRange((IEnumerable<Type>)roles);
        }
        /// <summary>Removes a collection of role types from this collection, if they exist.</summary>
        /// <param name="roles">The role types to remove.</param>
        public void RemoveRange(IEnumerable<Type> roles)
        {
            foreach (var r in roles)
                Remove(r);
        }

        /// <summary>Clears this collection.</summary>
        public void Clear()
        {
            roleTypesByAlignment.Clear();
            roleTypesByFaction.Clear();
            Count = 0;
        }
        /// <summary>Removes all role types that belong to the specified faction from this collection.</summary>
        /// <param name="faction">The faction that the roles to remove belong to.</param>
        public void ClearFaction(Faction faction)
        {
            var factionRoles = roleTypesByFaction[faction];
            var rolesToRemove = factionRoles.ToArray();
            factionRoles.Clear();
            RemoveRange(rolesToRemove);
            Count -= rolesToRemove.Length;
        }
        /// <summary>Removes all role types that belong to the specified alignment from this collection.</summary>
        /// <param name="alignment">The alignment that the roles to remove belong to.</param>
        public void ClearAlignment(RoleAlignment alignment)
        {
            var factionRoles = roleTypesByAlignment[alignment];
            var rolesToRemove = factionRoles.ToArray();
            factionRoles.Clear();
            RemoveRange(rolesToRemove);
            Count -= rolesToRemove.Length;
        }

        private bool AddConditionallyValidated(Type role, bool validate)
        {
            if (validate)
                if (!Role.IsValidRoleType(role))
                    return false;

            var instance = RoleInstancePool.Instance[role];

            roleTypesByAlignment[instance.FullAlignment].Add(role);
            roleTypesByFaction[instance.Faction].Add(role);

            Count++;
            return true;
        }

        #region ICollection Implementation
        public IEnumerator<Type> GetEnumerator() => roleTypesByAlignment.Values.Flatten().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<Type>.Add(Type item) => Add(item);
        void ICollection<Type>.CopyTo(Type[] array, int arrayIndex)
        {
            int current = arrayIndex;
            foreach (var t in this)
            {
                if (current >= array.Length)
                    return;

                array[current] = t;
            }
        }
        #endregion

        /// <summary>Gets all the roles that belong to a faction.</summary>
        /// <param name="faction">The faction.</param>
        /// <returns>A collection of elements that belong to the provided faction.</returns>
        public Type[] this[Faction faction] => roleTypesByFaction[faction].ToArray();
        /// <summary>Gets all the roles that belong to an alignment. </summary>
        /// <param name="alignment">The alignment.</param>
        /// <returns>A collection of elements that belong to the provided alignment. If the alignment is <seealso cref="RoleAlignment.Any"/>, this returns a copy of this collection's contents. If <seealso cref="RoleAlignment.Alignment"/> is <seealso cref="Alignment.Any"/>, the <seealso cref="this[Faction]"/> accessor is called instead.</returns>
        public Type[] this[RoleAlignment alignment]
        {
            get
            {
                if (alignment == RoleAlignment.Any)
                    return this.ToArray();

                if (alignment.Alignment == Alignment.Any)
                    return this[alignment.Faction];
                    
                return roleTypesByAlignment[alignment].ToArray();
            }
        }

        private class FlexibleTypeHashSetDictionary<T> : FlexibleDictionary<T, HashSet<Type>>
        {
            public FlexibleTypeHashSetDictionary()
                : base() { }
            public FlexibleTypeHashSetDictionary(int capacity)
                : base(capacity) { }
            public FlexibleTypeHashSetDictionary(IEnumerable<T> collection)
                : base(collection) { }
            public FlexibleTypeHashSetDictionary(IEnumerable<KeyValuePair<T, HashSet<Type>>> kvps)
                : base(kvps) { }
            public FlexibleTypeHashSetDictionary(FlexibleTypeHashSetDictionary<T> other)
                : base(other) { }
            public FlexibleTypeHashSetDictionary(IEnumerable<T> collection, HashSet<Type> initialValue)
                : base(collection, initialValue) { }

            protected override HashSet<Type> GetNewEntryInitializationValue() => new();
        }
    }
}
