using UnityEngine;

namespace NewBlood
{
    /// <summary>Base type for the <see cref="MonoSingleton{TSelf}"/> class.</summary>
    [DisallowMultipleComponent]
    public abstract class MonoSingleton : MonoBehaviour, IPersistentObject
    {
        // This class provides a method to initialize singletons in a non-generic fashion,
        // so the only class that should derive from it is the MonoSingleton<TSelf> class.
        private protected MonoSingleton()
        {
        }

        /// <summary>Makes this component the global instance.</summary>
        public abstract void MakeCurrent();

        /// <inheritdoc/>
        void IPersistentObject.Initialize()
        {
            MakeCurrent();
        }
    }

    /// <summary>Base type for singleton <see cref="MonoBehaviour"/> classes.</summary>
    public abstract class MonoSingleton<TSelf> : MonoSingleton
        where TSelf : MonoSingleton<TSelf>
    {
        /// <summary>Gets the current singleton instance.</summary>
        public static TSelf Instance { get; private set; }

        /// <inheritdoc/>
        public sealed override void MakeCurrent()
        {
            Instance = (TSelf)this;
        }
    }
}
