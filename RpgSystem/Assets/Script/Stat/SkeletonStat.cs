using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStat : Stat
{
    int _lastHp;
    int _lastAttack;
    int[] array;

    private void Awake()
    {
        _lastHp = 50;
        _lastAttack = 5;
        SetSkeletonLevel(GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>().Level);
        _defense = 1;
        _moveSpeed = 3.0f;
    }

    public void SetSkeletonLevel(int Playerlevel)
    {
        switch (Playerlevel)
        {
            case 1:
                _level = 1;
                break;
            case 2:
                _level = 1;
                break;
            case 3:
                _level = 2;
                break;
            case 4:
                _level = 3;
                break;
            case 5:
                _level = 4;
                break;
            case 6:
                _level = 5;
                break;
            case 7:
                _level = 5;
                break;
            case 8:
                _level = 6;
                break;
            case 9:
                _level = 7;
                break;
            case 10:
                _level = 8;
                break;
        }
        SetStat();
    }

    public void SetStat()
    {
        switch (_level)
        {
            case 1:
                _hp = 50;
                _maxHp = 50;
                _attack = 5;
                break;
            case 2:
                array = GetLast(1);
                break;
            case 3:
                array = GetLast(2);
                break;
            case 4:
                array = GetLast(3);
                break;
            case 5:
                array = GetLast(4);
                break;
            case 6:
                array = GetLast(5);
                break;
            case 7:
                array = GetLast(6);
                break;
            case 8:
                array = GetLast(7);
                break;
        }
    }

    void LevelSystem(int lastHp, int lastAttack)
    {
        _maxHp = lastHp + lastHp / 5;
        _hp = _maxHp;
        _attack = lastAttack + 1;
        _lastHp = _maxHp;
        _lastAttack = _attack;
    }

    int[] GetLast(int index)
    {
        for (int i = 0; i < index; i++)
        {
            LevelSystem(_lastHp, _lastAttack);
        }

        int[] INT = new int[2] { _lastHp, _lastAttack };
        _lastHp = 50;
        _lastAttack = 5;
        return INT;
    }
}
