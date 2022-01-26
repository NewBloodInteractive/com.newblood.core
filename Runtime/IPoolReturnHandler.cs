using UnityEngine;
using UnityEngine.EventSystems;

namespace NewBlood
{
    /// <summary>Interface to implement if you wish to receive OnReturn callbacks.</summary>
    public interface IPoolReturnHandler : IEventSystemHandler
    {
        /// <summary>Callback for when the object is returned.</summary>
        void OnReturn(IPool<GameObject> pool);
    }
}
