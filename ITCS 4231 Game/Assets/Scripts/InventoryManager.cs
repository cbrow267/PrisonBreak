using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool hasBoatKey = false;
    bool hasHeliKey = false;
    bool hasBoatGas = false;
    bool hasHeliGas = false;

    public void setBoatKey(bool has)
    {
        hasBoatKey = has;
    }
    public void setHeliKey(bool has)
    {
        hasHeliKey = has;
    }
    public void setBoatGas(bool has)
    {
        hasBoatGas = has;
    }
    public void setHeliGas(bool has)
    {
        hasHeliGas = has;
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
