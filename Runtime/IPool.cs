namespace NewBlood
{
    /// <summary>Provides a resource pool that enables reusing instances.</summary>
    public interface IPool<T>
    {
        /// <summary>Gets the number of instances currently available for rental.</summary>
        int Count { get; }

        /// <summary>Rents a value from the pool.</summary>
        T Rent();

        /// <summary>Returns a value to the pool.</summary>
        void Return(T value);

        /// <summary>Prepares the pool to rent a specified number of instances.</summary>
        void Prepare(int count);
    }
}
