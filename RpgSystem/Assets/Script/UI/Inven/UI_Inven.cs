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

    bool _isOpenBag = false;

    private void Start()
    {
        _isOpenBag = true;
        _inven = UIManager.FindChild<Button>(gameObject, "Inven");
        _weapon = UIManager.FindChild<Button>(gameObject, "Weapon");
        _posion = UIManager.FindChild<Button>(gameObject, "Posion");
        _close = UIManager.FindChild<Button>(gameObject, "Close");

        UIManager.BindEvent(_close, CloseBag, UIManager.UIEvent.Click);
    }

    void CloseBag(PointerEventData data)
    {
        _isOpenBag = false;
        UIManager.ShowSceneUI<UI_Scene>("UI_Scene");
        UIManager.CloseUI(gameObject);
    }

    public void AddItem(string name)
    {
        if (UnityEngine.Resources.Load($"Art/UI/Inventory/Armor/{name}") != null)
            define._weaponList.Add(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
            define._consumablesList.Add(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
            define._questList.Add(name);

    }

    public void RemoveItem(string name)
    {
        if (UnityEngine.Resources.Load($"Art/UI/Inventory/Armor/{name}") != null)
            define._weaponList.Remove(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
            define._consumablesList.Remove(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
            define._questList.Remove(name);
    }
}
