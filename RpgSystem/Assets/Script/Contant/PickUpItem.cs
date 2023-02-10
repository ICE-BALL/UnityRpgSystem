using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
<<<<<<< HEAD
    public define.InventoryType _inventoryType;

    public string _name = "";

    GameObject _player;
=======
    [SerializeField]
    define.InventoryType _inventoryType;

    [SerializeField]
    string _name = "";

    [SerializeField]
    GameObject _player;

>>>>>>> f46f6deb278149df6a7ccc3a3f7f09d52e8747f6
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
<<<<<<< HEAD
                UI_Inven.AddItem(_name);
                Resource.Destroy(gameObject);
=======
                UI_Inven.AddItem(_name, _inventoryType);
>>>>>>> f46f6deb278149df6a7ccc3a3f7f09d52e8747f6
            }
        }
    }
}
