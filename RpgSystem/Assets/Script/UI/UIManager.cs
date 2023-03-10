using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public static void ShowPopupUI<T>(string name = null) where T : Component
    {
        if (string.IsNullOrEmpty(name))
            name  = typeof(T).Name;
        GameObject go = null;
        foreach (GameObject item in _UiList)
        {
            if (item.name == name)
                go = item;
        }
        if (go == null)
            go = Resource.Instantiate($"UI/UI_Popup/{name}", GameObject.Find("@UI_Root").transform);
        go.SetActive(true);
        GetOrAddComponent<T>(go);
    }

    public static void ShowInformationUI<T>(string ItemName, string name = null) where T : Component
    {
        GameObject go = null;
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        foreach (GameObject item in _UiList)
        {
            if (item.name == name)
                go = item;
        }
        if (go == null)
            go = Resource.Instantiate($"UI/UI_Popup/{name}", GameObject.Find("@UI_Root").transform);
        go.SetActive(true);
        GetOrAddComponent<T>(go);
        go.GetComponent<Item_Information>().Information(ItemName);
    }

    public static void ShowInventoryUI<T>(string name) where T : Component
    {
        GameObject go = Resource.Instantiate($"UI/Inventory/{name}", GameObject.Find("@UI_Root").transform);
        LoadInventoryUI(define.InventoryType.WeaponAndArmor, define.Inven_LoadType.Load, go);
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

    public static void LoadInventoryUI(define.InventoryType type, define.Inven_LoadType loadType, GameObject parent = null)
    {
        if (parent== null)
            parent = GameObject.Find("UI_Inven");
        Transform child = parent.transform.GetChild(0);
        List<string> list = null;
        string path = null;
        switch (type)
        {
            case define.InventoryType.WeaponAndArmor:
                list = define._weaponList;
                path = "Weapon";
                break;
            case define.InventoryType.Consumables:
                list = define._consumablesList;
                path = "Consumables";
                break;
            case define.InventoryType.Quest:
                list = define._questList;
                path = "Quest Items";
                break;
        }

        for (int i = 0; i < 30; i++)
        {
            GameObject go = null;
            if (loadType == define.Inven_LoadType.Load)
            {
                go = Resource.Instantiate("UI/Inventory/UI_Inven_Item", child);
                if (list.Count <= i)
                    list.Add("empty");
                string name = list[i];
                if (name == "empty")
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Art/UI/{name}");
                else
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Art/UI/Inventory/{path}/{name}");

                go.name = name;
                // 0 ~ 29
                define._invenObjects.Add(i, go);
            }
            else if (loadType == define.Inven_LoadType.ReLoad)
            {
                go = define._invenObjects[i];
                if (list.Count <= i)
                    list.Add("empty");
                string name = list[i];
                if (name == "empty")
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Art/UI/{name}");
                else
                    go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Art/UI/Inventory/{path}/{name}");

                go.name = name;
                define._invenObjects.Remove(i);
                define._invenObjects.Add(i, go);
            }
            go.GetComponent<UI_Inven_Item>()._init.Invoke();

        }
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
