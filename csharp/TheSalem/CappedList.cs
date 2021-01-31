using Garyon.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TheSalem
{
    public class CappedList<T> : IList<T>
    {
        private List<T> list;

        public int Count => list.Count;
        public int Capacity { get; private init; }

        bool ICollection<T>.IsReadOnly => false;

        public CappedList(int capacity)
        {
            list = new(Capacity = capacity);
        }
        public CappedList(int capacity, T[] array)
        {
            if (array.Length > capacity)
                ThrowHelper.Throw<ArgumentException>("The provided array's length may not exceed the given capacity.");

            list = new(Capacity = capacity);
            list.AddRange(array);
        }
        public CappedList(int capacity, ICollection<T> collection)
        {
            if (collection.Count > capacity)
                ThrowHelper.Throw<ArgumentException>("The provided collection's count may not exceed the given capacity.");

            list = new(Capacity = capacity);
            list.AddRange(collection);
        }
        public CappedList(CappedList<T> other)
        {
            list = new(other);
            Capacity = other.Capacity;
        }

        public void Add(T item)
        {
            if (Count >= Capacity)
                return;

            list.Add(item);
        }
        public void Clear() => list.Clear();
        public bool Contains(T item) => list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public int IndexOf(T item) => list.IndexOf(item);
        public void Insert(int index, T item)
        {
            if (Count >= Capacity)
                return;

            list.Insert(index, item);
        }
        public bool Remove(T item) => list.Remove(item);
        public void RemoveAt(int index) => list.RemoveAt(index);

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
    }
}
