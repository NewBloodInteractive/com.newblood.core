using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NewBlood
{
    /// <summary>A reference to a scene.</summary>
    [Serializable]
    public sealed class SceneReference : ISerializationCallbackReceiver
    {
    #if UNITY_EDITOR
        [SerializeField]
        SceneAsset m_Scene;
    #endif

        [SerializeField]
        [HideInInspector]
        string m_ScenePath;

    #if UNITY_EDITOR
        /// <summary>Initializes a new <see cref="SceneReference"/> instance.</summary>
        public SceneReference(SceneAsset scene)
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            m_ScenePath = AssetDatabase.GetAssetPath(scene);
            m_Scene     = scene;
        }

        /// <summary>Initializes a new <see cref="SceneReference"/> instance.</summary>
        public SceneReference(Scene scene)
        {
            if (!scene.IsValid())
                throw new ArgumentException("Scene is not valid.", nameof(scene));

            m_ScenePath = scene.path;
            m_Scene     = AssetDatabase.LoadAssetAtPath<SceneAsset>(m_ScenePath);
        }
    #else
        /// <summary>Initializes a new <see cref="SceneReference"/> instance.</summary>
        public SceneReference(Scene scene)
        {
            if (!scene.IsValid())
                throw new ArgumentException("Scene is not valid.", nameof(scene));

            m_ScenePath = scene.path;
        }
    #endif

        /// <summary>Gets the build index of the scene referenced by this instance.</summary>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>Gets the path of the scene referenced by this instance.</summary>
        public string Path
        {
            get
            {
            #if UNITY_EDITOR
                if (m_Scene != null)
                    return AssetDatabase.GetAssetPath(m_Scene);
            #endif

                return m_ScenePath;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        #if UNITY_EDITOR
            m_ScenePath = AssetDatabase.GetAssetPath(m_Scene);
        #endif
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
    }
}
