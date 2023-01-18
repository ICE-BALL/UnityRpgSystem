using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

public class EnemyController : BaseController
{
    GameObject _player;
    State _state = State.Idle;
    NavMeshAgent _nav;
    Stat _stat;
    LayerMask _mask;
    define.MonsterType _type;

    System.Random _r = new System.Random();

    float _scanRange = 10f;
    float _attackRange = 2f;
    bool _attacking = false;
    bool _targeting = false;

    float _timer = 0;
    int _waitTime = 0;
    float _walkTime = 0;
    int _way = 0;
    float _walkTimer = 0;
    

    void Start()
    {
        string tag = gameObject.tag;
        switch (tag)
        {
            case "Skeleton":
                _type = define.MonsterType.Skeleton;
                _stat = GetComponent<SkeletonStat>();
                break;
        }
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _nav = GetComponent<NavMeshAgent>();
        _mask = LayerMask.GetMask("Player");
        _nav.speed = _stat.MoveSpeed;

        _player.GetComponent<PlayerStat>()._levelUp -= SetMonsterLevel;
        _player.GetComponent<PlayerStat>()._levelUp += SetMonsterLevel;
    }

    private void Update()
    {
        if (!_targeting)
        {
            _timer += Time.deltaTime;
            _walkTimer += Time.deltaTime;
        }
        
        Targetting();
        switch (_state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Run:
                Run();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    public void SetMonsterLevel()
    {
        if (_stat.Hp <= 0) 
            return;
        switch (_type)
        {
            case define.MonsterType.Skeleton:
                GetComponent<SkeletonStat>().SetSkeletonLevel(_player.GetComponent<Stat>().Level);
                break;
        }
    }

    #region Animation Event
    void OnHitEvent()
    {
        _state = State.Idle;
        _attacking = false;
    }

    void OnDieEvent()
    {
        PlayerStat stat = _player.GetComponent<PlayerStat>();
        stat.Exp += SetMonsterExp();
        
        Resource.Destroy(gameObject);
    }

    void OnAttack()
    {
        RaycastHit hit;
        Vector3 dis = transform.localRotation * Vector3.forward;
        if (Physics.Raycast(transform.position + Vector3.up, dis, out hit, _attackRange, _mask))
        {
            Transform player = hit.transform;
            Stat stat = player.GetComponent<Stat>();
            stat.Hp -= _stat.Attack;
        }
    }
    #endregion

    #region Monster Ai
    void Targetting()
    {
        if (_stat.Hp <= 0)
        {
            _state = State.Die;
            return;
        }

        if (!_targeting)
        {
            if (_waitTime == 0)
                _waitTime = _r.Next(2, 4);
            if (_walkTime == 0)
                _walkTime = _r.Next(3, 6);

            if (_timer >= _waitTime)
            {
                if (_way == 0)
                    _way = _r.Next(1, 5);

                _state = State.Run;
                switch (_way)
                {
                    case 1:
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
                        transform.position += Vector3.forward * _stat.MoveSpeed * Time.deltaTime;
                        break;
                    case 2:
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
                        transform.position += Vector3.back * _stat.MoveSpeed * Time.deltaTime;
                        break;
                    case 3:
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
                        transform.position += Vector3.left * _stat.MoveSpeed * Time.deltaTime;
                        break;
                    case 4:
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
                        transform.position += Vector3.right * _stat.MoveSpeed * Time.deltaTime;
                        break;
                }

                if (_walkTimer >= _walkTime)
                {
                    _timer = 0;
                    _waitTime = 0;
                    _walkTime = 0;
                    _way = 0;
                    _walkTimer = 0;
                }
            }
            else
                _state = State.Idle;
        }

        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance <= _attackRange && _attacking == false)
        {
            _attacking = true;
            transform.LookAt(_player.transform);
            _state = State.Attack;
            return;
        }

        if (distance <= _scanRange) //  && distance >= _attackRange
        {
            _targeting = true;
            _state = State.Run;
            FindTarget();
        }
        else
        {
            _targeting = false;
        }
    }

    void FindTarget()
    {
        _nav.destination = _player.transform.position;
    }

    #endregion

    #region State Functions
    void Idle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0f);
    }

    void Run()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 3.0f);
    }

    void Attack()
    {
        transform.LookAt(_player.transform);
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }

    void Die()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("DAMAGED01");
    }
    #endregion

    int SetMonsterExp()
    {
        int exp = 0;
        switch (_type)
        {
            case define.MonsterType.Skeleton:
                exp = ExpSkeleton();
                break;
        }

        return exp;
    }

    int ExpSkeleton()
    {
        PlayerStat stat = _player.GetComponent<PlayerStat>();
        return _stat.Level + _stat.MaxHp / 10 + _stat.Attack;
    }
}
