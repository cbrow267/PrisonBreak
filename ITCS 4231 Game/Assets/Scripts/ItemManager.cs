using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public static ItemManager self;
    [SerializeField] private Text acItem;

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
        if (item.tag == "Gas" && rand <= 0.7f && InventoryManager.self.getBoatGas())
        {
            rand = 1f;
        }
        else if (item.tag == "Key" && rand <= 0.7f && InventoryManager.self.getBoatKey())
        {
            rand = 1f;
        }
        else if (item.tag == "Gas" && rand > 0.8f && InventoryManager.self.getHeliGas())
        {
            rand = 0f;
        }
        else if (item.tag == "Key" && rand > 0.8f && InventoryManager.self.getBoatGas())
        {
            rand = 0f;
        }

        if (rand <= 0.8f)   //boat
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
    IEnumerator ItemAcquired()
    {
        yield return new WaitForSeconds(3f);
        acItem.text = "";
    }
}
