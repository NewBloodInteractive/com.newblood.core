using UnityEngine;
using Object = UnityEngine.Object;

namespace NewBlood
{
    /// <summary>Handles the initialization of persistent game objects.</summary>
    public static class PersistentObjects
    {
        /// <summary>Initializes persistent objects contained in the given prefab.</summary>
        public static void Initialize(GameObject prefab)
        {
            var root = new GameObject();

            // The root GameObject is disabled so that callbacks won't
            // execute on child objects until they have been detached.
            root.SetActive(false);

            // Instantiating the prefab as a child of the disabled root object ensures
            // that it won't be considered an active game object until we want it to be.
            var instance = Object.Instantiate(prefab, root.transform);

            // Initialize any MonoSingleton components that exist in the hierarchy.
            foreach (var singleton in instance.GetComponentsInChildren<MonoSingleton>(includeInactive: true))
            {
                singleton.MakeCurrent();
            }

            // DontDestroyOnLoad will move the root object to a special scene for persistent objects.
            Object.DontDestroyOnLoad(root);

            // Calling DetachChildren will unpack the hierarchy into the special DontDestroyOnLoad scene.
            // As a result, all the objects will be persistent with no need to call DontDestroyOnLoad again.
            instance.transform.DetachChildren();

            // After unpacking, we no longer need the root object.
            Object.Destroy(root);
        }
    }
}
