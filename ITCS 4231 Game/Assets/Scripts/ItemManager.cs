using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public static ItemManager self;
    [SerializeField] private Text acItem;
    float itemChance = 0.5f;

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

    // take object as param
    // store item in inventory
    // deactivate item
    // return
    public void PickUp(GameObject camTarget, GameObject item)
    {
        float rand = Random.Range(0f, 1f);
        if (item.tag == "Gas" && rand <= itemChance && InventoryManager.self.getBoatGas())
        {
            rand = 1f;
        }
        else if (item.tag == "Key" && rand <= itemChance && InventoryManager.self.getBoatKey())
        {
            rand = 1f;
        }
        else if (item.tag == "Gas" && rand > itemChance && InventoryManager.self.getHeliGas())
        {
            rand = 0f;
        }
        else if (item.tag == "Key" && rand > itemChance && InventoryManager.self.getHeliKey())
        {
            rand = 0f;
        }

        if (rand <= itemChance)   //boat
        {
            if (item.tag == "Gas")
            {
                InventoryManager.self.setBoatGas(true);
                item.SetActive(false);
                acItem.text = "Boat Fuel acquired";

            }
            else if (item.tag == "Key")
            {
                InventoryManager.self.setBoatKey(true);
                item.SetActive(false);
                acItem.text = "Boat Key acquired";
            }
        }
        else    //heli
        {
            if (item.tag == "Gas")
            {
                InventoryManager.self.setHeliGas(true);
                item.SetActive(false);
                acItem.text = "Helicopter Fuel acquired";
            }
            else if (item.tag == "Key")
            {
                InventoryManager.self.setHeliKey(true);
                item.SetActive(false);
                acItem.text = "Helicopter Key acquired";
            }
        }
        StartCoroutine(ItemAcquired());
    }
    WaitForSeconds wfs = new WaitForSeconds(3f);
    IEnumerator ItemAcquired()
    {
        yield return wfs;
        acItem.text = "";
    }
}
