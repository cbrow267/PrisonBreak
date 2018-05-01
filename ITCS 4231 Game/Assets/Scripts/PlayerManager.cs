using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private Animator anim;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject camTarget;
    public bool canMove = true;

    public Inventory inventory;
    // Use this for initialization
    void Start() {
        anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.idle);
        anim.SetInteger(HashIDs.self.playerStateInt, (int)PlayerState.standing);
    }

    // Update is called once per frame
    void Update()
    {
        float xmove = Input.GetAxis("Horizontal");
        float zmove = Input.GetAxis("Vertical");

        if (canMove)
        {
            //print("player" + transform.position);
            //Player is not moving
            if (xmove == 0 && zmove == 0) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.idle); }
            //Press W to move forward
            if (zmove > 0) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.forward); }
            //Press S to move backward
            if (zmove < 0) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.backward); }
            //Press D to move right
            if (xmove > 0) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.right); }
            //Press A to move left
            if (xmove < 0) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.left); }
            //Press spacebar to roll forward

            if (Input.GetKeyDown(KeyCode.Space)) { anim.SetInteger(HashIDs.self.playerMovementTypeInt, (int)PlayerMovementType.dive); }
            //Press C to toggle crouch
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (anim.GetInteger(HashIDs.self.playerStateInt) == 0)
                {
                    anim.SetInteger(HashIDs.self.playerStateInt, (int)PlayerState.crouched);
                }
                else if (anim.GetInteger(HashIDs.self.playerStateInt) == 1)
                {
                    anim.SetInteger(HashIDs.self.playerStateInt, (int)PlayerState.standing);
                }
            }

            //Press tab to switch camera shoulder
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (camTarget.transform.localPosition.x > 0f)
                    camTarget.transform.Translate(-(0.25f * 2), 0f, 0f);
                else if (camTarget.transform.localPosition.x < 0f)
                    camTarget.transform.Translate((0.25f * 2), 0f, 0f);
            }


            //Press F to interact with vehicle, door or item
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject seen = camTarget.GetComponent<DetectObjects>().detectedObject;
                if (seen != null)
                { 
                    if (seen.tag == "Door")
                    {
                        seen.GetComponent<DoorController>().OpenClose();
                    }
                    else if (camTarget.GetComponent<DetectObjects>().detected == "Item")
                    {
                        ItemManager.self.PickUp(camTarget, seen);
                    }
                    else if ((camTarget.GetComponent<DetectObjects>().detected == "Vehicle") && EscapeManager.self.CanEscape(seen.tag))
                    {
                        EscapeManager.self.Escape(seen.tag);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

   /* void playFootStep()
    {
//<<<<<<< HEAD
        //audioSource = footstep[Random.Range(0, footstep.Length()];
//=======
        //audioSource = footstep[Random.Range(0, footstep.length);
//>>>>>>> 55916f5deba8b87ec7fad2ba2831f4928f46e12c
        audioSource.volume = 0.3f;
        audioSource.Play();
    }*/

    public void SetDirection()
    {
        Vector3 towards = transform.eulerAngles;
        towards.y = Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = towards;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if(item != null)
        {
            inventory.AddItem(item);
        }
    }
}
