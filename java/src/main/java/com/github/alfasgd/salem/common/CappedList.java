package com.github.alfasgd.salem.common;

import java.util.*;

public class CappedList<T>
    implements List<T>
{
    private List<T> list;
    private final int capacity;

    public CappedList(int capacity)
    {
        this.capacity = capacity;
        list = new ArrayList<>(capacity);
    }
    public CappedList(int capacity, T[] array)
    {
        if (array.length > capacity)
            throw new IllegalArgumentException("The provided array's length may not exceed the given capacity.");

        this.capacity = capacity;
        list = new ArrayList<>(capacity);
        list.addAll(CollectionHandling.asCollection(array));
    }
    public CappedList(int capacity, Collection<T> collection)
    {
        if (collection.size() > capacity)
            throw new IllegalArgumentException("The provided collection's count may not exceed the given capacity.");

        this.capacity = capacity;
        list = new ArrayList<>();
        list.addAll(collection);
    }
    public CappedList(CappedList<T> other)
    {
        list = new ArrayList<>(other);
        capacity = other.capacity;
    }

    public int getCapacity() { return capacity; }

    @Override
    public int size()
    {
        return list.size();
    }

    @Override
    public boolean isEmpty() { return list.isEmpty(); }
    @Override
    public boolean contains(Object o) { return list.contains(o); }
    @Override
    public Iterator<T> iterator() { return list.iterator(); }
    @Override
    public Object[] toArray() { return list.toArray(); }
    @Override
    public <U> U[] toArray(U[] array) { return list.toArray(array); }
    @Override
    public boolean containsAll(Collection<?> collection) { return list.containsAll(collection); }
    @Override
    public boolean addAll(Collection<? extends T> collection) { return list.addAll(collection); }
    @Override
    public boolean addAll(int i, Collection<? extends T> collection) { return list.addAll(i, collection); }

    @Override
    public boolean removeAll(Collection<?> collection) { return list.removeAll(collection); }
    @Override
    public boolean retainAll(Collection<?> collection) { return list.retainAll(collection); }

    @Override
    public boolean add(T item)
    {
        if (size() >= capacity)
            return false;

        return list.add(item);
    }

    @Override
    public boolean remove(Object o) { return list.remove(o); }

    @Override
    public void clear() { list.clear(); }

    @Override
    public int indexOf(Object item) { return list.indexOf(item); }
    @Override
    public void add(int index, T item)
    {
        if (list.size() >= capacity)
            return;

        list.add(index, item);
    }

    @Override
    public T get(int index) { return list.get(index); }
    @Override
    public T set(int index, T value)
    {
        list.set(index, value);
        return value;
    }

    @Override
    public T remove(int i) { return list.remove(i); }
    @Override
    public int lastIndexOf(Object o) { return list.lastIndexOf(o); }
    @Override
    public ListIterator<T> listIterator() { return list.listIterator(); }
    @Override
    public ListIterator<T> listIterator(int i) { return list.listIterator(i); }
    @Override
    public List<T> subList(int a, int b) { return list.subList(a, b); }
}
