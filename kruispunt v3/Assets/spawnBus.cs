using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBus : MonoBehaviour {

    [SerializeField]
    GameObject _bus;
    [SerializeField]
    GameObject _spawnPoint;
    [SerializeField]
    GameObject _begin;
    public Vector3 beginpoint;
    public float timer = 0f;
    public GameObject bus;

	void Start ()
    {
        bus = _bus;
        beginpoint = _begin.transform.position;
    }
	

	void Update ()
    {
        timer += Time.deltaTime;

        if (!GameObject.Find("busspawn").GetComponent <BusNoPlace> ().occupied) //will check if place
        {
            if (timer >= 30)// veranderen naar 60
            {
                Quaternion draaiy = Quaternion.identity;
                draaiy.y = 40;
                Instantiate(bus,beginpoint, draaiy);
                timer = 0;
            }
        }

        
	}
}
