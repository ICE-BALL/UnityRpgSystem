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
    GameObject _consumables;
    [SerializeField]
    GameObject _quest;
    [SerializeField]
    GameObject _close;

    [System.Obsolete]
    private void Start()
    {
        _inven = UIManager.FindChild<Button>(gameObject, "Inven");
        _weapon = UIManager.FindChild<Button>(gameObject, "Weapon/Armor");
        _consumables = UIManager.FindChild<Button>(gameObject, "Consumables");
        _quest = UIManager.FindChild<Button>(gameObject, "Quest items");
        _close = UIManager.FindChild<Button>(gameObject, "Close");

        UIManager.BindEvent(_weapon, Weapon, UIManager.UIEvent.Click);
        UIManager.BindEvent(_consumables, Consumables, UIManager.UIEvent.Click);
        UIManager.BindEvent(_quest, Quest, UIManager.UIEvent.Click);
        UIManager.BindEvent(_close, CloseBag, UIManager.UIEvent.Click);
    }

    [System.Obsolete]
    void Weapon(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.WeaponAndArmor, gameObject, define.Inven_LoadType.ReLoad);
    }

    [System.Obsolete]
    void Consumables(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.Consumables, gameObject, define.Inven_LoadType.ReLoad);
    }

    [System.Obsolete]
    void Quest(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.Quest, gameObject, define.Inven_LoadType.ReLoad);
    }

    void CloseBag(PointerEventData data)
    {
        define._invenObjects.Clear();
        UIManager.ShowSceneUI<UI_Scene>("UI_Scene");
        UIManager.CloseUI(gameObject, null);
    }

    public static void AddItem(string name)
    {
        if (UnityEngine.Resources.Load($"Art/UI/Inventory/Armor/{name}") != null)
            define._weaponList.Add(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
            define._consumablesList.Add(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
            define._questList.Add(name);

    }

    public static void RemoveItem(string name)
    {
        if (UnityEngine.Resources.Load($"Art/UI/Inventory/Armor/{name}") != null)
            define._weaponList.Remove(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
            define._consumablesList.Remove(name);
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
            define._questList.Remove(name);
    }
}
