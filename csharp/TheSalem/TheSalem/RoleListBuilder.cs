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
    }
}
