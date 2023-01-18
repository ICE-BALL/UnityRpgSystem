using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : BaseController
{
    #region º¯¼ö
    [SerializeField]
    GameObject _camera;
    Stat _stat;
    public float _speed;
    Vector3[] _dirs = new Vector3[4];
    bool _attacking = false;

    State _state = State.Idle;

    Transform _enemy;
    LayerMask _mask;
    #endregion

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main.transform.parent.gameObject;
        _stat = GetComponent<Stat>();
        _speed = _stat.MoveSpeed;
        _mask = LayerMask.GetMask("Monster");
    }

    void Update()
    {
        GameUpdate();
        StateUpdate();
    }

    #region Animation Event
    void OnHitEvent()
    {
        _attacking = false;
        _state = State.Idle;
    }

    void OnDieEvent()
    {
        Resource.Destroy(gameObject);
    }

    void OnAttack()
    {
        Vector3 dis = transform.localRotation * Vector3.forward;
        RaycastHit hit;

        if (Physics.Raycast((transform.position + Vector3.up), dis, out hit, 2, _mask))
        {
            _enemy = hit.transform;
            Stat EnemyStat = _enemy.GetComponent<Stat>();
            EnemyStat.Hp -= _stat.Attack;
        }
    }
    #endregion

    #region Game
    void GameUpdate()
    {
        if (Input.GetMouseButtonDown(0) && _attacking == false)
        {
            _attacking = true;
            _state = State.Attack;
        }
        else
        {
            PlayerMove();
        }

    }

    void PlayerMove()
    {
        _dirs[0] = _camera.transform.localRotation * Vector3.forward;
        _dirs[1] = _camera.transform.localRotation * Vector3.back;
        _dirs[2] = _camera.transform.localRotation * Vector3.right;
        _dirs[3] = _camera.transform.localRotation * Vector3.left;
        
        if (Input.anyKey != false && _attacking == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirs[0]), 0.5f);
                transform.position += _dirs[0] * _speed * Time.deltaTime;
                _state = State.Run;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirs[1]), 0.5f);
                transform.position += _dirs[1] * _speed * Time.deltaTime;
                _state = State.Run;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirs[2]), 0.5f);
                transform.position += _dirs[2] * _speed * Time.deltaTime;
                _state = State.Run;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dirs[3]), 0.5f);
                transform.position += _dirs[3] * _speed * Time.deltaTime;
                _state = State.Run;
            }
        }
        else
            _state = State.Idle;
    }
    #endregion

    #region State Function
    void StateUpdate()
    {
        if (_stat.Hp <= 0)
            _state = State.Die;
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

    void Idle()
    {
        if (_attacking == false)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("Speed", 0f);
        }
        else
            _state = State.Attack;
    }

    void Run()
    {
        if (_attacking == false)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("Speed", 5.0f);
        }
        else
            _state = State.Attack;
    }

    void Attack()
    {
        if (_attacking == true)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Attack");
        }
    }

    void Die()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("DAMAGED01");
    }
    #endregion
}
