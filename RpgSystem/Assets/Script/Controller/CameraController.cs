using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using static UnityEditorInternal.ReorderableList;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    float _sensitivity = 1;
    [SerializeField]
    float _wheelSpeed = 5;

    LayerMask _mask;

    public Vector3 _delta;

    public float _mouseX;
    public float _mouseY;
    public float _mouseWheel;
    
    float _xAngle;
    float _yAngle;

    public Transform referenceTransform;
    public float collisionOffset = 0.3f;
    public float cameraSpeed = 15f;

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;

    private void Start()
    {
        referenceTransform = gameObject.transform;
        defaultPos = Camera.main.transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = gameObject.transform;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);

        _mask = LayerMask.GetMask("Ground");
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
        _yAngle -= _mouseY * _sensitivity;

        
        _yAngle = Mathf.Clamp(_yAngle, -70, 30);


        transform.rotation = Quaternion.Euler(_yAngle, _xAngle, 0);
    }

    private void LateUpdate()
    {
        // ·ÎÄÃ ÁÂÇ¥
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance, _mask))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));

            Camera.main.transform.localPosition = currentPos;
        }
        else
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, currentPos, Time.deltaTime * cameraSpeed);
        }
        //

        #region mouse wheel
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance, _mask) == false)
        {
            Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
            Debug.DrawRay(transform.position, dir);
            if ((transform.position - Camera.main.transform.position).magnitude <= 2)
            {
                if (_mouseWheel < 0)
                {
                    Camera.main.transform.position += -dir * Time.deltaTime * _wheelSpeed;
                    defaultPos = Camera.main.transform.localPosition;
                }
            }
            else if ((transform.position - Camera.main.transform.position).magnitude >= 8)
            {
                if (_mouseWheel > 0)
                {
                    Camera.main.transform.position += dir * Time.deltaTime * _wheelSpeed;
                    defaultPos = Camera.main.transform.localPosition;
                }
            }
            else
            {
                if (_mouseWheel > 0)
                {
                    Camera.main.transform.position += dir * Time.deltaTime * _wheelSpeed;
                    defaultPos = Camera.main.transform.localPosition;
                }
                else if (_mouseWheel < 0)
                {
                    Camera.main.transform.position += -dir * Time.deltaTime * _wheelSpeed;
                    defaultPos = Camera.main.transform.localPosition;
                }
            }
        }
        
        #endregion
        transform.position = _player.transform.position + _delta;

    }
}
