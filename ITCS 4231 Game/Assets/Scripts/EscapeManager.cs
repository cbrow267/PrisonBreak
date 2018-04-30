using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour {


    public static EscapeManager self;
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

    [SerializeField] private Camera boatCam;
    [SerializeField] private Camera heliCam;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject heli;

    private Animator anim;
    private GameObject player;
    //True if the player has acquired necessary items to escape for specified vehicle
    private bool canEscape_Boat = false;     
    private bool canEscape_Heli = false;
    private void Start()
    {
        Vector3.Lerp(boat.transform.position, boat.transform.forward * 1000, 5);

        boatCam.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        //heliCam.enabled = false;
    }
    private void Update()
    {
        Debug.DrawRay(heli.transform.position + 3 * (heli.transform.forward) + heli.transform.up, heli.transform.forward, Color.blue);

        if (canEscape_Heli) 
        {
            HeliEscape();
        }
        else if (canEscape_Boat) 
        {
            BoatEscape();
        }
    }

    public bool CanEscape(string vehicle)
    {
        if (vehicle == "Boat")
        {
            if (InventoryManager.self.getBoatGas() && InventoryManager.self.getBoatKey())
                return true;
            else
                return false;
        }
        else if (vehicle == "Helicopter")
        {
            if (InventoryManager.self.getHeliGas() && InventoryManager.self.getHeliKey())
                return true;
            else
                return false;
        }
        return true;
    }

    public void Escape(string vehicle)
    {
        if (vehicle == "Boat")
        {
            Camera.main.enabled = false;
            boatCam.enabled = true;
            canEscape_Boat = true;

            player.GetComponent<PlayerManager>().canMove = false;
            player.transform.parent = boat.transform;
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<Rigidbody>().detectCollisions = false;
            //player.transform.position = new Vector3(boat.transform.position.x, boat.transform.position.y - 0.4f, boat.transform.position.z);
            player.transform.position = new Vector3(boat.transform.position.x + 0.455f, boat.transform.position.y - 0.134f, boat.transform.position.z);
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y + 180, player.transform.eulerAngles.z);

            anim.SetBool("Escaping", true);
        }
        else if (vehicle == "Helicopter")
        {
            //Camera.main.enabled = false;
            //heliCam.enabled = true;
            canEscape_Heli = true;

            player.SetActive(false);
        }
    }

    private void BoatEscape()
    {
        Debug.DrawRay(boat.transform.position - 2 * (boat.transform.right), -boat.transform.right, Color.yellow);

        boat.transform.position = new Vector3(boat.transform.position.x, boat.transform.position.y, boat.transform.position.z + 0.3f);
    }

    private void HeliEscape()
    {
        //heli.transform.position = new Vector3(heli.transform.position.x, heli.transform.position.y, heli.transform.position.z + 0.3f);
        Debug.DrawRay(heli.transform.position + 5 * (heli.transform.forward) + heli.transform.up, heli.transform.forward, Color.blue);
        Debug.Log("heli away!!!!!!!!!!!!");

        // TODO 
        // 1.  for escaping, move helicopter straight up until y == 15, 
        //     then begin moving forward and add to x rotation until x == (somewhere between 15-20)
        // 2. Play rotor sound effect
    }
}
