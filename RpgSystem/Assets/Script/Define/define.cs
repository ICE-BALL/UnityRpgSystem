using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class define
{
    static List<GameObject> _monsterList = new List<GameObject>();
    public static List<GameObject> MonsterList { get { return _monsterList; } }

    public enum MonsterType
    {
        Skeleton,
    }

    
}
