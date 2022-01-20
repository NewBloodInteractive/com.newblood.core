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

        /// <summary>Dispatches a synchronous message to the main thread.</summary>
        public static void Send(SendOrPostCallback callback, object state)
        {
            SynchronizationContext.Send(callback, state);
        }

        /// <summary>Dispatches an asynchronous message to the main thread.</summary>
        public static void Post(SendOrPostCallback callback, object state)
        {
            SynchronizationContext.Post(callback, state);
        }

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
