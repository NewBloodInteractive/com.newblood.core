namespace NewBlood
{
    /// <summary>Provides a callback for initializing components with <see cref="PersistentObjects"/>.</summary>
    public interface IPersistentObject
    {
        /// <summary>Performs component-specific initialization.</summary>
        void Initialize();
    }
}
