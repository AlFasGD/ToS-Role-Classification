using System;

namespace TheSalem
{
    /// <summary>Represents a game pack type, i.e. Classic or Coven.</summary>
    [Flags]
    public enum GamePackTypes
    {
        /// <summary>Represents the Classic game pack.</summary>
        Classic = 1,
        /// <summary>Represents the Coven game pack.</summary>
        Coven = 1 << 1,

        /// <summary>Represents a combination of all the available game packs.</summary>
        All = Classic | Coven,
    }
}
