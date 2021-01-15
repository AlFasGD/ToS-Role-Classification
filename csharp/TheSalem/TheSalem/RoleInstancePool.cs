using Garyon.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheSalem
{
    /// <summary>Contains instances of the available roles.</summary>
    public class RoleInstancePool
    {
        /// <summary>The singleton instance of the <seealso cref="RoleInstancePool"/>.</summary>
        public static RoleInstancePool Instance { get; }

        static RoleInstancePool()
        {
            Instance = new(Assembly.GetExecutingAssembly());
        }

        private readonly FlexibleDictionary<Type, Role> roleTypeInstances = new();

        /// <summary>Gets all the available role types that have an instance registered. This creates a copy of the instance pool, meaning it should be called the least possible.</summary>
        public IReadOnlyDictionary<Type, Role> AllRoleTypes => new Dictionary<Type, Role>(roleTypeInstances);
        /// <summary>Gets the count of available role types that have an instance registered.</summary>
        public int RoleTypeCount => roleTypeInstances.Count;

        private RoleInstancePool(Assembly assembly)
        {
            var roleTypes = assembly.GetTypes().Where(Role.IsValidRoleType);
            foreach (var t in roleTypes)
                RegisterConditionallyChecked(t, false);
        }

        /// <summary>Registers a role type to the pool.</summary>
        /// <param name="roleType">The type of the role to register.</param>
        /// <returns><see langword="true"/> if the role was successfully added to the pool, otherwise <see langword="false"/>, if it was already contained, or if the type is not a valid <seealso cref="Role"/> type.</returns>
        public bool Register(Type roleType)
        {
            return RegisterConditionallyChecked(roleType, true);
        }
        private bool RegisterConditionallyChecked(Type roleType, bool check)
        {
            if (check)
                if (!Role.IsValidRoleType(roleType))
                    return false;

            if (roleTypeInstances[roleType] != null)
                return false;

            var instance = roleType.GetConstructor(Type.EmptyTypes).Invoke(null) as Role;

            roleTypeInstances[roleType] = instance;
            return true;
        }

        /// <summary>Gets the instance of the specified <seealso cref="Role"/> type.</summary>
        /// <typeparam name="T">The type of the <seealso cref="Role"/> whose instance to get.</typeparam>
        /// <returns>The instance of the specified <seealso cref="Role"/> type.</returns>
        public Role GetInstance<T>()
            where T : Role
        {
            return this[typeof(T)];
        }

        /// <summary>Gets the instance of the specified <seealso cref="Role"/> type.</summary>
        /// <param name="roleType">The type of the <seealso cref="Role"/> whose instance to get.</param>
        /// <returns>The instance of the specified <seealso cref="Role"/> type.</returns>
        public Role this[Type roleType] => roleTypeInstances[roleType];
    }
}
