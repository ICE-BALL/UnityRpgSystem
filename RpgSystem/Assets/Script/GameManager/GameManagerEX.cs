using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class GameManagerEX : MonoBehaviour
{
    [SerializeField]
    GameObject _Root;

    int _maxMonster = 10;
    int _monsterCount = 0;

    float _timer = 0.5f;

    System.Random _r = new System.Random();

    private void Awake()
    {
        if (_Root == null)
            _Root = new GameObject() { name = "@Root" };
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_Root);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Resource.Instantiate("Game/unitychan");
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if (_timer <= 0)
        {
            if (_maxMonster > _monsterCount || _Root.transform.childCount < _maxMonster)
            {
                int x = _r.Next(42, 141);
                float y = 0.006f;
                int z = _r.Next(50, 156);

                Vector3 vec = new Vector3(x, y, z);

                GameObject monster = Resource.Instantiate("Game/SKELETON");
                monster.transform.position = vec;
                monster.transform.SetParent(_Root.transform);
                _monsterCount += 1;
            }
            _timer = 0.5f;
        }
    }
}
