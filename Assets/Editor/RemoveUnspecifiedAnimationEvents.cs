// 14/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class RemoveUnspecifiedAnimationEvents
{
    [MenuItem("Tools/Remove Unspecified Animation Events")]
    public static void RemoveUnspecifiedEvents()
    {
        // Get all animation clips in the project
        string[] guids = AssetDatabase.FindAssets("t:AnimationClip");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);

            if (clip != null)
            {
                // Get all animation events
                AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
                bool modified = false;

                // Filter out events with unspecified function names
                var filteredEvents = new System.Collections.Generic.List<AnimationEvent>();
                foreach (var animEvent in events)
                {
                    if (!string.IsNullOrEmpty(animEvent.functionName))
                    {
                        filteredEvents.Add(animEvent);
                    }
                    else
                    {
                        modified = true;
                    }
                }

                // Apply the filtered events back to the clip
                if (modified)
                {
                    AnimationUtility.SetAnimationEvents(clip, filteredEvents.ToArray());
                    Debug.Log($"Removed unspecified animation events from: {clip.name}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Finished removing unspecified animation events.");
    }
}
