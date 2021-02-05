package org.salem.common;

import java.util.Arrays;

/**
 * Provides array utility functions.
 */
public final class ArrayUtils
{
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy. 
     * @param <T> The type of the contained elements.
     * @return The copied array.
     */
    public static <T> T[] copyOf(T[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static byte[] copyOf(byte[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static short[] copyOf(short[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static int[] copyOf(int[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static long[] copyOf(long[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static float[] copyOf(float[] original) { return Arrays.copyOf(original, original.length); }
    /**
     * Creates a new array containing all the elements of the provided array.
     * @param original The original array to copy.
     * @return The copied array.
     */
    public static double[] copyOf(double[] original) { return Arrays.copyOf(original, original.length); }
}
