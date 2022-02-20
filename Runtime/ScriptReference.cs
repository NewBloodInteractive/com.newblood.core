using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NewBlood
{
    /// <summary>A reference to a script asset.</summary>
    [Serializable]
    public sealed class ScriptReference : ISerializationCallbackReceiver
    {
    #if UNITY_EDITOR
        [SerializeField]
        MonoScript m_ScriptAsset;
    #endif

        [SerializeField]
        [HideInInspector]
        string m_TypeName;

    #if UNITY_EDITOR
        /// <summary>Initializes a new <see cref="ScriptReference"/> instance.</summary>
        public ScriptReference(MonoScript script)
        {
            if (script == null)
                throw new ArgumentNullException(nameof(script));

            m_ScriptAsset = script;
        }

        /// <summary>Initializes a new <see cref="ScriptReference"/> instance.</summary>
        public ScriptReference(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            m_ScriptAsset = GetMonoScript(type);
            m_TypeName    = type.AssemblyQualifiedName;
        }

        static MonoScript GetMonoScript(Type type)
        {
            foreach (MonoScript script in MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (script.GetClass() == type)
                {
                    return script;
                }
            }

            return null;
        }
    #else
        /// <summary>Initializes a new <see cref="ScriptReference"/> instance.</summary>
        public ScriptReference(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            m_TypeName = type.AssemblyQualifiedName;
        }
    #endif

        /// <summary>Gets the <see cref="System.Type"/> associated with the script referenced by this instance.</summary>
        public Type Type
        {
            get
            {
            #if UNITY_EDITOR
                if (m_ScriptAsset != null)
                    return m_ScriptAsset.GetClass();
            #endif

                if (string.IsNullOrEmpty(m_TypeName))
                    return null;

                return Type.GetType(m_TypeName, throwOnError: true);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        #if UNITY_EDITOR
            if (m_ScriptAsset != null)
            {
                m_TypeName = m_ScriptAsset.GetClass().AssemblyQualifiedName;
            }
        #endif
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
    }
}
