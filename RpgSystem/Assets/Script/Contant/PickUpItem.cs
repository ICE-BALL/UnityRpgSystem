using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    define.InventoryType _inventoryType;

    [SerializeField]
    string _name = "";

    [SerializeField]
    GameObject _player;

    public float _scanRange = 1f;
    
    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    [System.Obsolete]
    public void Update()
    {
        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance <= 2f)
        {
            if (Input.GetKey(KeyCode.F))
            {
                UI_Inven.AddItem(_name, _inventoryType);
            }
        }
    }
}
