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

    public static List<GameObject> _UiList = new List<GameObject>();

    public static void ShowSceneUI<T>(string name) where T : Component
    {
        GameObject go = null;
        foreach (GameObject item in _UiList)
        {
            if (item.name == name)
                go = item;
        }
        if (go == null)
            go = Resource.Instantiate($"UI/UI_Scene/{name}", GameObject.Find("@UI_Root").transform);
        go.SetActive(true);
        GetOrAddComponent<T>(go);
    }

    public static void ShowInventoryUI<T>(string name) where T : Component
    {
        //GameObject go = null;
        //foreach (GameObject item in _UiList)
        //{
        //    if (item.name == name)
        //        go = item;
        //}
        //if (go == null)
        //    go = Resource.Instantiate($"UI/Inventory/{name}", GameObject.Find("@UI_Root").transform);
        //go.SetActive(true);

        GameObject go = Resource.Instantiate($"UI/Inventory/{name}", GameObject.Find("@UI_Root").transform);
        GetOrAddComponent<T>(go);
    }

    public static void CloseUI(GameObject go, string pool = "pool")
    {
        if (pool == "pool")
        {
            go.SetActive(false);
            foreach (GameObject item in _UiList)
                if (item.transform.name == go.transform.name)
                    return;

            _UiList.Add(go);
        }
        else
            Resource.Destroy(go);
        
    }

    [Obsolete]
    public static void LoadInventoryUI(define.InventoryType type, GameObject parent)
    {
        Transform child = parent.transform.FindChild("Inven");
        List<string> list;
        switch (type)
        {
            case define.InventoryType.WeaponAndArmor:
                list = define._weaponList;
                break;
            case define.InventoryType.Consumables:
                list = define._consumablesList;
                break;
            case define.InventoryType.Quest:
                list = define._questList;
                break;
        }

        for (int i = 0; i < 30; i++)
        {
            MakeSlot(child);
        }
    }

    public static void MakeSlot(Transform parent)
    {
        // TODO
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
