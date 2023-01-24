using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Scene : MonoBehaviour
{
    public GameObject _bag;
    public GameObject _more;

    private void Start()
    {
        _bag = UIManager.FindChild<Button>(gameObject, "Bag");
        _more = UIManager.FindChild<Button>(gameObject, "More");

        UIManager.BindEvent(_bag, Bag, UIManager.UIEvent.Click);
    }

    void Bag(PointerEventData data)
    {
        UIManager.ShowInventoryUI<UI_Inven>("UI_Inven");
        UIManager.CloseUI(gameObject);
    }
}
