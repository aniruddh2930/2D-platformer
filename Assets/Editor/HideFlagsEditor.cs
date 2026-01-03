// 03/01/2026 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class HideFlagsEditor : EditorWindow
{
    private GameObject selectedObject;
    private HideFlags selectedHideFlags;

    [MenuItem("Tools/Hide Flags Editor")]
    public static void ShowWindow()
    {
        GetWindow<HideFlagsEditor>("Hide Flags Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Hide Flags Editor", EditorStyles.boldLabel);

        selectedObject = (GameObject)EditorGUILayout.ObjectField("Target GameObject", selectedObject, typeof(GameObject), true);

        if (selectedObject != null)
        {
            selectedHideFlags = (HideFlags)EditorGUILayout.EnumPopup("Hide Flags", selectedHideFlags);

            if (GUILayout.Button("Apply Hide Flags"))
            {
                ApplyHideFlags();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Please select a GameObject to modify its Hide Flags.", MessageType.Info);
        }
    }

    private void ApplyHideFlags()
    {
        if (selectedObject != null)
        {
            selectedObject.hideFlags = selectedHideFlags;
            EditorUtility.SetDirty(selectedObject);
            Debug.Log($"HideFlags for {selectedObject.name} set to {selectedHideFlags}");
        }
    }
}
