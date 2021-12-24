using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NewBlood
{
    /// <summary>Provides a pooling mechanism for <typeparamref name="T"/> instances.</summary>
    public sealed class ComponentPool<T> : IPool<T>, IDisposable
        where T : Component
    {
        bool disposed;

        readonly Transform container;

        readonly T prefab;

        /// <summary>Gets the number of objects currently available for rental.</summary>
        public int Count => container.childCount;

        /// <summary>Initializes a new <see cref="ComponentPool{T}"/> instance.</summary>
        public ComponentPool()
        {
            container = ObjectPool.CreateContainer(nameof(ComponentPool<T>));
        }

        /// <summary>Initializes a new <see cref="ComponentPool{T}"/> instance with the given prefab.</summary>
        public ComponentPool(T prefab)
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

        /// <summary>Rents a <typeparamref name="T"/> from the pool.</summary>
        public T Rent()
        {
            ThrowIfDisposed();
            return ObjectPool.RentOrCreateGameObject(prefab, container);
        }

        /// <summary>Returns a <typeparamref name="T"/> to the pool.</summary>
        public void Return(T rental)
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
