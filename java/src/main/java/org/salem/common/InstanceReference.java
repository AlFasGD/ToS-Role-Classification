package org.salem.common;

/**
 * Represents a reference to an object instance.
 * @param <T> The type of the instance that is referred to.
 */
public class InstanceReference<T>
{
    private T instance;

    /**
     * Initializes a new instance of the {@link InstanceReference} class with a reference to {@code null}.
     */
    public InstanceReference() { }
    /**
     * Initializes a new instance of the {@link InstanceReference} class with a reference to the provided object.
     * @param instance The instance to initialize this instance from.
     */
    public InstanceReference(T instance)
    {
        set(instance);
    }
    /**
     * Initializes a new instance of the {@link InstanceReference} class with a reference to another
     * {@link InstanceReference} object's reference.
     * @param other The {@link InstanceReference} whose reference to initialize this instance from.
     */
    public InstanceReference(InstanceReference<T> other)
    {
        instance = other.instance;
    }

    /**
     * Gets the currently referred instance.
     * @return The currently referred instance from this object.
     */
    public T get() { return instance; }

    /**
     * Sets the currently referred instance to the provided instance.
     * @param newInstance The new instance to set this reference's instance to.
     */
    public void set(T newInstance) { instance = newInstance; }
}
