namespace TheSalem
{
    /// <summary>Represents a <seealso cref="RoleList"/> builder.</summary>
    public class RoleListBuilder
    {
        /// <summary>The role list slots. Use this property to construct the role list.</summary>
        public CappedList<IRoleSlot> RoleSlots { get; set; }

        public RoleListBuilder()
        {
            RoleSlots = new(15);
        }

        /// <summary>Creates a new <seealso cref="RoleList"/> out of the current role list.</summary>
        /// <returns></returns>
        public RoleList ToRoleList() => new(RoleSlots);

        /// <summary>Adds a role of the provided type to the list, if it is not full, otherwise nothing happens.</summary>
        /// <typeparam name="T">The type of the role to add.</typeparam>
        public void Add<T>()
            where T : Role, new()
        {
            RoleSlots.Add(RoleInstancePool.Instance[typeof(T)]);
        }
        /// <summary>Adds a role alignment to the list, if it is not full, otherwise nothing happens.</summary>
        /// <param name="alignment">The role alignment to add to the role list.</param>
        public void Add(RoleAlignment alignment)
        {
            RoleSlots.Add(alignment);
        }
        /// <summary>Adds a role of the provided type to the list a number of times, until the list is full.</summary>
        /// <typeparam name="T">The type of the role to add.</typeparam>
        /// <param name="count">The number of times to add the role to the list.</param>
        public void Add<T>(int count)
            where T : Role, new()
        {
            for (int i = 0; i < count; i++)
                Add<T>();
        }
        /// <summary>Adds a role slot to the list a number of times, until the list is full.</summary>
        /// <param name="count">The number of times to add the role to the list.</param>
        /// <param name="slot">The slot to add to the list.</param>
        public void Add(int count, IRoleSlot slot)
        {
            for (int i = 0; i < count; i++)
                RoleSlots.Add(slot);
        }
    }
}
