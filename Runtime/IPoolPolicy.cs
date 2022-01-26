namespace NewBlood
{
    /// <summary>Implements a policy for pooling of objects.</summary>
    public interface IPoolPolicy<T>
        where T : class
    {
        /// <summary>Processes a rented object before it is returned to a consumer.</summary>
        void Rent(T obj, IPool<T> pool);

        /// <summary>Processes a rented object after it is returned to the pool.</summary>
        void Return(T obj, IPool<T> pool);
    }
}
