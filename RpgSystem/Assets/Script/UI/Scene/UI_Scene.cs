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

    Action openbag = null;

    private void Start()
    {
        _bag = UIManager.FindChild<Button>(gameObject, "Bag");
        _more = UIManager.FindChild<Button>(gameObject, "More");

        openbag -= Bag;
        openbag += Bag;
        UIManager.BindEvent(_bag, Bag, UIManager.UIEvent.Click);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
            openbag.Invoke();
    }

    void Bag(PointerEventData data)
    {
        UIManager.ShowInventoryUI<UI_Inven>("UI_Inven");
        UIManager.CloseUI(gameObject);
    }

    void Bag()
    {
        UIManager.ShowInventoryUI<UI_Inven>("UI_Inven");
        UIManager.CloseUI(gameObject);
    }
}
