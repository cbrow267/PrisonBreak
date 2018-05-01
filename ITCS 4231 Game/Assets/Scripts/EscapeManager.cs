using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text interact;
    [SerializeField] private AudioSource boatSound;
    [SerializeField] private AudioSource heliSound;
    [SerializeField] private AudioSource bg_music;
    [SerializeField] private AudioClip bg_clip;
    [SerializeField] private AudioClip victory_clip;

    private Animator anim;
    private GameObject player;
    //True if the player has acquired necessary items to escape for specified vehicle
    private bool canEscape_Boat = false;     
    private bool canEscape_Heli = false;
    public bool escaping = false;

    private void Start()
    {
        boatCam.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        heliCam.enabled = false;
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
        interact.enabled = false;
        bg_music.clip = victory_clip;
        bg_music.Play();
        if (vehicle == "Boat")
        {
            Camera.main.enabled = false;
            boatCam.enabled = true;
            canEscape_Boat = true;

            player.GetComponent<PlayerManager>().canMove = false;
            player.transform.eulerAngles = new Vector3(0f, 90f, 0f);
            player.transform.parent = boat.transform;
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<Rigidbody>().detectCollisions = false;
            player.GetComponent<Rigidbody>().useGravity = false;

            player.transform.position = new Vector3(boat.transform.position.x + 0.455f, boat.transform.position.y - 0.134f, boat.transform.position.z);

            anim.SetBool("Escaping", true);
            escaping = true;
            boatSound.Play();
        }
        else if (vehicle == "Helicopter")
        {
            Camera.main.enabled = false;
            heliCam.enabled = true;
            canEscape_Heli = true;

            player.SetActive(false);
            escaping = true;
            heliSound.Play();
        }
    }

    private void BoatEscape()
    {
        Debug.DrawRay(boat.transform.position - 2 * (boat.transform.right), -boat.transform.right, Color.yellow);

        boat.transform.position = new Vector3(boat.transform.position.x + 0.3f, boat.transform.position.y, boat.transform.position.z);

        boatSound.volume = boatSound.volume - 0.005f;
    }

    private void HeliEscape()
    {
        Debug.DrawRay(heli.transform.position + 5 * (heli.transform.forward) + heli.transform.up, heli.transform.forward, Color.blue);
        heliCam.transform.LookAt(heli.transform);
        if (heli.transform.position.y < 12f)
        {
            heli.transform.position = new Vector3(heli.transform.position.x, heli.transform.position.y + .2f, heli.transform.position.z);
        }
        else
        {
            if (heli.transform.position.z < 1000f)
            {
                heli.transform.position = new Vector3(heli.transform.position.x, heli.transform.position.y, heli.transform.position.z + 0.5f);
            }
            if (heli.transform.eulerAngles.x < 20)
            {
                heli.transform.eulerAngles = new Vector3(heli.transform.eulerAngles.x + 0.5f, 0f, 0f);
            }
        }
        if (heliSound.volume >= 0.5f)
            heliSound.volume = heliSound.volume - 0.005f;
        else
            heliSound.volume = heliSound.volume - 0.001f;
    }
}
