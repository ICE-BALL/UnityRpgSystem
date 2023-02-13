using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.VisualScripting;

public class GameManagerEX : MonoBehaviour
{
    [SerializeField]
    GameObject _Root;
    [SerializeField]
    GameObject _UIRoot;

    int _maxMonster = 10;
    int _monsterCount = 0;

    float _timer = 0f;

    System.Random _r = new System.Random();

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (_Root == null)
            _Root = new GameObject() { name = "@Root" };
        if (_UIRoot == null)
            _UIRoot = new GameObject() { name = "@UI_Root" };
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_UIRoot);
        DontDestroyOnLoad(_Root);
        GameObject _camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (_camera == null)
            Resource.Instantiate("Game/Cam");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Resource.Instantiate("Game/unitychan");
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        SpawnMonster();
        if (Input.GetKey(KeyCode.LeftAlt) || UI_Inven._isOpenBag)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void SpawnMonster()
    {
        if (_timer <= 0)
        {
            if (_maxMonster > _monsterCount || _Root.transform.childCount < _maxMonster)
            {
                int x = _r.Next(42, 141);
                int z = _r.Next(50, 156);

                Vector3 vec = new Vector3(x, 0f, z);

                GameObject monster = Resource.Instantiate("Game/SKELETON");
                monster.transform.position = vec;
                monster.transform.SetParent(_Root.transform);
                define.MonsterList.Add(monster);
                _monsterCount += 1;
            }
            _timer = 0.5f;
        }
    }
}
