using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnBicycle : MonoBehaviour {

    [SerializeField]
    GameObject _bike1;
    [SerializeField]
    GameObject _bike2;
    [SerializeField]
    GameObject _bike3;
    [SerializeField]
    
    GameObject _begin1;
    [SerializeField]
    GameObject _begin2;
    [SerializeField]
    GameObject _begin3;
    [SerializeField]
    GameObject _begin4;

    public GameObject bike;

    public GameObject bike1;
    public GameObject bike2;
    public GameObject bike3;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;
    public Vector3 beginpoint3;
    public Vector3 beginpoint4;

    public float timer = 0f;

    public List<GameObject> Bikes = new List<GameObject>();
    public List<Vector3> beginings = new List<Vector3>();
    System.Random r = new System.Random();


    void Start()
    {
        beginpoint1 = _begin1.transform.position;
        beginpoint2 = _begin2.transform.position;
        beginpoint3 = _begin3.transform.position;
        beginpoint4 = _begin4.transform.position;

        beginings.Add(beginpoint1);
        beginings.Add(beginpoint2);
        beginings.Add(beginpoint3);
        beginings.Add(beginpoint4);
        
        bike1 = _bike1;
        bike2 = _bike2;
        bike3 = _bike3;

        Bikes.Add(bike1);
        Bikes.Add(bike2);
        Bikes.Add(bike3);

    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 7) // other 7
        {
            int k = r.Next(3);
            bike = Bikes[k];
            int num = r.Next(4);
            beginpoint = beginings[num];
            if (!GameObject.Find("biginpointBike" + (num + 1)).GetComponent<BusNoPlace>().occupied) //will check if true
            {
                Instantiate(bike, beginpoint, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
