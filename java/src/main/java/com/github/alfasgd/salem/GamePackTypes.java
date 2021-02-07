package com.github.alfasgd.salem;

import java.util.*;

/**
 * Represents a game pack type, i.e. Classic or Coven.
 */
public enum GamePackTypes
{
    /**
     * Represents the Classic game pack.
     */
    Classic(1),
    /**
     * Represents the Coven game pack.
     */
    Coven(1 << 1),

    /**
     * Represents a combination of all the available game packs.
     */
    All(Classic.code | Coven.code);

    private static final HashMap<Integer, GamePackTypes> typesByCode;

    static
    {
        typesByCode = new HashMap<>();
        for (var type : values())
            typesByCode.put(type.code, type);
    }

    /**
     * The code of the game pack type.
     */
    public final int code;

    GamePackTypes(int code)
    {
        this.code = code;
    }

    /**
     * Gets the game pack type, given the requested code.
     * @param code The code of the game pack type to get.
     * @return The requested game pack type.
     */
    public static GamePackTypes getTypeFromCode(int code)
    {
        return typesByCode.get(code);
    }
}
