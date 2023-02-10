using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : MonoBehaviour
{
    [SerializeField]
    GameObject _image;

    string _itemName = "";
    int _itemIndex;

    private void Start()
    {
        _image = UIManager.FindChild<Image>(gameObject);

        UIManager.BindEvent(gameObject, Click, UIManager.UIEvent.Click);
        UIManager.BindEvent(_image, Click, UIManager.UIEvent.Click);
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
