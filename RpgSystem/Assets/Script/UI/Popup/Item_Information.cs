using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Information : MonoBehaviour
{
    [SerializeField]
    Transform _panel;
    [SerializeField]
    Transform _backGround;
    [SerializeField]
    GameObject _use;
    [SerializeField]
    GameObject _remove;

    string _itemName = "";

    private void Start()
    {
        _panel = transform.GetChild(1);
        _backGround = transform.GetChild(0);
        _use = _panel.GetChild(2).gameObject;
        _remove = _panel.GetChild(3).gameObject;

        UIManager.BindEvent(_use, UseItem, UIManager.UIEvent.Click);
        UIManager.BindEvent(_remove, RemoveItem, UIManager.UIEvent.Click);
        UIManager.BindEvent(_backGround.gameObject, Close, UIManager.UIEvent.Click);
    }

    public void Information(string name)
    {
        _itemName = name;
        _panel = transform.GetChild(1);
        if (name != "empty")
        {
            if (UnityEngine.Resources.Load($"Art/UI/Inventory/Weapon/{name}") != null)
                _panel.GetChild(0).GetComponent<Image>().sprite = UnityEngine.Resources.Load<Sprite>($"Art/UI/Inventory/Weapon/{name}");
            else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
                _panel.GetChild(0).GetComponent<Image>().sprite = UnityEngine.Resources.Load<Sprite>($"Art/UI/Inventory/Consumables/{name}");
            else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
                _panel.GetChild(0).GetComponent<Image>().sprite = UnityEngine.Resources.Load<Sprite>($"Art/UI/Inventory/Quest Items/{name}");
        }
        else
            _panel.GetChild(0).GetComponent<Image>().sprite = UnityEngine.Resources.Load<Sprite>($"Art/UI/{name}");

        _panel.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
    }

    void UseItem(PointerEventData data)
    {
        // TODO
    }

    void RemoveItem(PointerEventData data)
    {
        UI_Inven.RemoveItem(_itemName);
        UIManager.CloseUI(gameObject);
        Debug.Log($"Removed {_itemName}");
    }

    void Close(PointerEventData data)
    {
        UIManager.CloseUI(gameObject);
    }
}
