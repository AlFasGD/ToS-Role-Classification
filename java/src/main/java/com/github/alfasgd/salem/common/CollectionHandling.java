package com.github.alfasgd.salem.common;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;

/**
 * Contains functions useful for handling collections.
 */
public final class CollectionHandling
{
    /**
     * Collects the objects that are either the exact or a subclass of the provided subclass type.
     * @param baseCollection The base collection whose elements to filter.
     * @param subclass The type of the elements that the resulting collection will contain.
     * @param <TBase> The base type of the elements in the initial collection.
     * @param <TSub> The type of the elements that the resulting collection will contain.
     * @return A collection of the filtered elements.
     */
    public static <TBase, TSub extends TBase> Collection<TSub> collectOfType(Collection<TBase> baseCollection, Class<TSub> subclass)
    {
        var list = new ArrayList<TSub>();
        for (var e : baseCollection)
            if (subclass.isInstance(e))
                list.add((TSub)e);
        return list;
    }

    /**
     * Converts the provided array into a {@link Collection<T>}. Any modifications to either the array or the collection
     * will be reflected, as this method is backed up by {@link Arrays#asList(T[])}.
     * @param array The array to convert into a {@link Collection<T>}.
     * @param <T> The type of the contained elements.
     * @return A collection of the elements that are contained in the array.
     */
    public static <T> Collection<T> asCollection(T[] array)
    {
        return Arrays.asList(array);
    }
}
