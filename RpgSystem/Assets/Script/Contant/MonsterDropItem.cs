using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropItem
{
    static List<string> _dropAble = new List<string>() { "hp", "mp" };

    public static void DropItem(define.MonsterType type)
    {
        switch (type)
        {
            case define.MonsterType.Skeleton:
                int r = Random.Range(0, 2);
                UI_Inven.AddItem(_dropAble[r]);
                break;
        }
    }
}
