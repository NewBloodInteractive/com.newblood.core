using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NewBlood
{
    /// <summary>Registers a component with the assembly to be initialized as a singleton.</summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RegisterSingletonAttribute : Attribute
    {
        /// <summary>The component type.</summary>
        public Type ComponentType { get; }

        /// <summary>Initializes a new <see cref="RegisterSingletonAttribute"/> instance.</summary>
        public RegisterSingletonAttribute(Type componentType)
        {
            ComponentType = componentType;
        }

        /// <summary>Initializes all singleton components registered to the given assembly.</summary>
        public static void Initialize(Assembly assembly)
        {
            var root = new GameObject();

            // The root GameObject is disabled so that callbacks won't
            // execute on child objects until they have been detached.
            root.SetActive(false);

            foreach (var attribute in assembly.GetCustomAttributes<RegisterSingletonAttribute>())
            {
                var gameObject = new GameObject(attribute.ComponentType.Name);

                // Making the singleton object a child of the disabled root object ensures
                // that it won't be considered an active game object until we want it to be.
                gameObject.transform.SetParent(root.transform);

                // If the component implements IPersistentObject, we should call Initialize.
                if (gameObject.AddComponent(attribute.ComponentType) is IPersistentObject persistent)
                {
                    persistent.Initialize();
                }
            }

            // DontDestroyOnLoad will move the root object to a special scene for persistent objects.
            Object.DontDestroyOnLoad(root);

            // Calling DetachChildren will unpack the hierarchy into the special DontDestroyOnLoad scene.
            // As a result, all the objects will be persistent with no need to call DontDestroyOnLoad again.
            root.transform.DetachChildren();

            // After unpacking, we no longer need the root object.
            Object.Destroy(root);
        }
    }
}
