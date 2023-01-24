using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager
{
    public  static Action OpenBag;
    public Action CloseBag;

    public static void ShowSceneUI<T>(string name) where T : Component
    {
        GameObject go = Resource.Instantiate($"UI/UI_Scene/{name}", GameObject.Find("@UI_Root").transform);
        GetOrAddComponent<T>(go);
    }

    public static void ShowInventoryUI<T>(string name) where T : Component
    {
        GameObject go = Resource.Instantiate($"UI/Inventory/{name}", GameObject.Find("@UI_Root").transform);
        GetOrAddComponent<T>(go);
    }

    public static void CloseUI(GameObject go)
    {
        Resource.Destroy(go);
    }

    public enum UIEvent
    {
        Click,
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEvent type)
    {
        UI_EventHandler evt = GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
        }
    }

    public static GameObject FindChild<T>(GameObject go, string name = null) where T : UnityEngine.Component
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).GetComponent<T>() != null)
            {
                if (string.IsNullOrEmpty(name))
                    return go.transform.GetChild(i).gameObject;

                if (go.transform.GetChild(i).gameObject.name == name)
                    return go.transform.GetChild(i).gameObject;
            }

        }

        return null;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

}
