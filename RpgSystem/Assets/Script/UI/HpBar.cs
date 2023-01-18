using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    GameObject _obj;

    Slider _hpBar;
    Stat _stat;
    Transform _parent;
    Transform _cam;

    void Start()
    {
        if (_obj == null)
            _obj = transform.parent.transform.parent.gameObject;
        _parent = gameObject.transform.parent;
        _stat = _obj.GetComponent<Stat>();
        _hpBar = gameObject.GetComponent<Slider>();
        _cam = Camera.main.transform.parent;
    }

    void Update()
    {
        _parent.rotation = _cam.localRotation;
        _hpBar.value = _stat.Hp / (float)_stat.MaxHp;
    }
}
