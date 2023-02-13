using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData
{
    static int _hp = 0;
    static int _mp = 0;
    static int _apple = 0;
    static int _book = 0;
    static int _ingots = 0;
    static int _meat = 0;

    public static int Hp { get { return _hp; } set { _hp = value; } }
    public static int Mp { get { return _mp;} set { _mp = value; } }
    public static int Apple { get { return _apple; } set { _apple = value; } }
    public static int Book { get { return _book; } set { _book = value; } }
    public static int Ingots { get { return _ingots; } set { _ingots = value; } }
    public static int Meat { get { return _meat; } set { _meat = value; } }

    public static void AddItem(string name)
    {
        switch (name)
        {
            case "hp":
                ItemData.Hp += 1;
                break;
            case "mp":
                ItemData.Mp += 1;
                break;
            case "apple":
                ItemData.Apple += 1;
                break;
            case "book":
                ItemData.Book += 1;
                break;
            case "ingots":
                ItemData.Ingots += 1;
                break;
            case "Meat":
                ItemData.Meat += 1;
                break;
            default:
                break;
        }
    }

    public static bool FirstItem(string name)
    {
        switch (name)
        {
            case "hp":
                if (ItemData.Hp == 0)
                    return true;
                break;
            case "mp":
                if (ItemData.Mp == 0)
                    return true;
                break;
            case "apple":
                if (ItemData.Apple == 0)
                    return true;
                break;
            case "book":
                if (ItemData.Book == 0)
                    return true;
                break;
            case "ingots":
                if (ItemData.Ingots == 0)
                    return true;
                break;
            case "Meat":
                if (ItemData.Meat == 0)
                    return true;
                break;
            default:
                break;
        }

        return false;
    }

    public static string ItemText(string name)
    {
        switch (name)
        {
            case "hp":
                return $"{ItemData.Hp}";
            case "mp":
                return $"{ItemData.Mp}";
            case "apple":
                return $"{ItemData.Apple}";
            case "book":
                return $"{ItemData.Book}";
            case "ingots":
                return $"{ItemData.Ingots}";
            case "Meat":
                return $"{ItemData.Meat}";
            default:
                break;
        }

        return null;
    }

    public static int ReturnItem(string name)
    {
        switch (name)
        {
            case "hp":
                return Hp;
            case "mp":
                return Mp;
            case "apple":
                return Apple;
            case "book":
                return Book;
            case "ingots":
                return Ingots;
            case "Meat":
                return Meat;
            default:
                break;
        }

        return 0;
    }

    public static void MinusItem(string name, int num)
    {
        switch (name)
        {
            case "hp":
                Hp -= num;
                break;
            case "mp":
                Mp -= num;
                break;
            case "apple":
                Apple -= num;
                break;
            case "book":
                Book -= num;
                break;
            case "ingots":
                Ingots -= num;
                break;
            case "Meat":
                Meat -= num;
                break;
            default:
                break;
        }
    }
}
