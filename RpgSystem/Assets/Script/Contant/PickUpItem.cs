using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public define.InventoryType _inventoryType;

    public string _name = "";

    GameObject _player;
    public float _scanRange = 1f;
    
    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        float distance = (_player.transform.position - transform.position).magnitude;

        if (distance <= 2f)
        {
            if (Input.GetKey(KeyCode.F))
            {
                UI_Inven.AddItem(_name);
                Resource.Destroy(gameObject);
            }
        }
    }
}
