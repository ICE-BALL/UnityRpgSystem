using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    public static GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject gameObject = Load<GameObject>($"Prefabs/{path}");
        if (gameObject== null)
        {
            Debug.Log($"Failed to instantiate -> Prefabs/{path}");
            return null;
        }

        GameObject go = Object.Instantiate(gameObject, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public static void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
