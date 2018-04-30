using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {


    public static InventoryManager self;

    void Awake()
    {
        // The singleton reference hasn't been set yet -> set it
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(gameObject);
        }
        // The singleton already exists -> destroy this one
        else
        {
            Destroy(gameObject);
        }
    }

    bool hasBoatKey;
    bool hasHeliKey;
    bool hasBoatGas;
    bool hasHeliGas;

    //Reference to UI elements displaying inventory items
    [SerializeField] private Image boatKey;
    [SerializeField] private Image heliKey;
    [SerializeField] private Image boatGas;
    [SerializeField] private Image heliGas;

    //Sprites used within each UI image (bk = boatkey, etc)
    [SerializeField] private Sprite q_sprite;    // question mark is default, before item is picked up
    [SerializeField] private Sprite key_sprite;
    [SerializeField] private Sprite gas_sprite;

    private void Start()
    {
        hasBoatKey = false;
        hasHeliKey = false;
        hasBoatGas = false;
        hasHeliGas = false;

        //Set all inventory slots to question marks initially
        boatKey.sprite = q_sprite;
        heliKey.sprite = q_sprite;
        boatGas.sprite = q_sprite;
        heliGas.sprite = q_sprite;
    }

    //  In each setter, sprite for specific inventory slot is updated with sprite 
    //  This should be safe since each method will only be called once per game (hopefully)
    public void setBoatKey(bool has)
    {
        hasBoatKey = has;
        boatKey.sprite = key_sprite;
        boatKey.transform.eulerAngles = new Vector3(0f, 0f, -45f);
    }
    public void setHeliKey(bool has)
    {
        hasHeliKey = has;
        heliKey.sprite = key_sprite;
        heliKey.transform.eulerAngles = new Vector3(0f, 0f, -45f);
    }
    public void setBoatGas(bool has)
    {
        hasBoatGas = has;
        boatGas.sprite = gas_sprite;
    }
    public void setHeliGas(bool has)
    {
        hasHeliGas = has;
        heliGas.sprite = gas_sprite;
    }

    public bool getBoatKey()
    {
        return hasBoatKey;
    }
    public bool getHeliKey()
    {
        return hasHeliKey;
    }
    public bool getBoatGas()
    {
        return hasBoatGas;
    }
    public bool getHeliGas()
    {
        return hasHeliGas;
    }

}
