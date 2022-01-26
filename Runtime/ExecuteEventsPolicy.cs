using UnityEngine;
using UnityEngine.EventSystems;

namespace NewBlood
{
    /// <summary>A pooling policy that dispatches messages using <see cref="ExecuteEvents"/>.</summary>
    public sealed class ExecuteEventsPolicy : IPoolPolicy<GameObject>
    {
        readonly ExecuteEventsMode mode;

        /// <summary>A shared <see cref="ExecuteEventsPolicy"/> instance using <see cref="ExecuteEvents.Execute{T}(GameObject, BaseEventData, ExecuteEvents.EventFunction{T})"/>.</summary>
        public static ExecuteEventsPolicy Execute { get; } = new ExecuteEventsPolicy(ExecuteEventsMode.Execute);

        /// <summary>A shared <see cref="ExecuteEventsPolicy"/> instance using <see cref="ExecuteEvents.ExecuteHierarchy{T}(GameObject, BaseEventData, ExecuteEvents.EventFunction{T})"/>.</summary>
        public static ExecuteEventsPolicy ExecuteHierarchy { get; } = new ExecuteEventsPolicy(ExecuteEventsMode.ExecuteHierarchy);

        /// <summary>Initializes a new <see cref="ExecuteEventsPolicy"/> instance.</summary>
        public ExecuteEventsPolicy(ExecuteEventsMode mode)
        {
            this.mode = mode;
        }

        /// <inheritdoc/>
        public void Rent(GameObject obj, IPool<GameObject> pool)
        {
            if (mode == ExecuteEventsMode.Execute)
                ExecuteEvents.Execute<IPoolRentHandler>(obj, new PoolEventData(EventSystem.current) { Pool = pool }, ExecuteOnRent);
            else if (mode == ExecuteEventsMode.ExecuteHierarchy)
                ExecuteEvents.ExecuteHierarchy<IPoolRentHandler>(obj, new PoolEventData(EventSystem.current) { Pool = pool }, ExecuteOnRent);
        }

        /// <inheritdoc/>
        public void Return(GameObject obj, IPool<GameObject> pool)
        {
            if (mode == ExecuteEventsMode.Execute)
                ExecuteEvents.Execute<IPoolReturnHandler>(obj, new PoolEventData(EventSystem.current) { Pool = pool }, ExecuteOnReturn);
            else if (mode == ExecuteEventsMode.ExecuteHierarchy)
                ExecuteEvents.ExecuteHierarchy<IPoolReturnHandler>(obj, new PoolEventData(EventSystem.current) { Pool = pool }, ExecuteOnReturn);
        }

        static void ExecuteOnRent(IPoolRentHandler handler, BaseEventData data)
        {
            handler.OnRent(((PoolEventData)data).Pool);
        }

        static void ExecuteOnReturn(IPoolReturnHandler handler, BaseEventData data)
        {
            handler.OnReturn(((PoolEventData)data).Pool);
        }

        sealed class PoolEventData : BaseEventData
        {
            public IPool<GameObject> Pool { get; set; }

            public PoolEventData(EventSystem eventSystem)
                : base(eventSystem)
            {
            }
        }
    }
}
