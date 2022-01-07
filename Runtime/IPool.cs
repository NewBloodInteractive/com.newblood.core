namespace NewBlood
{
    /// <summary>Provides a resource pool that enables reusing instances.</summary>
    public interface IPool<T>
        where T : class
    {
        /// <summary>The number of objects that the pool is capable of storing.</summary>
        int Capacity { get; }

        /// <summary>The number of objects currently stored in the pool.</summary>
        int Count { get; }

        /// <summary>Requests an object from the pool.</summary>
        bool TryRent(out T obj);

        /// <summary>Returns an object to the pool.</summary>
        bool TryReturn(T obj);
    }
}
