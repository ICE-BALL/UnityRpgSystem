using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    float _sensitivity = 1;
    [SerializeField]
    float _wheelSpeed = 5;

    public Vector3 _delta;

    public float _mouseX;
    public float _mouseY;
    public float _mouseWheel;

    float _xAngle;
    float _yAngle;

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
        _mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        MouseUpdate();
        
    }

    void MouseUpdate()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _xAngle += _mouseX * _sensitivity;
        _yAngle += _mouseY * _sensitivity;

        _yAngle = Mathf.Clamp(_yAngle, -50, 30);//-50, 30);

        transform.rotation = Quaternion.Euler(_yAngle, _xAngle, 0);
    }

    private void LateUpdate()
    {
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if ((transform.position - Camera.main.transform.position).magnitude <= 2)
        {
            if (_mouseWheel < 0)
            {
                Camera.main.transform.position += -dir * Time.deltaTime * _wheelSpeed;
            }
        }
        else if ((transform.position - Camera.main.transform.position).magnitude >= 8)
        {
            if (_mouseWheel > 0)
            {
                Camera.main.transform.position += dir * Time.deltaTime * _wheelSpeed;
            }
        }
        else
        {
            if (_mouseWheel > 0)
            {
                Camera.main.transform.position += dir * Time.deltaTime * _wheelSpeed;
            }
            else if (_mouseWheel < 0)
            {
                Camera.main.transform.position += -dir * Time.deltaTime * _wheelSpeed;
            }
        }

        transform.position = _player.transform.position + _delta;

    }
}
