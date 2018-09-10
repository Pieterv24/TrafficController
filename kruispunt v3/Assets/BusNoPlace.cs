using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusNoPlace : MonoBehaviour {

    public bool occupied = false; 

    void OnTriggerEnter(Collider other)
    {
        occupied = true;
    }

    void OnTriggerExit(Collider other)
    {
        occupied = false;
    }
}
