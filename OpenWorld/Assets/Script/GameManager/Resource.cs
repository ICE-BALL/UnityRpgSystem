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

        return Object.Instantiate(gameObject, parent);
    }

    public static void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
