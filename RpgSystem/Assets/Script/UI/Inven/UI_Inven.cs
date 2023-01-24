using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : MonoBehaviour
{
    [SerializeField]
    UI_Scene _scene;

    [SerializeField]
    GameObject _inven;
    [SerializeField]
    GameObject _weapon;
    [SerializeField]
    GameObject _posion;
    [SerializeField]
    GameObject _close;

    private void Start()
    {
        _inven = UIManager.FindChild<Button>(gameObject, "Inven");
        _weapon = UIManager.FindChild<Button>(gameObject, "Weapon");
        _posion = UIManager.FindChild<Button>(gameObject, "Posion");
        _close = UIManager.FindChild<Button>(gameObject, "Close");

        UIManager.BindEvent(_close, CloseBag, UIManager.UIEvent.Click);
    }

    void CloseBag(PointerEventData data)
    {
        UIManager.ShowSceneUI<UI_Scene>("UI_Scene");
        UIManager.CloseUI(gameObject);
    }
}
