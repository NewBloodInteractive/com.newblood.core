using UnityEngine.EventSystems;

namespace NewBlood
{
    /// <summary>Specifies the mode to use with <see cref="ExecuteEvents"/>.</summary>
    public enum ExecuteEventsMode
    {
        /// <summary><see cref="ExecuteEvents.Execute{T}(UnityEngine.GameObject, BaseEventData, ExecuteEvents.EventFunction{T})"/></summary>
        Execute,

        /// <summary><see cref="ExecuteEvents.ExecuteHierarchy{T}(UnityEngine.GameObject, BaseEventData, ExecuteEvents.EventFunction{T})"/></summary>
        ExecuteHierarchy
    }
}
