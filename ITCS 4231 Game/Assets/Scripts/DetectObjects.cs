using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectObjects : MonoBehaviour
{

    [SerializeField] private float openDist = 3;
    private float dist = 0;
    [SerializeField] private Text interact;
    public GameObject detectedObject;
    public string detected = "";


    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, (transform.position - Camera.main.transform.position), Color.red);

        if (Physics.Raycast(transform.position, (transform.position - Camera.main.transform.position), out hit))
        {
            if (hit.collider.tag == "Door" && hit.distance < openDist)
            {
                //dist = hit.distance;
                detected = "Door";
                detectedObject = hit.collider.gameObject;
                interact.text = "F to open";
            }
            else if ((hit.collider.tag == "Gas" || hit.collider.tag == "Key") && hit.distance < openDist)
            {
                //dist = hit.distance;
                detected = "Item";
                detectedObject = hit.collider.gameObject;
                interact.text = "F to pick up";
            }
            /*
            else if (hit.collider.tag == "Key" && hit.distance < openDist)
            {
                //dist = hit.distance;
                detected = "Item";
                detectedObject = hit.collider.gameObject;
                interact.text = "F to pick up";
            } */
            else if ((hit.collider.tag == "Boat" || hit.collider.tag == "Helicopter") && hit.distance < openDist)
            {
                detected = "Vehicle";
                detectedObject = hit.collider.gameObject;
                if (EscapeManager.self.CanEscape(detectedObject.tag))
                    interact.text = "F to Escape";
            }
            else
            {
                //dist = 0f;
                detected = "";
                interact.text = "";
            }
            //TODO check for item detected
        }
    }
}
