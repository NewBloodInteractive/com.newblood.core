# New Blood Core Library
A collection of general APIs to assist development.
## Singletons
The `MonoSingleton` class enables boilerplate-free implementation of the singleton pattern for `MonoBehaviour`.
```cs
using NewBlood;
using UnityEngine;
using System.Collections;

public sealed class CoroutineDispatcher : MonoSingleton<CoroutineDispatcher>
{
    void Awake()
    {
        MakeCurrent();
    }

    public static new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return ((MonoBehaviour)Instance).StartCoroutine(coroutine);
    }
}
```
Above is a simple example showcasing one use of the `MonoSingleton` class. It enables the developer to start a coroutine in a "fire and forget" fashion, without needing to create a new game object.
```cs
IEnumerator ExampleCoroutine()
{
    Debug.Log("Hello.");
    yield return new WaitForSeconds(1);
    Debug.Log("Hello, one second later.");
}

// ...
CoroutineDispatcher.StartCoroutine(ExampleCoroutine());
```
However, keen observers may have noticed that the `CoroutineDispatcher` class is not entirely boilerplate-free: it was still required to implement an `Awake` method to make itself available globally. This problem is solved through use of the `PersistentObjects` class.

## PersistentObjects
The `PersistentObjects` class is used to manage the initialization of objects that exist outside the lifetime of a scene.

This is achieved by defining a `SubsystemRegistration` callback in your code that calls the `PersistentObjects.Initialize` method.

The `Initialize` method receives a prefab as input. This prefab is used to generate persistent instances at initialization time.
* Locating the prefab is the responsibility of the caller.
  * This can be done via `Resources.Load`, `AddressableAssets.LoadAssetAsync`, or other means.
* The prefab's root game object must not contain any components. It acts purely as a container.
* Children of the prefab's root game object will become persistent objects once `Initialize` is called.
* Any `MonoSingleton` components located within the prefab's hierarchy will be marked current.

## Complete Example
Given a prefab located at `Assets/Resources/PersistentObjects.prefab` with the following layout:

* PersistentObjects
  * Transform
* PersistentObjects/CoroutineDispatcher
  * Transform
  * CoroutineDispatcher

### `Assets/PersistentObjectsInitializer.cs`
```cs
using NewBlood;
using UnityEngine;

static class PersistentObjectsInitializer
{
    static GameObject GetPrefab()
    {
        return Resources.Load<GameObject>("PersistentObjects");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void OnSubsystemRegistration()
    {
        PersistentObjects.Initialize(GetPrefab());
    }
}
```
### `Assets/CoroutineDispatcher.cs`
```cs
using NewBlood;
using UnityEngine;
using System.Collections;

public sealed class CoroutineDispatcher : MonoSingleton<CoroutineDispatcher>
{
    public static new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return ((MonoBehaviour)Instance).StartCoroutine(coroutine);
    }
}
```
Supporting new singletons is as simple as adding a new child object to the `PersistentObjects` prefab with the component.

## RegisterSingletonAttribute
An additional method of defining singletons is to use `RegisterSingletonAttribute`, which can be more convenient for trivial singletons that don't need to be wired up in the inspector. Usage is similar to that of `PersistentObjects`:

### `Assets/SingletonInitializer.cs`
```cs
using NewBlood;
using UnityEngine;

static class SingletonInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void OnSubsystemRegistration()
    {
        RegisterSingletonAttribute.Initialize(typeof(SingletonInitializer).Assembly);
    }
}
```

### `Assets/CoroutineDispatcher.cs`
```cs
using NewBlood;
using UnityEngine;
using System.Collections;

[assembly: RegisterSingleton(typeof(CoroutineDispatcher))]
public sealed class CoroutineDispatcher : MonoSingleton<CoroutineDispatcher>
{
    public static new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return ((MonoBehaviour)Instance).StartCoroutine(coroutine);
    }
}
```
