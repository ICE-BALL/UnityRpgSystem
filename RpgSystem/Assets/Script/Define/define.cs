using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class define
{
    static List<GameObject> _monsterList = new List<GameObject>();
    public static List<GameObject> MonsterList { get { return _monsterList; } }

    public static List<string> _weaponList = new List<string>();
    public static List<string> _consumablesList = new List<string>();
    public static List<string> _questList = new List<string>();

    public static Dictionary<int, GameObject> _invenObjects = new Dictionary<int, GameObject>();

    public enum MonsterType
    {
        Skeleton,
    }

    public enum InventoryType
    {
        WeaponAndArmor,
        Consumables,
        Quest,
    }

    public enum Inven_LoadType
    {
        Load,
        ReLoad,
    }
}
