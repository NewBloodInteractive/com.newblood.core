using UnityEngine;

namespace NewBlood
{
    /// <summary>A pooling policy that dispatches messages using <see cref="GameObject.SendMessage(string, SendMessageOptions)"/>.</summary>
    public sealed class SendMessagePolicy : IPoolPolicy<GameObject>
    {
        readonly SendMessageOptions options;

        /// <summary>A shared <see cref="SendMessagePolicy"/> instance using <see cref="SendMessageOptions.DontRequireReceiver"/>.</summary>
        public static SendMessagePolicy DontRequireReceiver { get; } = new SendMessagePolicy(SendMessageOptions.DontRequireReceiver);

        /// <summary>A shared <see cref="SendMessagePolicy"/> instance using <see cref="SendMessageOptions.RequireReceiver"/>.</summary>
        public static SendMessagePolicy RequireReceiver { get; } = new SendMessagePolicy(SendMessageOptions.RequireReceiver);

        /// <summary>The name of the message dispatched for rentals.</summary>
        public const string OnRentMessage = "OnRent";

        /// <summary>The name of the message dispatched for returns.</summary>
        public const string OnReturnMessage = "OnReturn";

        /// <inheritdoc/>
        public void Rent(GameObject obj)
        {
            obj.SendMessage(OnRentMessage, options);
        }

        /// <inheritdoc/>
        public void Return(GameObject obj)
        {
            obj.SendMessage(OnReturnMessage, options);
        }

        /// <summary>Initializes a new <see cref="SendMessagePolicy"/> instance.</summary>
        public SendMessagePolicy(SendMessageOptions options)
        {
            this.options = options;
        }
    }
}
