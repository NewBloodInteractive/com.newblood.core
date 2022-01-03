using UnityEngine;

namespace NewBlood
{
    /// <summary>A pooling policy that dispatches messages using <see cref="GameObject.BroadcastMessage(string, SendMessageOptions)"/>.</summary>
    public sealed class BroadcastMessagePolicy : IPoolPolicy<GameObject>
    {
        readonly SendMessageOptions options;

        /// <summary>A shared <see cref="BroadcastMessagePolicy"/> instance using <see cref="SendMessageOptions.DontRequireReceiver"/>.</summary>
        public static BroadcastMessagePolicy DontRequireReceiver { get; } = new BroadcastMessagePolicy(SendMessageOptions.DontRequireReceiver);

        /// <summary>A shared <see cref="BroadcastMessagePolicy"/> instance using <see cref="SendMessageOptions.RequireReceiver"/>.</summary>
        public static BroadcastMessagePolicy RequireReceiver { get; } = new BroadcastMessagePolicy(SendMessageOptions.RequireReceiver);

        /// <summary>The name of the message dispatched for rentals.</summary>
        public const string OnRentMessage = "OnRent";

        /// <summary>The name of the message dispatched for returns.</summary>
        public const string OnReturnMessage = "OnReturn";

        /// <inheritdoc/>
        public void Rent(GameObject obj)
        {
            obj.BroadcastMessage(OnRentMessage, options);
        }

        /// <inheritdoc/>
        public void Return(GameObject obj)
        {
            obj.BroadcastMessage(OnReturnMessage, options);
        }

        /// <summary>Initializes a new <see cref="BroadcastMessagePolicy"/> instance.</summary>
        public BroadcastMessagePolicy(SendMessageOptions options)
        {
            this.options = options;
        }
    }
}
