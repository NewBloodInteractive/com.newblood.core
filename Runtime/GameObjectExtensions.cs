using System;
using UnityEngine;

namespace NewBlood
{
    /// <summary>Provides extension methods for <see cref="GameObject"/>.</summary>
    public static class GameObjectExtensions
    {
        /// <summary>Returns the component of type <typeparamref name="T"/> if the game object has one attached, adds one if it doesn't.</summary>
        public static T GetOrAddComponent<T>(this GameObject @this)
            where T : Component
        {
            if (@this.TryGetComponent(out T component))
                return component;

            return @this.AddComponent<T>();
        }

        /// <summary>Returns the component of type <paramref name="type"/> if the game object has one attached, adds one if it doesn't.</summary>
        public static Component GetOrAddComponent(this GameObject @this, Type type)
        {
            if (@this.TryGetComponent(type, out Component component))
                return component;

            return @this.AddComponent(type);
        }
    }
}
