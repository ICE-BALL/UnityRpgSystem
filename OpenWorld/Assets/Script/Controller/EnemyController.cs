using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
    GameObject _player;
    State _state = State.Idle;
    NavMeshAgent _nav;
    Stat _stat;
    LayerMask _mask;

    float _scanRange = 10f;
    float _attackRange = 2f;
    bool _attacking = false;
    

    void Start()
    {
        _stat = GetComponent<Stat>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _nav = GetComponent<NavMeshAgent>();
        _mask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
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

    #region Animation Event
    void OnHitEvent()
    {
        
        _state = State.Idle;
        _attacking = false;
    }

    void OnDieEvent()
    {
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
        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance <= _attackRange && _attacking == false)
        {
            _attacking = true;
            transform.LookAt(_player.transform);
            _state = State.Attack;
            return;
        }

        if (distance <= _scanRange && distance >= _attackRange)
        {
            _state = State.Run;
            FindTarget();
        }
        else
            _state = State.Idle;
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
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }

    void Die()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("DAMAGED01");
    }
    #endregion
}