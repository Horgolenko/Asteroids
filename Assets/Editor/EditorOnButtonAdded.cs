using Audio;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

namespace Editor
{
    [InitializeOnLoad]
    public class EditorOnButtonAdded
    {
        static EditorOnButtonAdded()
        {
            EditorApplication.hierarchyChanged -= EditorApplicationOnhierarchyChanged;
            EditorApplication.hierarchyChanged += EditorApplicationOnhierarchyChanged;
            
            ObjectFactory.componentWasAdded -= HandleComponentAdded;
            ObjectFactory.componentWasAdded += HandleComponentAdded;
 
            EditorApplication.quitting -= OnEditorQuiting;
            EditorApplication.quitting += OnEditorQuiting;
        }

        private static void EditorApplicationOnhierarchyChanged()
        {
            var buttons = Resources.FindObjectsOfTypeAll<Button>();

            foreach (var button in buttons)
            {
                if (!button.TryGetComponent<ButtonAudio>(out var audio))
                {
                    button.AddComponent<ButtonAudio>();
                }
                if (!button.TryGetComponent<ButtonScale>(out var scale))
                {
                    button.AddComponent<ButtonScale>();
                }
            }
        }

        private static void HandleComponentAdded(Component obj)
        {
            if (obj is Button button)
            {
                if (!button.TryGetComponent<ButtonAudio>(out var audio))
                {
                    button.AddComponent<ButtonAudio>();
                }
                if (!button.TryGetComponent<ButtonScale>(out var scale))
                {
                    button.AddComponent<ButtonScale>();
                }
            }
        }
 
        private static void OnEditorQuiting()
        {
            ObjectFactory.componentWasAdded -= HandleComponentAdded;
            EditorApplication.quitting -= OnEditorQuiting;
            EditorApplication.hierarchyChanged -= EditorApplicationOnhierarchyChanged;
        }
    }
}