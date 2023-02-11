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

    static GameObject _this;
    static bool _isOpenBag = false;

    private void Start()
    {
        _this = gameObject;
        _isOpenBag = true;
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

    void Weapon(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.WeaponAndArmor, define.Inven_LoadType.ReLoad);
    }

    void Consumables(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.Consumables, define.Inven_LoadType.ReLoad);
    }

    void Quest(PointerEventData data)
    {
        UIManager.LoadInventoryUI(define.InventoryType.Quest, define.Inven_LoadType.ReLoad);
    }

    void CloseBag(PointerEventData data)
    {
        _isOpenBag = false;
        define._invenObjects.Clear();
        UIManager.ShowSceneUI<UI_Scene>("UI_Scene");
        UIManager.CloseUI(gameObject, null);
    }

    public static void AddItem(string name)
    {
        define.InventoryType type = define.InventoryType.WeaponAndArmor;
        string path = FindInPath(name);
        switch (path)
        {
            case "Weapon":
                type = define.InventoryType.WeaponAndArmor;
                define._weaponList.Insert(FindEmptyIndex(define._weaponList), name);
                break;
            case "Consumables":
                type = define.InventoryType.Consumables;
                define._consumablesList.Insert(FindEmptyIndex(define._consumablesList), name);
                break;
            case "Quedt Items":
                type = define.InventoryType.Quest;
                define._questList.Insert(FindEmptyIndex(define._questList), name);
                break;
        }

        if (_isOpenBag == true)
            UIManager.LoadInventoryUI(type, define.Inven_LoadType.ReLoad, _this);

    }

    public static void RemoveItem(string name)
    {
        define.InventoryType type = define.InventoryType.WeaponAndArmor;
        string path = FindInPath(name);
        switch (path)
        {
            case "Weapon":
                type = define.InventoryType.WeaponAndArmor;
                define._weaponList.Remove(name);
                break;
            case "Consumables":
                type = define.InventoryType.Consumables;
                define._consumablesList.Remove(name);
                break;
            case "Quedt Items":
                type = define.InventoryType.Quest;
                define._questList.Remove(name);
                break;
        }

        if (_isOpenBag == true)
            UIManager.LoadInventoryUI(type, define.Inven_LoadType.ReLoad, _this);
    }

    public static int FindEmptyIndex(List<string> list)
    {
        if (list.Count >= 30)
        {
            for (int i = 0; i < 30; i++)
                if (list[i] == "empty")
                    return i;
        }

        return 0;
    }

    public static string FindInPath(string name)
    {
        if (UnityEngine.Resources.Load($"Art/UI/Inventory/Weapon/{name}") != null)
            return "Weapon";
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Consumables/{name}") != null)
            return "Consumables";
        else if (UnityEngine.Resources.Load($"Art/UI/Inventory/Quest Items/{name}") != null)
            return "Quest Items";

        return null;
    }
}
