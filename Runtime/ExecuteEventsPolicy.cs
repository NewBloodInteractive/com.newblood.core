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
        public void Rent(GameObject obj)
        {
            if (mode == ExecuteEventsMode.Execute)
                ExecuteEvents.Execute<IPoolRentHandler>(obj, null, ExecuteOnRent);
            else if (mode == ExecuteEventsMode.ExecuteHierarchy)
                ExecuteEvents.ExecuteHierarchy<IPoolRentHandler>(obj, null, ExecuteOnRent);
        }

        /// <inheritdoc/>
        public void Return(GameObject obj)
        {
            if (mode == ExecuteEventsMode.Execute)
                ExecuteEvents.Execute<IPoolReturnHandler>(obj, null, ExecuteOnReturn);
            else if (mode == ExecuteEventsMode.ExecuteHierarchy)
                ExecuteEvents.ExecuteHierarchy<IPoolReturnHandler>(obj, null, ExecuteOnReturn);
        }

        static void ExecuteOnRent(IPoolRentHandler handler, BaseEventData data)
        {
            handler.OnRent();
        }

        static void ExecuteOnReturn(IPoolReturnHandler handler, BaseEventData data)
        {
            handler.OnReturn();
        }
    }
}
