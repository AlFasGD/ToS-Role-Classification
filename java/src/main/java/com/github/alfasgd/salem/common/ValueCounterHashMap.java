package com.github.alfasgd.salem.common;

import java.util.HashMap;

/**
 * Represents a hash map of keys mapped to integers, that act as counters.
 * @param <TKey> The type of the keys this hash map contains.
 */
public class ValueCounterHashMap<TKey>
    extends HashMap<TKey, Integer>
{
    /**
     * Adds a value to the counter for the specified key.
     * @param key The key whose counter to increase by the requested value.
     * @param value The value to add.
     */
    public void add(TKey key, int value) { put(key, get(key) + value); }
    /**
     * Adds 1 to the counter for the specified key.
     * @param key The key whose counter to increase by 1.
     */
    public void add(TKey key) { add(key, 1); }

    /**
     * Subtracts a value from the counter for the specified key.
     * @param key The key whose counter to decrease by the requested value.
     * @param value The value to add.
     */
    public void subtract(TKey key, int value) { put(key, get(key) - value); }
    /**
     * Subtracts 1 from the counter for the specified key.
     * @param key The key whose counter to decrease by 1.
     */
    public void subtract(TKey key) { subtract(key, 1); }
}
