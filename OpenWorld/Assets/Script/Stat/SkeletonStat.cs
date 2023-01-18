using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStat : Stat
{
    private void Awake()
    {
        _level = 1;
        _hp = 50;
        _maxHp = 50;
        _attack = 5;
        _defense = 1;
        _moveSpeed = 3.0f;
    }
}
