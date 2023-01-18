using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    int _needExp = 20;

    public Action _levelUp;

    public int Exp { get { return _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Awake()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;
    }

    public void Leveling()
    {
        if (_exp >= _needExp)
        {
            _level += 1;
            _exp = _exp - _needExp;
            LevelExp(_level);
            LevelUp(_level);
            _levelUp.Invoke();
        }
    }

    void LevelExp(int level)
    {
        switch (level)
        {
            case 1:
                _needExp = 20;
                break;
            case 2:
                _needExp = _needExp * 2;
                break;
            case 3:
                _needExp = _needExp * 2;
                break;
            case 4:
                _needExp = _needExp * 2;
                break;
            case 5:
                _needExp = _needExp * 2;
                break;
            case 6:
                _needExp = _needExp * 2;
                break;
            case 7:
                _needExp = _needExp * 2;
                break;
            case 8:
                _needExp = _needExp * 2;
                break;
            case 9:
                _needExp = _needExp * 2;
                break;
            case 10:
                _needExp = _needExp * 2;
                break;
        }
    }

    public void LevelUp(int level)
    {
        switch (level)
        {
            case 1:
                break;
            case 2:
                LevelSystem();
                break;
            case 3:
                LevelSystem();
                break;
            case 4:
                LevelSystem();
                break;
            case 5:
                LevelSystem();
                break;
            case 6:
                LevelSystem();
                break;
            case 7:
                LevelSystem();
                break;
            case 8:
                LevelSystem();
                break;
            case 9:
                LevelSystem();
                break;
            case 10:
                LevelSystem();
                break;
        }
    }

    void LevelSystem()
    {
        _maxHp = _maxHp + (_maxHp / 2);
        _hp = _maxHp;
        _attack = _attack + _level;
    }
}
