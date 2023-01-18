using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    public Vector3 _delta;

    public float _mouseX;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player == null)
            {
                Debug.Log("Missing Player Object");
            }
        }
    }

    void Update()
    {
        MouseUpdate();
    }

    void MouseUpdate()
    {
        _mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(0, _mouseX, 0);

    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _delta;
    }
}
