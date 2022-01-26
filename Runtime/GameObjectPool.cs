using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace NewBlood
{
    /// <summary>Provides a pooling mechanism for <see cref="GameObject"/> instances.</summary>
    public sealed class GameObjectPool : IPool<GameObject>, IDisposable
    {
        bool disposed;

        // The hierarchy size of a new object.
        readonly int objectHierarchyCount;

        // An optional prefab to use for constructing new objects.
        readonly GameObject prefab;

        // An optional policy for defining rent and return behaviour.
        readonly IPoolPolicy<GameObject> policy;

        // A persistent, hidden game object acting as a container for pooled objects.
        // It will remain in the DontDestroyOnLoad scene until the pool is disposed of.
        //
        // This object is always disabled, ensuring that its child objects are also
        // disabled without requiring manual SetActive calls each time they are rented
        // and returned to the pool.
        //
        readonly Transform container;

        /// <summary>Initializes a new <see cref="GameObjectPool"/> instance.</summary>
        public GameObjectPool()
            : this(null, null)
        {
        }

        /// <summary>Initializes a new <see cref="GameObjectPool"/> instance.</summary>
        public GameObjectPool(GameObject prefab)
            : this(prefab, null)
        {
        }

        /// <summary>Initializes a new <see cref="GameObjectPool"/> instance.</summary>
        public GameObjectPool(GameObject prefab, IPoolPolicy<GameObject> policy)
        {
            this.prefab = prefab;
            this.policy = policy;
            container   = CreateContainer();

            if (prefab == null)
                objectHierarchyCount = 1;
            else
                objectHierarchyCount = prefab.transform.hierarchyCount;
        }

        /// <inheritdoc/>
        public int Capacity
        {
            get
            {
                ThrowIfDisposed();
                return GetCapacity();
            }

            set
            {
                ThrowIfDisposed();

                if (value < 0 || value < container.childCount)
                    throw new ArgumentOutOfRangeException();

                SetCapacity(value);
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                ThrowIfDisposed();
                return container.childCount;
            }

            set
            {
                ThrowIfDisposed();

                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                SetCount(value);
            }
        }

        /// <summary>Requests a new object from the pool.</summary>
        public GameObject Rent()
        {
            ThrowIfDisposed();
            var gameObject = GetNextRental();
            var transform  = gameObject.transform;

            // Detach the object from the container and move it to the active scene.
            // This will activate the object, causing the OnEnable event to be raised.
            transform.SetParent(null, worldPositionStays: true);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

            // If we have a policy defined, allow it to process the rental.
            policy?.Rent(gameObject, this);
            return gameObject;
        }

        /// <summary>Requests a new object from the pool.</summary>
        public GameObject Rent<TState>(TState state, Action<GameObject, TState> callback)
        {
            ThrowIfDisposed();
            var gameObject = GetNextRental();
            var transform  = gameObject.transform;
            callback(gameObject, state);
            transform.SetParent(null, worldPositionStays: true);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            policy?.Rent(gameObject, this);
            return gameObject;
        }

        /// <summary>Requests a new object from the pool.</summary>
        public GameObject Rent(Vector3 position, Quaternion rotation, Transform parent)
        {
            ThrowIfDisposed();
            var gameObject = GetNextRental();
            var transform  = gameObject.transform;
            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent, worldPositionStays: true);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            policy?.Rent(gameObject, this);
            return gameObject;
        }

        /// <summary>Requests a new object from the pool.</summary>
        public GameObject Rent<TState>(Vector3 position, Quaternion rotation, Transform parent, TState state, Action<GameObject, TState> callback)
        {
            ThrowIfDisposed();
            var gameObject = GetNextRental();
            var transform = gameObject.transform;
            transform.SetPositionAndRotation(position, rotation);
            callback(gameObject, state);
            transform.SetParent(parent, worldPositionStays: true);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            policy?.Rent(gameObject, this);
            return gameObject;
        }

        /// <summary>Returns an object to the pool.</summary>
        public void Return(GameObject obj)
        {
            ThrowIfDisposed();

            // If we have a policy defined, allow it to process the return.
            policy?.Return(obj, this);

            // Attach the object to the container. This will deactivate the object.
            obj.transform.SetParent(container, worldPositionStays: true);
        }

        GameObject GetNextRental()
        {
            // The container stores all of our pooled objects, so first check that.
            int count = container.childCount;

            if (count == 0)
                return Create();

            // It is less expensive to detach the last child in the hierarchy.
            // Children further up require reordering the internal structures.
            return container.GetChild(count - 1).gameObject;
        }

        /// <inheritdoc/>
        bool IPool<GameObject>.TryRent(out GameObject obj)
        {
            obj = Rent();
            return true;
        }

        /// <inheritdoc/>
        bool IPool<GameObject>.TryReturn(GameObject obj)
        {
            Return(obj);
            return true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (disposed)
                return;

            Object.Destroy(container.gameObject);
            disposed = true;
        }

        /// <summary>Creates a new pooled object.</summary>
        GameObject Create()
        {
            GameObject gameObject;

            if (prefab != null)
            {
                gameObject      = Object.Instantiate(prefab, container);
                gameObject.name = prefab.name;
            }
            else
            {
                gameObject = new GameObject();
                gameObject.transform.SetParent(container);
            }

            return gameObject;
        }

        /// <summary>Gets the amount of objects the pool is capable of storing.</summary>
        int GetCapacity()
        {
            var remaining = container.hierarchyCapacity - container.hierarchyCount;
            return container.childCount + (remaining / objectHierarchyCount);
        }

        /// <summary>Expands or shrinks the pool to contain the requested number of objects.</summary>
        void SetCount(int count)
        {
            int difference = count - container.childCount;

            if (difference == 0)
                return;

            // Positive difference, we need to create more objects.
            if (difference > 0)
            {
                if (GetCapacity() < count)
                    SetCapacity(count);

                for (int i = 0; i < difference; i++)
                    Create();
            }

            // Negative difference, we need to delete some objects.
            if (difference < 0)
            {
                for (int i = 0; i > difference; i--)
                {
                    var index = container.childCount - 1 + i;
                    var child = container.GetChild(index);
                    Object.Destroy(child.gameObject);
                }
            }
        }

        /// <summary>Modifies the hierarchy capacity of the container object to fit the requested number of objects.</summary>
        void SetCapacity(int capacity)
        {
            int difference = capacity - GetCapacity();

            if (difference == 0)
                return;

            // If there is some extra hierarchy space remaining, use that up.
            int currentCapacity   = container.hierarchyCapacity;
            int remainingCapacity = currentCapacity - container.hierarchyCount;

            // Increase the hierarchy capacity to fit the requested number of objects.
            container.hierarchyCapacity = currentCapacity + (difference * objectHierarchyCount) - remainingCapacity;
        }

        /// <summary>Creates a new container object for pooled objects.</summary>
        static Transform CreateContainer()
        {
            var gameObject = new GameObject(nameof(GameObjectPool))
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            Object.DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
            return gameObject.transform;
        }

        /// <summary>Throws <see cref="ObjectDisposedException"/> if the pool has been disposed.</summary>
        void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(GameObjectPool));
            }
        }
    }
}
