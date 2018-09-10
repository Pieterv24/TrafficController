using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCar : MonoBehaviour {
    
    [SerializeField]
    GameObject _car1;
    [SerializeField]
    GameObject _car2;
    [SerializeField]
    GameObject _car3;
    [SerializeField]
    GameObject _car4;
    [SerializeField]
    GameObject _car5;
    [SerializeField]
    GameObject _car6;
    [SerializeField]
    GameObject _car7;
    [SerializeField]
    GameObject _car8;
    [SerializeField]
    GameObject _begin1;
    [SerializeField]
    GameObject _begin2;
    [SerializeField]
    GameObject _begin3;
    [SerializeField]
    GameObject _begin4;
    [SerializeField]
    GameObject _begin5;
    [SerializeField]
    GameObject _begin6;
    [SerializeField]
    GameObject _begin7;
    [SerializeField]
    GameObject _begin8;
    [SerializeField]
    GameObject _begin9;
    [SerializeField]
    GameObject _begin10;
    [SerializeField]
    GameObject _begin11;

    public GameObject car;

    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject car4;
    public GameObject car5;
    public GameObject car6;
    public GameObject car7;
    public GameObject car8;

    public Vector3 beginpoint;

    public Vector3 beginpoint1;
    public Vector3 beginpoint2;
    public Vector3 beginpoint3;
    public Vector3 beginpoint4;
    public Vector3 beginpoint5;
    public Vector3 beginpoint6;
    public Vector3 beginpoint7;
    public Vector3 beginpoint8;
    public Vector3 beginpoint9;
    public Vector3 beginpoint10;
    public Vector3 beginpoint11;

    public float timer = 0f;
    public float fasttimer = 0f;



    public List<GameObject> cars = new List<GameObject>();
    public List<Vector3> beginings = new List<Vector3>();
    public List<Vector3> fastlane = new List<Vector3>();
    System.Random r = new System.Random();


    void Start ()
    {
        beginpoint1 = _begin1.transform.position;
        beginpoint2 = _begin2.transform.position;
        beginpoint3 = _begin3.transform.position;
        beginpoint4 = _begin4.transform.position;
        beginpoint5 = _begin5.transform.position;
        beginpoint6 = _begin6.transform.position;
        beginpoint7 = _begin7.transform.position;
        beginpoint8 = _begin8.transform.position;
        beginpoint9 = _begin9.transform.position;
        beginpoint10 = _begin10.transform.position;
        beginpoint11 = _begin11.transform.position;

        beginings.Add(beginpoint1);
        beginings.Add(beginpoint2);
        beginings.Add(beginpoint3);
        beginings.Add(beginpoint4);
        beginings.Add(beginpoint5);
        beginings.Add(beginpoint6);
        beginings.Add(beginpoint7);
        beginings.Add(beginpoint8);
        beginings.Add(beginpoint9);
        beginings.Add(beginpoint10);
        beginings.Add(beginpoint11);

        fastlane.Add(beginpoint4);
        fastlane.Add(beginpoint4);
        fastlane.Add(beginpoint9);
        fastlane.Add(beginpoint10);

        car1 = _car1;
        car2 = _car2;
        car3 = _car3;
        car4 = _car4;
        car5 = _car5;
        car6 = _car6;
        car7 = _car7;
        car8 = _car8;

        cars.Add(car1);
        cars.Add(car2);
        cars.Add(car3);
        cars.Add(car4);
        cars.Add(car5);
        cars.Add(car6);
        cars.Add(car7);
        cars.Add(car8);
        
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= 2) 
        {
            int k = r.Next(7);
            car = cars[k];
            int num = r.Next(11);
            beginpoint = beginings[num];// moet nog random
            if (!GameObject.Find("carspawn" + (num + 1) ).GetComponent<BusNoPlace>().occupied) //will check if true
            {
                Instantiate(car, beginpoint, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
