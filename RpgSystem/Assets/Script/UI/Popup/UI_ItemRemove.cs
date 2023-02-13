using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemRemove : MonoBehaviour
{
    GameObject _text;
    GameObject _button;

    private void Start()
    {
        _text = gameObject.transform.GetChild(0).gameObject;
        _button = UIManager.FindChild<Button>(gameObject);

        UIManager.BindEvent(_button, Enter, UIManager.UIEvent.Click);
    }

    void Enter(PointerEventData data)
    {
        string text = _text.GetComponent<TMP_InputField>().text;
        int num;
        if (int.TryParse(text, out num))
        {
            if (num > 0)
            {
                UI_Inven.RemoveItem(Item_Information._itemName, int.Parse(text));
            }

        }

        if (ItemData.ReturnItem(Item_Information._itemName) == 0)
            UI_Inven.RemoveItem(Item_Information._itemName);

        _text.GetComponent<TMP_InputField>().text = "";
        UIManager.CloseUI(gameObject);
    }
}
