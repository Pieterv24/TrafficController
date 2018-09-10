using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primaryTrigger : MonoBehaviour {

    public bool occupied = false;
    public string id = "";
    public bool isPrimary = true;
    public GameObject networkObject;
    private Networking networkHandler;

    void Start()
    {
        networkHandler = networkObject.GetComponent<Networking>();
        //Debug.Log(networkHandler == null);
    }
    
    void FixedUpdate()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);

        if (!string.IsNullOrEmpty(id))
        {
           
            if (Physics.Raycast(transform.position, up, 10))
            {
                if (occupied == false)
                {
                    occupied = true;
                    networkHandler.addTrigger(new Assets.Models.Trigger()
                    {
                        type = isPrimary ? "PrimaryTrigger" : "SecondaryTrigger",
                        triggered = occupied,
                        id = id
                    });
                }
            }
            else
            {
                if (occupied == true)
                {
                    occupied = false;
                    networkHandler.addTrigger(new Assets.Models.Trigger()
                    {
                        type = isPrimary ? "PrimaryTrigger" : "SecondaryTrigger",
                        triggered = occupied,
                        id = id
                    });
                }
            }
        }
        
    }
}
