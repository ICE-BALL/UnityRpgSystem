using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : MonoBehaviour
{
    [SerializeField]
    GameObject _image;
    [SerializeField]
    GameObject _itemNumText;

    string _itemName = "";
    int _itemIndex;

    public Action _init = null;

    private void Awake()
    {
        _image = UIManager.FindChild<Image>(gameObject);
        _itemNumText = UIManager.FindChild<Text>(gameObject);

        _init -= Init;
        _init += Init;
        UIManager.BindEvent(gameObject, Click, UIManager.UIEvent.Click);
        UIManager.BindEvent(_image, Click, UIManager.UIEvent.Click);
    }

    private void Init()
    {
        _itemName = gameObject.name;
        for (int i = 0; i < define._invenObjects.Count; i++)
            if (define._invenObjects[i] == gameObject)
                _itemIndex = i;

        if (ItemData.FirstItem(_itemName))
            _itemNumText.SetActive(false);
        else
            _itemNumText.SetActive(true);
        _itemNumText.GetComponent<Text>().text = ItemData.ItemText(_itemName);
    }

    void Click(PointerEventData data)
    {
        _itemName = gameObject.name;
        for (int i = 0; i < 30; i++)
            if (define._invenObjects[i] == gameObject)
                _itemIndex = i;

        Debug.Log($"{_itemIndex}¹ø {_itemName} Å¬¸¯");
        if (_itemName != "empty")
            UIManager.ShowInformationUI<Item_Information>(_itemName);
    }
}
