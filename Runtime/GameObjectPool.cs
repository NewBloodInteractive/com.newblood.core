using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NewBlood
{
    /// <summary>Provides a pooling mechanism for <see cref="GameObject"/> instances.</summary>
    public sealed class GameObjectPool : IPool<GameObject>, IDisposable
    {
        bool disposed;

        readonly Transform container;

        readonly GameObject prefab;

        /// <summary>Gets the number of objects currently available for rental.</summary>
        public int Count => container.childCount;

        /// <summary>Initializes a new <see cref="GameObjectPool"/> instance.</summary>
        public GameObjectPool()
        {
            container = ObjectPool.CreateContainer(nameof(GameObjectPool));
        }

        /// <summary>Initializes a new <see cref="GameObjectPool"/> instance with the given prefab.</summary>
        public GameObjectPool(GameObject prefab)
            : this()
        {
            this.prefab = prefab;
        }

        /// <summary>Prepares the pool to rent a specified number of instances.</summary>
        public void Prepare(int count)
        {
            ThrowIfDisposed();

            for (int i = 0; i < count; i++)
            {
                ObjectPool.CreateGameObject(prefab, container);
            }
        }

        /// <summary>Rents a <see cref="GameObject"/> from the pool.</summary>
        public GameObject Rent()
        {
            ThrowIfDisposed();
            return ObjectPool.RentOrCreateGameObject(prefab, container);
        }

        /// <summary>Returns a <see cref="GameObject"/> to the pool.</summary>
        public void Return(GameObject rental)
        {
            ThrowIfDisposed();
            rental.transform.SetParent(container);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (disposed)
                return;

            Object.Destroy(container.gameObject);
            disposed = true;
        }

        void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(GameObjectPool));
            }
        }
    }
}
