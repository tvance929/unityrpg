using System;
using UnityEditor;

// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool isLayerPrioritiesUnfolded = true; // store the UI state

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Serialize cameraRaycaster instance

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Properties");
        if (isLayerPrioritiesUnfolded)
        {
            EditorGUI.indentLevel++;
            {
                BindArraySize();
                BindArrayElements();
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
    }

    void BindArraySize()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
        if (requiredArraySize != currentArraySize)
        {
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
        }
    }

    void BindArrayElements()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++)
        {
            var prop = serializedObject.FindProperty($"layerPriorities.Array.data[{i}]");
            prop.intValue = EditorGUILayout.LayerField($"Layer {i}:", prop.intValue);
        }
    }
}
