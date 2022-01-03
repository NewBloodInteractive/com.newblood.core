using System.Threading;
using UnityEngine;

namespace NewBlood
{
    /// <summary>Provides methods for interacting with the main thread.</summary>
    public static class MainThread
    {
        /// <summary>Gets the main thread object.</summary>
        public static Thread Instance { get; private set; }

        /// <summary>Gets the unique identifier for the main thread.</summary>
        public static int ManagedThreadId { get; private set; }

        /// <summary>Gets the synchronization context for the main thread.</summary>
        public static SynchronizationContext SynchronizationContext { get; private set; }

        /// <summary>Initializes the <see cref="MainThread"/> class.</summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void OnSubsystemRegistration()
        {
            Instance               = Thread.CurrentThread;
            ManagedThreadId        = Instance.ManagedThreadId;
            SynchronizationContext = SynchronizationContext.Current;
        }
    }
}
