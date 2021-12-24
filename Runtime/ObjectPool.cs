using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewBlood
{
    static class ObjectPool
    {
        public static Transform CreateContainer(string name)
        {
            var gameObject = new GameObject(name)
            {
                // The pool container is hidden from the hierarchy,
                // as its existence is only an implementation detail.
                hideFlags = HideFlags.HideAndDontSave
            };

            Object.DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
            return gameObject.transform;
        }

        public static GameObject RentOrCreateGameObject(GameObject prefab, Transform container)
        {
            if (container.childCount == 0)
                return CreateGameObject(prefab, null);

            return RentGameObject(container);
        }

        public static T RentOrCreateGameObject<T>(T prefab, Transform container)
            where T : Component
        {
            if (container.childCount == 0)
                return CreateGameObject(prefab, null);

            return RentGameObject(container).GetComponent<T>();
        }

        public static GameObject CreateGameObject(GameObject prefab, Transform container)
        {
            if (prefab == null)
                return CreateGameObject(container);

            var instance  = Object.Instantiate(prefab, container);
            instance.name = prefab.name;
            return instance;
        }

        public static T CreateGameObject<T>(T prefab, Transform container)
            where T : Component
        {
            if (prefab == null)
                return CreateGameObject(container).AddComponent<T>();

            var instance  = Object.Instantiate(prefab, container);
            instance.name = prefab.name;
            return instance;
        }

        static GameObject RentGameObject(Transform container)
        {
            // Retrieve the first available child of the root container.
            var transform  = container.GetChild(0);
            var gameObject = transform.gameObject;

            // We don't want the game object to remain persistent once it's been rented,
            // so detach the game object from the root and move it to the active scene.
            transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            return gameObject;
        }

        static GameObject CreateGameObject(Transform container)
        {
            var gameObject = new GameObject();
            gameObject.transform.SetParent(container);
            return gameObject;
        }
    }
}
