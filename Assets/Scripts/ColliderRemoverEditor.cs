using UnityEditor;
using UnityEngine;

public class BatchColliderRemover : EditorWindow
{
    [MenuItem("Tools/Remove Colliders from Selected %#d")] // Ctrl+D or Cmd+D
    static void RemoveCollidersFromSelection()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                DestroyImmediate(col);
            }
        }
        Debug.Log("Removed colliders from selected objects");
    }
}
