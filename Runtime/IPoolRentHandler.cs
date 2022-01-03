using UnityEngine.EventSystems;

namespace NewBlood
{
    /// <summary>Interface to implement if you wish to receive OnRent callbacks.</summary>
    public interface IPoolRentHandler : IEventSystemHandler
    {
        /// <summary>Callback for when the object is rented.</summary>
        void OnRent();
    }
}
